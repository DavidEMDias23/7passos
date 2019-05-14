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
        public int Sala { get; set; }
        public bool Monstro { get; set; }
        public bool ItemSurpresa { get; set; }

        public Perfil(string nomeEscolhido, string perfilTipoEscolhido)
        {

            MoedasOuro = 0;
            Chave = false;
            Sala = 1;
            Monstro = true;
            ItemSurpresa = false;

            if (perfilTipoEscolhido == "Nerd")
            {
                Nome = nomeEscolhido;
                PerfilTipo = perfilTipoEscolhido;              
                PontosVida = 4;
                PontosAtaque = 3;
                PontosSorte = 2;
                PocoesVida = 1;
                

            }

            if (perfilTipoEscolhido == "Fat")
            {
                Nome = nomeEscolhido;
                PerfilTipo = perfilTipoEscolhido;
                PontosVida = 3;
                PontosAtaque = 3;
                PontosSorte = 3;
                PocoesVida = 1;

            }

            if (perfilTipoEscolhido == "Old")
            {
                Nome = nomeEscolhido;
                PerfilTipo = perfilTipoEscolhido;
                PontosVida = 3;
                PontosAtaque = 2;
                PontosSorte = 4;
                PocoesVida = 1;

            }

        }
    }
}
