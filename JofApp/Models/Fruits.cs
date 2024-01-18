using JofApp.Models.Common;

namespace JofApp.Models
{
    public class Fruits:BaseEntity
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string? ImgUrl { get; set; }
    }
}
