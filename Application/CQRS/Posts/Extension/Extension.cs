using Application.CQRS.Posts.Dtos;
using Domain.Entities;

namespace Application.CQRS.Posts.Extension;

public static class Extension
{
    public static DisplayPostsDto PostAsDto(this Places post)
    {
        var ulrList = new List<string>();

        foreach (var image in post.images)
        {
            ulrList.Add(image.Url);
        }
        int userCount = post.ratings.Count();
        int totalRating = post.ratings.Sum(x => (int)x.Rating);
        double averageRating = userCount > 0 ? Math.Round((double)totalRating / userCount, 2) : 0;
        return new DisplayPostsDto
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            LocalizationX = post.LocalizationX,
            LocalizationY = post.LocalizationY,
            AverageRating = averageRating,
            Images = ulrList,
            LikesCount = post.LikedBy?.Count ?? 0
        };
    }
}