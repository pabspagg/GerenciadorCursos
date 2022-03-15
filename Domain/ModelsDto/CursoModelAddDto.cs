
namespace GerenciadorCursos.DomainCore.Models
{
    public class CursoModelAddDto
    {
        public string Titulo { get; set; }
        public int Duracao { get; set; }
        public Status Status { get; set; }
    }
}