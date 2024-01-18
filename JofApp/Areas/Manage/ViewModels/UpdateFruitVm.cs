namespace JofApp.Areas.Manage.ViewModels
{
    public class UpdateFruitVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public IFormFile? File { get; set; }
    }
}
