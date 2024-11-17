using _7Assist.Models;
using BCrypt.Net;

namespace _7Assist.Data
{
    public static class InitialData
    {
        public static List<User> UsersList { get; set; } = new List<User>()
        {
            new User {IdUser=1, Login="arhterminal", Password=BCrypt.Net.BCrypt.HashPassword("1")},
            new User {IdUser=2, Login="ekbterminal", Password=BCrypt.Net.BCrypt.HashPassword("2")},
            new User {IdUser=3, Login="Ivan", Password=BCrypt.Net.BCrypt.HashPassword("3")}
        };
        public static List<Terminal> TerminalsList { get; set; } = new List<Terminal>()
        {
           new Terminal{IdUser=1, Address = "г. Архангельск"},
           new Terminal{IdUser=2, Address = "г. Екатеринбург"}
        };
        public static List<Admin> AdminsList { get; set; } = new List<Admin>()
        {
           new Admin{IdUser=3, Name = "Иван", Surname = "Иванов", Patronymic = "Иванович" }
        };
    }
}
