using System.ComponentModel.DataAnnotations;
namespace SetepassosPRJ.Models
{
    public class Jogo
    {
      
        [Required(ErrorMessage = "Por favor introduza o seu nome")]
        public string Nome { get; set; }
        public string PerfilTipo { get; set; }
        public int MoedasOuro { get; set; }
        public double PontosVida { get; set; }
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



        public Jogo(string nomeEscolhido, string perfilTipoEscolhido)
        {
            Nome = nomeEscolhido;
            PerfilTipo = perfilTipoEscolhido;
            MoedasOuro = 0;
            Chave = false;
            Sala = 0;
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
        public void AtualizarJogo(GameStateApi nGS)
        {
           
            PontosVida = PontosVida - nGS.EnemyDamageSuffered;
            PontosVida = PontosVida + nGS.ItemHealthEffect;
            PontosVida = PontosVida - nGS.ItemAttackEffect;
            PontosSorte = PontosSorte + nGS.ItemLuckEffect;
            MoedasOuro = MoedasOuro + nGS.GoldFound;
            GameID = nGS.GameID;
            Monstro = nGS.FoundEnemy;
            if (Chave == false) { Chave = nGS.FoundKey; }
            if (nGS.FoundPotion == true)
                {
                    PocoesVida = PocoesVida + 1;
                    PocoesObtidas = PocoesObtidas + 1;
                }
                if (nGS.FoundItem == true)
                {
                    NumItensEncontrados = NumItensEncontrados + 1;
                }
                if (nGS.FoundEnemy == true)
                {
                    PontosVidaMonstro = nGS.EnemyHealthPoints;
                    PontosAtaqueMonstro = nGS.EnemyAttackPoints;
                    PontosSorteMonstro = nGS.EnemyLuckPoints;
                }

            if (nGS.Action == PlayerAction.GoForward & nGS.Result == Result.Success)
            {
                Sala = Sala + 1;
            }
            if (nGS.Action == PlayerAction.GoBack & nGS.Result == Result.Success)
            {
                Sala = Sala - 1;
            }
        }
    }
}
