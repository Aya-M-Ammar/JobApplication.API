namespace JobApplication.Application.DTOs
{
    public class JobDTO
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool IsActive { get; set; } = default!;

    }
}
