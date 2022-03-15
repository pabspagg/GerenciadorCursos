namespace GerenciadorCursos.DomainCore.Models
{
    public class UserModelDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
