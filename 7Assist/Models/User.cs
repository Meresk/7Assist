using System.ComponentModel.DataAnnotations;

namespace _7Assist.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(155)]
        public string Name { get; set; }

        [Required, StringLength(155)]
        public string Surname { get; set; }

        [Required, StringLength(155)]
        public string Patronymic { get; set; }

        [Required, StringLength(155)]
        public string Login { get; set; }

        [Required, StringLength(255)]
        public string Password { get; set; }
    }
}
