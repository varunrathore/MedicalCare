using MedicalCare.Application.Features.TestCategories;
using MedicalCare.Presentation.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;
using MedicalCare.Application.Interfaces;

namespace MedicalCare.Presentation.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class TestCategoryController : Controller
    {

        private readonly CreateTestCategoryHandler _handler;
        private readonly GetAllTestCategoriesHandler _getAllHandler;
        private readonly UpdateTestCategoryHandler _updateHandler;
        private readonly ToggleTestCategoryStatusHandler _toggleHandler;
        private readonly ITestCategoryRepository _testCategoryRepository;

        public TestCategoryController(
            CreateTestCategoryHandler handler,
            UpdateTestCategoryHandler updateHandler,
            GetAllTestCategoriesHandler getAllHandler,
            ToggleTestCategoryStatusHandler toggleHandler,
            ITestCategoryRepository testCategoryRepository)
        {
            _handler = handler;
            _getAllHandler = getAllHandler;
            _updateHandler = updateHandler;
            _toggleHandler = toggleHandler;
            _testCategoryRepository = testCategoryRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var categories = await _getAllHandler.HandleAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return PartialView("_CategoryModal", new TestCategoryModalVm());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, string name)
        {
            var category = await _testCategoryRepository.GetByIdAsync(id);
            return PartialView("_CategoryModal", new TestCategoryModalVm
            {
                Id = id,
                Name = name,
                IsActive = category.IsActive,
                IsSystemCategory = category.IsSystem()

            });
        }
        [HttpPost]
        public async Task<IActionResult> Save(TestCategoryModalVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (vm.IsEdit)
                {
                    await _updateHandler.HandleAsync(new UpdateTestCategoryCommand {
                        Id = vm.Id!.Value,
                        Name = vm.Name
                    });
                } else
                {
                    await _handler.HandleAsync(new CreateTestCategoryCommand
                    {
                        Name = vm.Name
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> ToggleStatus(Guid id)
        {
            await _toggleHandler.HandleAsync(new ToggleTestCategoryStatusCommand
            {
                Id = id
            });
            return RedirectToAction("Index");
        }
    }
}
