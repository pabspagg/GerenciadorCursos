using System.ComponentModel.DataAnnotations;

namespace GerenciadorCursos.DomainCore.Models
{
    public enum Status
    { Previsto, EmAndamento, Concluido };

    public class CursoModel
    {
        public CursoModel(string titulo, int duracao, Status status)
        {
            Titulo = titulo;
            Duracao = duracao;
            Status = status;
        }

        public CursoModel() { }

        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public int Duracao { get; set; }

        public Status Status { get; set; }
    }
}