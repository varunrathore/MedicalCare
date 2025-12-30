namespace MedicalCare.Presentation.ViewModels.Admin
{
    public class TestModalVm
    {
        public Guid? Id { get; set; }   // null = Create, value = Edit
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }

        public bool IsEdit => Id.HasValue;
    }
}
