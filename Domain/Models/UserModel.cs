namespace GerenciadorCursos.DomainCore.Models
{
    public enum Role
    { Aluno, Secretaria, Gerencia };

    public class UserModel
    {
        public UserModel(string username, string password, Role role)
        {
            Username = username;
            Password = password;
            Role = role;
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}