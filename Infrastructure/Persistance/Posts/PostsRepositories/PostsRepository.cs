using Application.CQRS.Comment.Dtos;
using Application.CQRS.Comment.Extension;
using Application.CQRS.Posts.Dtos;
using Application.CQRS.Posts.Extension;
using Application.CQRS.Ratings.Dtos;
using Application.CQRS.Ratings.Extension;
using Application.Persistance.Interfaces.PostsInterfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistance.Posts.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Posts.PostsRepositories;

public class PostsRepository : IPostsRepository
{
    private readonly MiejscaKulturyDbContext _context;

    public PostsRepository(MiejscaKulturyDbContext context)
    {
        _context = context;
    }
    
    public async Task AddPostAsync(Places place, CancellationToken cancellationToken)
    {
        await _context.AddAsync(place, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task IsPostExistAsync(Guid postId, CancellationToken cancellationToken)
    {
        var post = await _context.Place.FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);

        if (post is null) throw new PostNotFoundException();
    }

    public async Task AddCommentAsync(Comments comment, CancellationToken cancellationToken)
    {
        await _context.Comment.AddAsync(comment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateCommentAsync(Guid userId, Guid commentId, string message, CancellationToken cancellationToken)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken);

        if (comment is null || comment.UsersId != userId) throw new NotAccessToCommentException();

        comment.Message = message;

        _context.Comment.Update(comment);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCommentAsync(Guid userId, IList<string> roles, Guid commentId, CancellationToken cancellationToken)
    {
        var comment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == commentId, cancellationToken);

        if (comment is null)
        {
            throw new NotAccessToDeleteCommentException();
        }
        else if(roles.Contains("Admin") || comment.UsersId == userId)
        {
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            throw new NotAccessToDeleteCommentException();
        }
    }

    public async Task AddRatingAsync(Ratings ratings, CancellationToken cancellationToken)
    {
        await _context.Rating.AddAsync(ratings, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task IsRatingExistAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        var rating = await _context.Rating.FirstOrDefaultAsync(x => x.PlacesId == postId && x.UsersId == userId, cancellationToken);
        if (rating is not null) throw new RatingAlreadyExistsException();
    }

    public async Task UpdateRatingAsync(Guid userId, Guid placeId, TypesOfRatings newRating,
        CancellationToken cancellationToken)
    {
        var rating =
            await _context.Rating.FirstOrDefaultAsync(
                x => x.PlacesId == placeId && x.UsersId == userId, cancellationToken);
        if (rating is null)
        {
            throw new NoRattingToUpdateException();
        }
        rating.Rating = newRating;
        _context.Rating.Update(rating);
        await _context.SaveChangesAsync(cancellationToken);
        
    }

    public async Task UpdateAverageRatingsAsync(Guid placeId, CancellationToken cancellationToken)
    {
        var aver = await _context.Rating.Where(x => x.PlacesId == placeId).AverageAsync(x=>(int)x.Rating,cancellationToken);
        var place = await _context.Place.FirstOrDefaultAsync(x => x.Id == placeId,cancellationToken);
        _context.Place.Update(place);
        await _context.SaveChangesAsync(cancellationToken);
        
     
    }

    public async Task<RatingDto> DisplayRatingAsync(Guid placeId,Guid userId, CancellationToken cancellationToken)
    {
        var rating = await _context.Rating.FirstOrDefaultAsync(x => x.PlacesId == placeId && x.UsersId == userId,cancellationToken);
        if (rating is null)
        {
            throw new NoRatingToDisplayException();}
        else
        {
            return rating.RatingAsDto();
        }
    }
    public async Task UpdatePostAsync(Guid userId, Guid postId, PlacesCategory placesCategory, string title, string description,
        double localizationX, double localizationY, CancellationToken cancellationToken)
    {
        var post = await _context.Place.FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);

        if (post is null || post.UsersId != userId) throw new HasNoAccessException();

        post.Title = title;
        post.Description = description;
        post.Category = placesCategory;
        post.LocalizationX = localizationX;
        post.LocalizationY = localizationY;

        _context.Place.Update(post);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePostAsync(Guid userId, Guid postId, CancellationToken cancellationToken)
    {
        var post = await _context.Place.FindAsync(postId, cancellationToken);

        if (post is null || post.UsersId != userId) throw new HasNoAccessException();

        var comments = _context.Comment.Where(x => x.PlacesId == post.Id).ToList();

        var rating = _context.Rating.Where(x => x.PlacesId == post.Id).ToList();

        foreach (var comment in comments) 
            _context.Comment.Remove(comment);

        foreach (var ratings in rating)
            _context.Rating.Remove(ratings);
        
        
        _context.Place.Remove(post);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<DisplayPostsDto>> DisplayPostsAsync(PlacesCategory placesCategory, CancellationToken cancellationToken)
    {
        var post = await _context.Place.Include(x => x.images).Include(x=>x.ratings).
            Where(x => x.Category == placesCategory)
            .ToListAsync(cancellationToken);

        if (post is null) throw new HasNoDataException();

        return post.Select(x => x.PostAsDto()).ToList();
    }

    public async Task<List<CommentDto>> DisplayCommentAsync(Guid postId, CancellationToken cancellationToken)
    {
        var comments = await _context.Comment.Where(x => x.PlacesId == postId)
            .Include(x => x.Users).ToListAsync(cancellationToken);

        return comments.Select(x => x.CommentAsDto()).ToList();
    }
    
    public async Task<int> LikeSystemAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        
        var post = await _context.Place.FirstOrDefaultAsync(p => p.Id == postId, cancellationToken);
        
        if (post == null)
        {
            throw new PostNotFoundException();
        }

        if (post.LikedBy.Contains(userId))
        {
            post.LikedBy.Remove(userId);
        }
        else if (!post.LikedBy.Contains(userId))
        {
            post.LikedBy.Add(userId);
        }

        int likecout = post.LikedBy.Count;
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return likecout;
    }
}