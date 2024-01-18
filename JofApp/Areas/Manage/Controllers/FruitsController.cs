using JofApp.Areas.Manage.ViewModels;
using JofApp.DAL;
using JofApp.Helpers;
using JofApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JofApp.Areas.Manage.Controllers
{
    [AutoValidateAntiforgeryToken]
    [Authorize(Roles ="Admin")]
    [Area("Manage")]
    public class FruitsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public FruitsController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            var fruits = await _context.Fruits.ToListAsync();
            return View(fruits);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFruitVm createFruitVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Fruits newFruit = new Fruits()
            {
                Name = createFruitVm.Name,
                Category = createFruitVm.Category,
                ImgUrl = createFruitVm.File.Upload(_environment.WebRootPath, @"/Upload/")
            };

            var result = await _context.Fruits.AddAsync(newFruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Update(int id)
        {

            Fruits fruit = await _context.Fruits.FindAsync(id);
            UpdateFruitVm updateFruitVm = new UpdateFruitVm()
            {
                Id = fruit.Id,
                Name = fruit.Name,
                Category= fruit.Category               
            };
            return View(updateFruitVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateFruitVm updateFruitVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var oldFruit = await _context.Fruits.FirstOrDefaultAsync(c => c.Id == updateFruitVm.Id);
            if (updateFruitVm.File != null)
            {
                oldFruit.ImgUrl.DeleteFile(_environment.WebRootPath, @"/Upload/");
                oldFruit.ImgUrl=updateFruitVm.File.Upload(_environment.WebRootPath, @"/Upload/");
            }

            oldFruit.Name = updateFruitVm.Name;
            oldFruit.Category = updateFruitVm.Category;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var oldFruit = _context.Fruits.FirstOrDefault(c => c.Id == id);
            _context.Fruits.Remove(oldFruit);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
