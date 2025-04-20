using MediatR;

namespace Application.CQRS.Account.Events.SendConfirmAccountEmail;

public sealed record SendConfirmAccountEmailEvent(Guid? UserId = null) : INotification;