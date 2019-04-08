using System.ComponentModel.DataAnnotations;
namespace SetepassosPRJ.Models
{
    public class Perfil
    {
        [Required(ErrorMessage = "Por favor introduza o seu nome")]
        public string Nome { get; set; }
        public string PerfilTipo { get; set; }
        public int MoedasOuro { get; set; }
        public int PontosVida { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public bool Chave { get; set; }

        
    }
}
