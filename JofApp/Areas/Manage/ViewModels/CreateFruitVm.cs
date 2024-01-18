using System.ComponentModel.DataAnnotations;

namespace JofApp.Areas.Manage.ViewModels
{
    public class CreateFruitVm
    {

        public string Name { get; set; }
        public string Category { get; set; }
        public IFormFile File { get; set; }
    }
}
