namespace Services.DTOs.Review;

public class CreateReviewRequest
{
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public Guid? StoreId { get; set; }
    public Guid? CourierId { get; set; }
    public Guid? OrderId { get; set; }
}
