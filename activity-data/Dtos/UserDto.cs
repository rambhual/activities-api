using System.ComponentModel.DataAnnotations;

namespace activity_data.Dtos
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage="You must enter password")]
        public string Password { get; set; }
    }
}