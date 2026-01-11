namespace MedicalCare.Application.Features.Tests
{
    public class CreateTestCommand
    {
        public string Name { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
