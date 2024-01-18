using System.ComponentModel.DataAnnotations;

namespace JofApp.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string Name { get; set; }


        [Required]
        [MinLength(6)]
        [MaxLength(25)]
        public string Surname { get; set; }


        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string UserName { get; set; }


        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        [Required]
        [MinLength(6)]
        [MaxLength(15)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
