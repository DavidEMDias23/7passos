using System.ComponentModel.DataAnnotations;
namespace SetepassosPRJ.Models
{
    public class Jogo
    {
      
        [Required(ErrorMessage = "Por favor introduza o seu nome")]
        public string Nome { get; set; }
        public string PerfilTipo { get; set; }
        public int MoedasOuro { get; set; }
        public int PontosVida { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public bool Pocao { get; set; }
        public bool Chave { get; set; }
        public int Sala { get; set; }
        public bool Monstro { get; set; }
        public bool ItemSurpresa { get; set; }
        public double PontosVidaMonstro { get; set; }
        public int PontosAtaqueMonstro { get; set; }
        public int PontosSorteMonstro { get; set; }
        public bool ResultadoFinal { get; set; }
        public int NumFugas { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumAreasInvestigadas { get; set; }
        public int NumItensEncontrados { get; set; }
        public int PocoesObtidas { get; set; }
        public int PocoesUsadas { get; set; }
        public int GameID { get; set; }
        public PlayerAction PlayerAction { get; set; }

        public void AtualizarJogo(GameStateResponse gameState)
        {
            PlayerAction = gameState.Action;         
            Monstro = gameState.FoundEnemy;
            ItemSurpresa = gameState.FoundItem;
            Chave = gameState.FoundKey;
            MoedasOuro = gameState.GoldFound;
            Pocao = gameState.FoundPotion;
        }

        public Jogo(string nomeEscolhido, string perfilTipoEscolhido)
        {
            Nome = nomeEscolhido;
            PerfilTipo = perfilTipoEscolhido;
            MoedasOuro = 0;
            Chave = true;
            Sala = 1;
            Monstro = false;
            ItemSurpresa = false;
            PocoesVida = 1;
            Pocao = false;

            if (perfilTipoEscolhido == "S")
            {
                           
                PontosVida = 4;
                PontosAtaque = 3;
                PontosSorte = 2;
                
                

            }

            if (perfilTipoEscolhido == "W")
            {
               
                PontosVida = 3;
                PontosAtaque = 3;
                PontosSorte = 3;
            

            }

            if (perfilTipoEscolhido == "B")
            {
                
                PontosVida = 3;
                PontosAtaque = 2;
                PontosSorte = 4;

            }

        }
    }
}
