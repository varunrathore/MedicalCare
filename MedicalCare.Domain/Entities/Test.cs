namespace MedicalCare.Domain.Entities
{
    public class Test
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }
        public TestCategory Category { get; set; } = null!;

        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
