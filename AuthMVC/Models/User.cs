using System.ComponentModel.DataAnnotations;

namespace AuthMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Enter Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; }    
    }
}
