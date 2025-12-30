namespace MedicalCare.Presentation.ViewModels.Admin
{
    public class TestCategoryModalVm
    {
        public Guid? Id { get; set; }   // null = Create, value = Edit
        public string Name { get; set; } = string.Empty;

        public bool IsEdit => Id.HasValue;
    }
}
