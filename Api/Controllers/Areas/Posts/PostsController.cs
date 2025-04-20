using Application.CQRS.Account.Static;
using Application.CQRS.Posts.Commands.AddPosts;
using Application.CQRS.Posts.Commands.DeletePosts;
using Application.CQRS.Posts.Commands.LikePosts;
using Application.CQRS.Posts.Commands.UpdatePosts;
using Application.CQRS.Posts.Dtos;
using Application.CQRS.Posts.Queries.DisplayPosts;
using Application.CQRS.Posts.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Areas.Posts;

[Route("api/post")]
public class PostsController : BaseController
{
    /// <summary>
    /// Add post by admin
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPost("add-posts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPosts([FromForm] AddPostsCommand command, CancellationToken cancellationToken)
    {
        
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Update posts
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.Admin)]
    [HttpPut("update-posts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePosts([FromBody] UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Delete posts
    /// TODO Jak będzie dodawanie lików i ocen, to trzeba dodać też usuwanie wszystkich ocen i lików
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.Admin)]
    [HttpDelete("delete-posts")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeletePosts([FromBody] DeletePostsCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Display posts
    /// TODO Jak będzie dodawanie lików i ocen, to trzeba dodać wyświetlanie lików i ocen
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{Category}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<DisplayPostsDto>>> DisplayPosts([FromRoute] DisplayPostsQuery query, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }
    
    /// <summary>
    /// Like a post
    /// </summary>
    /// <param name="postId"></param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.User)]
    [HttpPost("like-post")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> LikePost([FromBody] LikePostCommand command, CancellationToken cancellationToken)
    {
        var response=await Mediator.Send(command, cancellationToken);
        return Ok(response);
    }
}