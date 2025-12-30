using MedicalCare.Application.Features.TestCategories;
using MedicalCare.Presentation.ViewModels.Admin;
using Microsoft.AspNetCore.Mvc;

namespace MedicalCare.Presentation.Controllers.Admin
{
    [Route("Admin/[controller]/[action]")]
    public class TestCategoryController : Controller
    {

        private readonly CreateTestCategoryHandler _handler;
        private readonly GetAllTestCategoriesHandler _getAllHandler;
        private readonly UpdateTestCategoryHandler _updateHandler;
        private readonly ToggleTestCategoryStatusHandler _toggleHandler;

        public TestCategoryController(
            CreateTestCategoryHandler handler,
            UpdateTestCategoryHandler updateHandler,
            GetAllTestCategoriesHandler getAllHandler,
            ToggleTestCategoryStatusHandler toggleHandler)
        {
            _handler = handler;
            _getAllHandler = getAllHandler;
            _updateHandler = updateHandler;
            _toggleHandler = toggleHandler;
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
        public IActionResult Edit(Guid id, string name)
        {
            return PartialView("_CategoryModal", new TestCategoryModalVm
            {
                Id = id,
                Name = name
            });
        }
        [HttpPost]
        public async Task<IActionResult> Save(TestCategoryModalVm vm)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (vm.IsEdit)
            {
                await _updateHandler.Handle(new UpdateTestCategoryCommand {
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
