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

        public int TotalMover { get; set; }
        public int TotalAtaques { get; set; }
        public int TotalPocoesUsadas { get; set; }
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
        public string MensagemPassarTempo { get; set; }
        public string MensagemMeuAtaque { get; set; }

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

            TotalMover = -1;
            TotalAtaques = 0;
            TotalPocoesUsadas = 0;
            TotalAreasExaminadas = 0;

            if (perfilTipoEscolhido == "S")
            {
                           
                PontosVida = 3;
                PontosAtaque = 3;
                PontosSorte = 3;
                  

            }

            if (perfilTipoEscolhido == "W")
            {
               
                PontosVida = 3;
                PontosAtaque = 2;
                PontosSorte = 4;
            

            }

            if (perfilTipoEscolhido == "B")
            {
                
                PontosVida = 4;
                PontosAtaque = 3;
                PontosSorte = 2;

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
            MensagemMeuAtaque = "";
            MensagemPassarTempo = "";

            PontosVida = PontosVida + nGS.ItemHealthEffect;
            PontosAtaque = PontosAtaque + nGS.ItemAttackEffect;
            PontosSorte = PontosSorte + nGS.ItemLuckEffect;
            MoedasOuro = MoedasOuro + nGS.GoldFound;
            GameID = nGS.GameID;
            Monstro = nGS.FoundEnemy;
            PontosAtaqueMonstro = nGS.EnemyAttackPoints;
            PontosSorteMonstro = nGS.EnemyLuckPoints;
            UltimaAccao = nGS.Action;

            //Acoes com Sucesso//
            if (nGS.Result == Result.Success)
            {
                //Movimento sucesso
                if (UltimaAccao == PlayerAction.GoForward)
                {
                    TotalMover = TotalMover + 1;
                    Sala = Sala + 1;

                    //Detetar se apareceu monstro
                    if (Monstro == true)
                    {
                        MensagemAccao = MensagemAccao + " Esta sala tem um inimigo! ";
                    }
                }
                if (UltimaAccao == PlayerAction.GoBack)
                {
                    TotalMover = TotalMover +1;
                    Sala = Sala - 1;
                    //Detetar se apareceu monstro
                    if (Monstro == true)
                    {
                        MensagemAccao = MensagemAccao + " Deste de caras com um inimigo! ";
                    }
                }
                if (UltimaAccao == PlayerAction.Flee)
                {
                    TotalMover = TotalMover + 1;
                    Sala = Sala +1;
                    //Detetar se inimigo deu dano para meter a mensagem de acordo
                    if (nGS.EnemyDamageSuffered == 0)
                    {
                        MensagemAccao = MensagemAccao + " Escapaste por um triz! ";
                        MensagemVidaNeg = "Miss";
                    }
                    else
                    {
                        MensagemAccao = MensagemAccao + " Fugiste mas ainda te bateu. ";
                    }
                    //Detetar se na sala para onde fugimos tem inimigo
                    if (Monstro == true)
                    {
                        MensagemAccao = MensagemAccao + " Azar... Nesta sala há outro inimigo! ";
                    }

                }

                //Atacar sucesso
                if (UltimaAccao == PlayerAction.Attack)
                {
                    TotalAtaques = TotalAtaques + 1;
                    double danoDado = PontosVidaMonstro - nGS.EnemyHealthPoints;

                    //Ataque acertou ou falhou
                    if (danoDado > 0)
                    {
                        MensagemDano = "-" + Convert.ToString(danoDado);
                    }
                    else if (danoDado == 0)
                    {
                        MensagemDano = "Miss";
                    }
                    //Detetar se inimigo falhou ataque
                    if (nGS.EnemyDamageSuffered == 0)
                    {
                        MensagemAccao = MensagemAccao + " Uff... o gajo falhou!";
                        MensagemVidaNeg = "Miss";
                    }
                    //Mensagem ataque personalizada
                    if (PerfilTipo == "S")
                    {
                        MensagemMeuAtaque = "Mandaste um peido...";
                    }
                    if (PerfilTipo == "B")
                    {
                        MensagemMeuAtaque = "Mandaste-lhe com a calculadora.";
                    }
                    if (PerfilTipo == "W")
                    {
                        MensagemMeuAtaque = "Atiraste um gato!";
                    }
                    //Detetar se monstro morre
                    if (Monstro == false)
                    {
                        NumInimigosDerrotados = NumInimigosDerrotados + 1;
                        MensagemAccao = MensagemAccao + " Mataste o inimigo!!! ";
                    }

                }

                //Examinar Area sucesso
                if (UltimaAccao == PlayerAction.SearchArea)
                {
                    TotalAreasExaminadas = TotalAreasExaminadas + 1;
                    //Detetar se apareceu monstro
                    if (Monstro == true)
                    {
                        MensagemAccao = MensagemAccao + " O inimigo estava escondido! ";
                    }
                }

                //Beber Pocao Sucesso
                if (UltimaAccao == PlayerAction.DrinkPotion)
                {
                    PocoesUsadas = +1;
                    PocoesVida = PocoesVida - 1;
                    MensagemPocao = "-1";
                    if (PerfilTipo == "B")
                    {
                        if (PontosVida < 4)
                        {
                            MensagemVidaPos = "+" + Convert.ToString(4 - PontosVida);
                            MensagemAccao = " Bebeste uma imperial! ";
                            PontosVida = 4;
                        }
                        else
                        {
                            MensagemAccao = " Bêbado! A tua vida já estava cheia... ";
                        }
                    }
                    if (PerfilTipo == "W" || PerfilTipo == "S")
                    {
                        if (PontosVida < 3)
                        {
                            MensagemVidaPos = "+" + Convert.ToString(3 - PontosVida);
                            MensagemAccao = " Bebeste uma imperial! ";
                            PontosVida = 3;
                        }
                        else
                        {
                            MensagemAccao = " Bêbado! A tua vida já estava cheia... ";
                        }
                    }
                }

                //Encontrar Items
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
                    if (nGS.ItemHealthEffect > 0)
                    {
                        MensagemVidaPos = "+" + Convert.ToString(nGS.ItemHealthEffect);
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SURPRESA deu-te vida! ";
                    }
                    else if (nGS.ItemHealthEffect < 0)
                    {
                        MensagemVidaNeg = Convert.ToString(nGS.ItemHealthEffect);
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SUPRESA era leite estragado! ";
                    }

                    if (nGS.ItemAttackEffect > 0)
                    {
                        MensagemAtaque = "+" + Convert.ToString(nGS.ItemAttackEffect);
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SUPRESA que aumentou o ataque! ";
                    }
                    else if (nGS.ItemAttackEffect < 0)
                    {
                        MensagemAtaque = Convert.ToString(nGS.ItemAttackEffect);
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SUPRESA que diminuiu o ataque! ";
                    }


                    if (nGS.ItemLuckEffect > 0)
                    {
                        MensagemSorte = "+" + Convert.ToString(nGS.ItemLuckEffect);
                        MensagemAccao = MensagemAccao + " Encontraste um trevo de 4 folhas! ";
                    }
                    else if (nGS.ItemLuckEffect < 0)
                    {
                        MensagemSorte = Convert.ToString(nGS.ItemLuckEffect);
                        MensagemAccao = MensagemAccao + " Passou um gato preto à tua frente! ";
                    }
                }
                //Encontrar donuts e dar ênfase se for mais de 100 donuts
                if (nGS.GoldFound != 0)
                {
                    MensagemOuro = "+" + Convert.ToString(nGS.GoldFound);
                    if (nGS.GoldFound > 100)
                       {    
                        MensagemPlim = "Nham Nham Nham...";                  
                       }
                }

            }

            //Fazer acerto de vida quando levamos dano de inimigo
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

            //Sempre que existir inimigo acertar vida do monstro, fazer no fim para poder calcular dano que monstro levou.
            if (nGS.FoundEnemy == true)
                {
                    PontosVidaMonstro = nGS.EnemyHealthPoints;
                }
            
             PassagemTempo();

        }
        public void PassagemTempo()
        {
            if ((TotalAreasExaminadas > 7) || (TotalAtaques > 7) || (TotalMover > 7))
            {
                PontosVida = PontosVida - 0.5;
                MensagemPassarTempo = "Cançaço: -0.5";
            }
            if (PontosVida <= 0)
            {
                MensagemAccao = " Temos pena mas morreste! Fica para a próxima...";
            }
        }
    }
}
