using HelpPawApi.Domain.Entities.Notification;

public class Notification
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string UserId { get; set; }

    public string Title { get; set; }
    public string Message { get; set; }

    public NotificationType Type { get; set; }

    public bool IsRead { get; set; } = false;

    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
}
