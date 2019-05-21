using System;
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

        public int TotalFugas { get; set; }
        public int TotalAtaques { get; set; }
        public int TotalPocoesUsadas { get; set; }
        public int TotalAvancar { get; set; }
        public int TotalRecuar { get; set; }
        public int TotalAreasExaminadas { get; set; }


        public string MensagemAccao { get; set; }
        public string MensagemVidaPos { get; set; }
        public string MensagemVidaNeg { get; set; }
        public string MensagemAtaque { get; set; }
        public string MensagemSorte { get; set; }
        public string MensagemOuro { get; set; }
        public string MensagemPlim { get; set; }
        public string MensagemPocao { get; set; }
        public string MensagemDano { get; set; }
        public string MensagemChave { get; set; }

        public PlayerAction UltimaAccao { get; set; }


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

            TotalFugas = 0;
            TotalAtaques = 0;
            TotalPocoesUsadas = 0;
            TotalAvancar = -1;
            TotalRecuar = 0;
            TotalAreasExaminadas = 0;

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
           MensagemAccao = "";
           MensagemVidaPos = "";
           MensagemVidaNeg = "";
           MensagemAtaque = "";
           MensagemSorte = "";
           MensagemOuro = "";
           MensagemPlim = "";
           MensagemPocao = "";
           MensagemDano = "";
           MensagemChave = "";

            

            if (nGS.EnemyDamageSuffered != 0)
            {
                PontosVida = PontosVida - nGS.EnemyDamageSuffered;
                MensagemVidaNeg = "-" + Convert.ToString(nGS.EnemyDamageSuffered);
                if (nGS.EnemyDamageSuffered < 2)
                {
                    MensagemAccao = MensagemAccao + " O inimigo acertou-te de raspão! ";
                }
                if (nGS.EnemyDamageSuffered > 1)
                {
                    MensagemAccao = MensagemAccao + " O inimigo acertou-te em cheio! ";
                }
            }
            else if (nGS.EnemyDamageSuffered == 0)
            {
                if (nGS.FoundEnemy == true && Monstro == false)
                {
                    MensagemAccao = MensagemAccao + " Apareceu um inimigo! ";
                    
                }
                if (nGS.FoundEnemy == true && Monstro == true)
                {
                    MensagemAccao = MensagemAccao + " Uff... o Inimigo falhou o ataque!";
                }
                if (nGS.FoundEnemy == false && Monstro == true)
                {
                    MensagemAccao = MensagemAccao + " O inimigo não te acertou! ";
                }
            }

            if (nGS.Action == PlayerAction.SearchArea && nGS.Result != Result.InvalidAction)
            {
                TotalAreasExaminadas = TotalAreasExaminadas + 1;

                if (nGS.FoundEnemy == true && Monstro == false)
                {
                    MensagemAccao = MensagemAccao + " Mais valia estar quieto... Estava escondido! ";
                }
                
             }


            if (nGS.Action == PlayerAction.DrinkPotion && nGS.Result != Result.InvalidAction)
            {
                TotalPocoesUsadas = TotalPocoesUsadas + 1;

                if (nGS.Result == Result.Success)
                {
                    PocoesUsadas = +1;
                    PocoesVida = PocoesVida - 1;
                    MensagemAccao = MensagemAccao + " Bebeste uma imperial! ";
                    MensagemPocao = "-1";
                    if (PerfilTipo == "S")
                    {
                        MensagemVidaPos = "+" + Convert.ToString(4 - PontosVida);
                    }
                    if (PerfilTipo == "W" || PerfilTipo == "B")
                    {
                        MensagemVidaPos = "+" + Convert.ToString(3 - PontosVida);
                    }
                }
            }
            else if (nGS.Action == PlayerAction.DrinkPotion && nGS.Result == Result.InvalidAction)
            {
                if (PocoesVida < 1)
                {
                    MensagemAccao = MensagemAccao + " Não tens cerveja para beber ";
                }
            }

           // Accoes ao atacar //
            if (nGS.Action == PlayerAction.Attack && nGS.Result != Result.InvalidAction)
            {
                TotalAtaques = TotalAtaques + 1;

                if (nGS.Result == Result.Success)
                {
                    double danoDado = PontosVidaMonstro - nGS.EnemyHealthPoints;
                    if (danoDado > 0)
                    {
                        MensagemDano = "-" + Convert.ToString(danoDado);
                    }
                    else if (danoDado == 0 )
                    {
                        MensagemDano = "Miss";
                    }
                    
                }
                // Detetar Monstro Morre //
                if (nGS.FoundEnemy == false)
                {
                    NumInimigosDerrotados = NumInimigosDerrotados + 1;
                    MensagemAccao = MensagemAccao + " Derrotaste o inimigo. ";
                }
            }

            PontosVida = PontosVida + nGS.ItemHealthEffect;
            PontosAtaque = PontosAtaque + nGS.ItemAttackEffect;
            PontosSorte = PontosSorte + nGS.ItemLuckEffect;
            MoedasOuro = MoedasOuro + nGS.GoldFound;
            GameID = nGS.GameID;
            Monstro = nGS.FoundEnemy;
            PontosAtaqueMonstro = nGS.EnemyAttackPoints;
            PontosVidaMonstro = nGS.EnemyHealthPoints;
            PontosSorteMonstro = nGS.EnemyLuckPoints;


            if (Chave == false)
            {
                if (nGS.FoundKey == true)
                {
                    Chave = nGS.FoundKey;
                    MensagemChave = " Found it! ";
                    MensagemAccao = " Encontraste a Chave! ";
                }
            }
                
            if (nGS.FoundPotion == true)
                {
                    PocoesVida = PocoesVida + 1;
                    PocoesObtidas = PocoesObtidas + 1;
                    MensagemAccao = MensagemAccao + " Encontraste uma cerveja! ";
                    MensagemPocao = "+1";
            }
            if (nGS.FoundItem == true)
                {
                    NumItensEncontrados = NumItensEncontrados + 1;
                if (nGS.ItemHealthEffect != 0)
                  {
                    MensagemAccao = MensagemAccao + " Encontraste um item surpresa que te afetou a vida! ";
                    if (nGS.ItemHealthEffect > 0)
                    {
                        MensagemVidaPos = "+" + Convert.ToString(nGS.ItemHealthEffect);
                    }
                    else if (nGS.ItemHealthEffect < 0)
                    {
                        MensagemVidaNeg = Convert.ToString(nGS.ItemHealthEffect);
                    }
                }
                if (nGS.ItemAttackEffect != 0)
                  {
                    MensagemAccao = MensagemAccao + " Encontraste um item surpresa que te afetou o ataque! ";
                    if (nGS.ItemAttackEffect > 0)
                    {
                        MensagemAtaque = "+" + Convert.ToString(nGS.ItemAttackEffect);
                    }
                    else if (nGS.ItemAttackEffect < 0)
                    {
                        MensagemAtaque = Convert.ToString(nGS.ItemAttackEffect);
                    }
                }
                if (nGS.ItemLuckEffect != 0)
                  {
                    MensagemAccao = MensagemAccao + " Encontraste um item surpresa que te afetou a sorte! ";
                    if (nGS.ItemLuckEffect > 0)
                    {
                        MensagemSorte = "+" + Convert.ToString(nGS.ItemLuckEffect);
                    }
                    else if (nGS.ItemLuckEffect < 0)
                    {
                        MensagemSorte = Convert.ToString(nGS.ItemLuckEffect);
                    }
                  }
                }
            if (nGS.GoldFound != 0)
                if (nGS.GoldFound > 100)
            {
                {
                    MensagemPlim = "PLiM pLiM PlIM...";
                    MensagemOuro = "+" + Convert.ToString(nGS.GoldFound);
                }
            }
            if (nGS.FoundEnemy == true)
                {
                    PontosVidaMonstro = nGS.EnemyHealthPoints;
                    PontosAtaqueMonstro = nGS.EnemyAttackPoints;
                    PontosSorteMonstro = nGS.EnemyLuckPoints;
                }



            if (nGS.Action == PlayerAction.GoForward && nGS.Result != Result.InvalidAction)
            {
                TotalAvancar = TotalAvancar + 1;

                if (nGS.Result == Result.Success)
                {
                    Sala = Sala + 1;
                }
            }
            if (nGS.Action == PlayerAction.GoBack && nGS.Result != Result.InvalidAction)
            {
                TotalRecuar = TotalRecuar + 1;

                if (nGS.Result == Result.Success)
                {
                    Sala = Sala - 1;
                }
            }
            if (nGS.Action == PlayerAction.Flee && nGS.Result != Result.InvalidAction)
            {
                TotalFugas = TotalFugas + 1;

                if (nGS.Result == Result.Success)
                {
                    Sala = Sala + 1;
                    NumFugas = NumFugas + 1;
                }
            }

            UltimaAccao = nGS.Action;
            PassagemTempo();

        }
        public void PassagemTempo()
        {
            if ((UltimaAccao == PlayerAction.SearchArea && TotalAreasExaminadas > 7) || (UltimaAccao == PlayerAction.Attack && TotalAtaques > 7) || (UltimaAccao == PlayerAction.GoForward && TotalAvancar > 7) || (UltimaAccao == PlayerAction.Flee && TotalFugas > 7) || (UltimaAccao == PlayerAction.DrinkPotion && TotalPocoesUsadas > 7) || (UltimaAccao == PlayerAction.GoBack && TotalRecuar > 7))
            {
                PontosVida = PontosVida - 0.5;
            }
            if (PontosVida <= 0)
            {
                MensagemAccao = "Temos pena mas morreste! Fica para a próxima...";
            }
        }
    }
}
