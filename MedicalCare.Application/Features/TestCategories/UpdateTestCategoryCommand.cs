using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalCare.Application.Features.TestCategories
{
    public class UpdateTestCategoryCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
