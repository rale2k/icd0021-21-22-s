namespace WebApp.Models;

public class RoomTypeAmenityViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}