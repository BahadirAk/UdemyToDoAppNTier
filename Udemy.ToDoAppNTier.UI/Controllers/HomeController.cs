using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Udemy.ToDoAppNTier.Business.Interfaces;
using Udemy.ToDoAppNTier.Common.ResponseObjects;
using Udemy.ToDoAppNTier.Dtos.WorkDtos;
using Udemy.ToDoAppNTier.UI.Extensions;

namespace Udemy.ToDoAppNTier.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWorkService _workService;

        public HomeController(IWorkService workService)
        {
            _workService = workService;
        }

        public async Task<IActionResult> Index()
        {
            var workList = await _workService.GetAll();
            return View(workList.Data);
        }
        public IActionResult Create()
        {
            return View(new WorkCreateDto());
        }
        [HttpPost]
        public async Task<IActionResult> Create(WorkCreateDto dto)
        {
            var response = await _workService.Create(dto);
            return this.ResponseRedirectToAction(response, "Index");

            //if (response.ResponseType == ResponseType.ValidationError)
            //{
            //    foreach (var error in response.ValidationErrors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return View(dto);
            //}
            //else
            //{
            //    return RedirectToAction("Index");
            //}
        }
        public async Task<IActionResult> Update(int id)
        {
            //var work = await _workService.GetById<WorkUpdateDto>(id);
            //return View(new WorkUpdateDto
            //{
            //    Id = work.Id,
            //    Definition = work.Definition,
            //    IsCompleted = work.IsCompleted
            //});

            var response = await _workService.GetById<WorkUpdateDto>(id);
            return this.ResponseView(response);

            //if (response.ResponseType == ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(WorkUpdateDto dto)
        {
            var response = await _workService.Update(dto);
            return this.ResponseRedirectToAction(response, "Index");

            //if (response.ResponseType == ResponseType.ValidationError)
            //{
            //    foreach (var error in response.ValidationErrors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return View(dto);
            //}
            //return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            var response = await _workService.Remove(id);
            return this.ResponseRedirectToAction(response, "Index");

            //if (response.ResponseType == ResponseType.NotFound)
            //{
            //    return NotFound();
            //}
            //return RedirectToAction("Index");
        }
        public IActionResult NotFound(int code)
        {
            return View();
        }
    }
}
