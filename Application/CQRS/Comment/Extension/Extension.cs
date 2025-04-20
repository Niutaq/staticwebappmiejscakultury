using Application.CQRS.Comment.Dtos;
using Domain.Entities;

namespace Application.CQRS.Comment.Extension;

public static class Extension
{
    public static CommentDto CommentAsDto(this Comments comment)
    {
        
        return new CommentDto
        {
            Id = comment.Id,
            DateAdded = comment.DateAdded,
            Message = comment.Message,
            Name = comment.Users.Name,
            Surname = comment.Users.Surname
        };
    }
}