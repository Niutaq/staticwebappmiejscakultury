using Application.CQRS.Account.Static;
using Application.CQRS.Comment.Commands.AddComment;
using Application.CQRS.Comment.Commands.DeleteComment;
using Application.CQRS.Comment.Commands.UpdateComment;
using Application.CQRS.Comment.Dtos;
using Application.CQRS.Comment.Queries.DisplayComments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Areas.Comment;

[Route("api/comment")]
public class CommentController : BaseController
{
    /// <summary>
    /// Add comment to exists post
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.User)]
    [HttpPost("add-comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddCommentToPost([FromBody] AddCommentCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Update exists comment
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.User)]
    [HttpPut("update-comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Delete comment
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [Authorize(Roles = UserRoles.User)]
    [HttpDelete("delete-comment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeteleComment([FromBody] DeleteCommentCommand command, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(command, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Display comments
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{PostId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CommentDto>>> DisplayComments([FromRoute] DisplayCommentsQuery query, CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(query, cancellationToken);

        return Ok(response);
    }
}