namespace Application.Dto.Image;

public class ImageDto
{
    public Guid Id { get; set; }
    public required string ImageUrl { get; set; }
    public required string ImageName { get; set; }
}