using Amazon.S3;
using Amazon.S3.Model;
using Application.Persistance.Interfaces.S3StorageInterfaces;
using Domain.Entities;
using Infrastructure.Persistance.FilesStorage.Configuration;
using Infrastructure.Persistance.FilesStorage.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistance.FilesStorage.Services;

public class S3StorageService : IS3StorageService
{
    private readonly S3Configuration _s3Config;
    private readonly AmazonS3Client _s3Client;
    private readonly MiejscaKulturyDbContext _context;

    public S3StorageService(S3Configuration s3Config, MiejscaKulturyDbContext context)
    {
        _s3Config = s3Config;
        _context = context;
        var connectionConfig = new AmazonS3Config
        {
            ServiceURL = _s3Config.S3Url,
            ForcePathStyle = true,
            UseHttp = true
        };
        _s3Client = new AmazonS3Client(_s3Config.AccessKey, _s3Config.SecretKey, connectionConfig);
    }
    
    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";

        try
        {
            await using var stream = file.OpenReadStream();
            await _s3Client.PutObjectAsync(new PutObjectRequest()
            {
                BucketName = _s3Config.BucketName,
                Key = uniqueFileName,
                InputStream = stream,
                ContentType = file.ContentType
            }, cancellationToken);
        }
        catch (AmazonS3Exception e)
        {
            if (e.ErrorCode == "NoSuchBucket")
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = _s3Config.BucketName,
                };
                await _s3Client.PutBucketAsync(putBucketRequest, cancellationToken);

                await using var stream = file.OpenReadStream();
                await _s3Client.PutObjectAsync(new PutObjectRequest()
                {
                    BucketName = _s3Config.BucketName,
                    Key = uniqueFileName,
                    InputStream = stream,
                    ContentType = file.ContentType
                }, cancellationToken);

                return uniqueFileName;
            }

            throw new S3UploadException(e.ErrorCode);
        }
        catch (Exception e)
        {
            throw new S3UnknownException();
        }

        return uniqueFileName;
    }

    public string GetFileUrl(string fileKey)
    {
        try
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = _s3Config.BucketName,
                Key = fileKey,
                Expires = System.DateTime.MaxValue
            };

            var url = _s3Client.GetPreSignedURL(request);
            url = url.Replace("https", "http");
            url = url.Replace("minio:9000", "localhost:9000");
            return url;
        }
        catch (AmazonS3Exception e)
        {
            throw new S3GetUrlException(e.ErrorCode);
        }
        catch (Exception e)
        {
            throw new S3UnknownException();
        }
    }

    public async Task<MemoryStream> GetFileAsync(string fileKey, CancellationToken cancellationToken)
    {
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = _s3Config.BucketName,
                Key = fileKey
            };

            var response = await _s3Client.GetObjectAsync(request, cancellationToken);

            await using var responseStream = response.ResponseStream;
            var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            return memoryStream;
        }
        catch (AmazonS3Exception e)
        {
            throw new S3GetUrlException(e.ErrorCode);
        }
        catch (Exception e)
        {
            throw new S3UnknownException();
        }
    }

    public async Task<Guid> SaveChangesAsync(Avatarimage avatarImage, CancellationToken cancellationToken)
    {
        await _context.AddAsync(avatarImage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return avatarImage.Id;
    }

    public async Task DeleteFileAsync(string s3Key, CancellationToken cancellationToken)
    {
        try
        {
            await _s3Client.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _s3Config.BucketName,
                Key = s3Key
            }, cancellationToken);
        }
        catch (AmazonS3Exception e)
        {
            throw new S3UploadException(e.ErrorCode);
        }
        catch (Exception e)
        {
            throw new S3UnknownException();
        }
    }

    public async Task SavePostImageAsync(Postimage image, CancellationToken cancellationToken)
    {
        await _context.AddAsync(image, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}