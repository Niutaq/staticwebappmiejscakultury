namespace Domain.Entities;

public class Postimage
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContentType { get; set; }
    public long TotalBytes { get; set; }
    public string S3Key { get; set; }
    public string Url { get; set; }
    public Guid PlacesId { get; set; }
    public Places Places { get; set; }
}