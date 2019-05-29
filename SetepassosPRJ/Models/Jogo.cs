using System;
using System.ComponentModel.DataAnnotations;
namespace SetepassosPRJ.Models
{
    public class Jogo : IComparable
    {
        
        [Required(ErrorMessage = "Por favor introduza o seu nome")]
        public string Nome { get; set; }
        public string PerfilTipo { get; set; }
        public int MoedasOuro { get; set; }
        public double PontosVida { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public bool Chave { get; set; }
        public int Sala { get; set; }
        public bool Desistiu { get; set; }
        

        public bool Monstro { get; set; }
        public bool ItemSurpresa { get; set; }
        public double PontosVidaMonstro { get; set; }
        public int PontosAtaqueMonstro { get; set; }
        public int PontosSorteMonstro { get; set; }

        public int NumFugas { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumItensEncontrados { get; set; }
        public int PocoesObtidas { get; set; }
        public int PocoesUsadas { get; set; }
        public int GameID { get; set; }
        public int Posicao { get; set; }


        public int TotalMover { get; set; }
        public int TotalAtaques { get; set; }
        public int TotalAreasExaminadas { get; set; }
        public bool Recuou { get; set; }
        public int Bonus { get; set; }
        public int MoedasOuroTotal { get; set; }
        public bool LeveiDano { get; set; }
        public bool EncontradoItem { get; set; }
        public bool EncontradoTrevo { get; set; }
        public bool EncontradoGato { get; set; }
        public bool EncontradoPocao { get; set; }
        public bool EncontradoOuro { get; set; }

        public Result ResultadoAccao { get; set; }

        public string MensagemAccao { get; set; }
        public string MensagemAccaoMonstro { get; set; }
        public string MensagemAccaoFuga { get; set; }
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

        public int BonusVitoria { get; set; }
        public int BonusChave { get; set; }
        public int BonusPocao { get; set; }
        public int BonusInimigo { get; set; }
        public int BonusItem { get; set; }
        public int BonusRecuar { get; set; }
        public int BonusLutar { get; set; }
        public int BonusVida { get; set; }

        public PlayerAction UltimaAccao { get; set; }

        
        public Jogo(string nomeEscolhido, string perfilTipoEscolhido)
        {
            Nome = nomeEscolhido;
            PerfilTipo = perfilTipoEscolhido;
            MoedasOuro = 0;
            Sala = 0;
            PocoesVida = 1;
            PocoesObtidas = 0;
            PocoesUsadas = 0;
            TotalMover = -1;
            TotalAtaques = 0;
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
            MensagemAccaoMonstro = "";
            MensagemAccaoFuga = "";
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

            
            MoedasOuro = MoedasOuro + nGS.GoldFound;
            GameID = nGS.GameID;
            Monstro = nGS.FoundEnemy;
            PontosAtaqueMonstro = nGS.EnemyAttackPoints;
            PontosSorteMonstro = nGS.EnemyLuckPoints;
            UltimaAccao = nGS.Action;
            ResultadoAccao = nGS.Result;
            LeveiDano = false;
            EncontradoItem = false;
            EncontradoTrevo = false;
            EncontradoGato = false;
            EncontradoPocao = false;
            EncontradoOuro = false;


            //Acoes com Sucesso//
            if (ResultadoAccao == Result.Success)
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
                    Recuou = true;
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
                     NumFugas = NumFugas + 1;
                    //Detetar se inimigo deu dano para meter a mensagem de acordo
                    if (nGS.EnemyDamageSuffered == 0)
                    {
                        MensagemAccaoFuga = MensagemAccaoFuga + " Escapaste por um triz! ";
                        MensagemVidaNeg = "Miss";
                    }
                    else
                    {
                        MensagemAccaoFuga = MensagemAccaoFuga + " Fugiste mas ainda te bateu. ";
                    }
                    //Detetar se na sala para onde fugimos tem inimigo
                    if (Monstro == true)
                    {
                        MensagemAccao = MensagemAccao + " Azar...Fugiste para uma sala com outro inimigo! ";
                    }
                    if (Sala < 7)
                    {
                        Sala = Sala +1;
                    }
                    else
                    {
                        if (Chave == false)
                            {
                             Sala = Sala - 1;
                            }
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
                        MensagemDano = "-" + Convert.ToString(danoDado) + " Hit";
                    }
                    else if (danoDado == 0)
                    {
                        MensagemDano = "MISS";
                    }
                    //Detetar se inimigo falhou ataque
                    if (nGS.EnemyDamageSuffered == 0)
                    {
                        MensagemAccaoMonstro = MensagemAccaoMonstro + " Uff... o gajo falhou!";
                        MensagemVidaNeg = "Miss";
                    }
                    //Mensagem ataque personalizada
                    if (PerfilTipo == "S")
                    {
                        MensagemMeuAtaque = "Mandaste uma bufa...";
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
                        MensagemAccaoMonstro = MensagemAccaoMonstro + " Mataste o inimigo!!! ";
                    }

                }

                //Examinar Area sucesso
                if (UltimaAccao == PlayerAction.SearchArea)
                {
                    TotalAreasExaminadas = TotalAreasExaminadas + 1;
                    //Detetar se apareceu monstro
                    if (Monstro == true)
                    {
                        MensagemAccaoFuga = MensagemAccaoFuga + " O inimigo estava escondido! ";
                    }
                    if (Monstro == false && nGS.FoundKey == false && nGS.FoundItem == false && nGS.GoldFound == 0)
                    {
                        MensagemAccao = MensagemAccao + " Examinaste a área mas não encontraste nada. ";
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
                    EncontradoPocao = true;
                }
                if (nGS.FoundItem == true)
                {
                    
                    EncontradoItem = true;
                    if (nGS.ItemHealthEffect > 0 && PontosVida < 5)
                    {
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SURPRESA deu-te vida! ";
                        if (PontosVida + nGS.ItemHealthEffect <= 5)
                        {
                        double VidaGanha = nGS.ItemHealthEffect;
                        MensagemVidaPos = "+" + VidaGanha;
                        PontosVida = PontosVida + VidaGanha;
                        NumItensEncontrados = NumItensEncontrados + 1;
                        }
                        else 
                        {
                            double VidaGanhaDiferenca = PontosVida + nGS.ItemHealthEffect - 5;
                            double VidaGanha = nGS.ItemHealthEffect - VidaGanhaDiferenca;
                            MensagemVidaPos = "+" + VidaGanha;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                    }
                    else if (nGS.ItemHealthEffect < 0)
                    {
                        MensagemVidaNeg = Convert.ToString(nGS.ItemHealthEffect);
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SUPRESA era leite estragado! ";
                        PontosVida = PontosVida + nGS.ItemHealthEffect;
                        NumItensEncontrados = NumItensEncontrados + 1;
                    }

                    if (nGS.ItemAttackEffect > 0 && PontosAtaque < 5)
                    {
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SUPRESA que aumentou o ataque! ";
                        if (PontosAtaque + nGS.ItemAttackEffect <= 5)
                        {
                            int AtaqueGanho = nGS.ItemAttackEffect;
                            MensagemAtaque = "+" + AtaqueGanho;
                            PontosAtaque = PontosAtaque + AtaqueGanho;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                        else
                        {
                            int AtaqueGanhoDiferenca = PontosAtaque + nGS.ItemAttackEffect - 5;
                            int AtaqueGanho = nGS.ItemAttackEffect - AtaqueGanhoDiferenca;
                            PontosAtaque = PontosAtaque + AtaqueGanho;
                            MensagemAtaque = "+" + AtaqueGanho;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                    }
                    else if (nGS.ItemAttackEffect < 0)
                    {
                        MensagemAtaque = Convert.ToString(nGS.ItemAttackEffect);
                        MensagemAccao = MensagemAccao + " Encontraste ITEM SUPRESA que diminuiu o ataque! ";
                        if (PontosAtaque + nGS.ItemAttackEffect >= 0)
                        {
                            PontosAtaque = PontosAtaque + nGS.ItemAttackEffect;
                            MensagemAtaque = Convert.ToString(nGS.ItemAttackEffect);
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                        else
                        {
                            int AtaquePerdidoDiferenca = PontosAtaque + nGS.ItemAttackEffect;
                            PontosAtaque = 0;
                            MensagemAtaque = "-" + (nGS.ItemAttackEffect - AtaquePerdidoDiferenca);
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                    }


                    if (nGS.ItemLuckEffect > 0)
                    {
                        EncontradoTrevo = true;
                        MensagemAccao = MensagemAccao + " Encontraste um trevo de 4 folhas! ";
                        if (PontosSorte + nGS.ItemLuckEffect <= 5)
                        {
                            MensagemSorte = "+" + Convert.ToString(nGS.ItemLuckEffect);
                            PontosSorte = PontosSorte + nGS.ItemLuckEffect;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                        else
                        {
                            int SorteGanhaDiferenca = PontosSorte + nGS.ItemLuckEffect - 5;
                            MensagemSorte = "+" + SorteGanhaDiferenca;
                            PontosSorte = PontosSorte + SorteGanhaDiferenca;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                    }
                    else if (nGS.ItemLuckEffect < 0)
                    {
                        MensagemAccao = MensagemAccao + " Passou um gato preto à tua frente! ";
                        EncontradoGato = true;
                        if (PontosSorte + nGS.ItemLuckEffect >= 0)
                        {
                            MensagemSorte = Convert.ToString(nGS.ItemLuckEffect);
                            PontosSorte = PontosSorte + nGS.ItemLuckEffect;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                        else
                        {
                            int SortePerdidaDiferenca = PontosSorte + nGS.ItemLuckEffect;
                            MensagemSorte = Convert.ToString(nGS.ItemLuckEffect - SortePerdidaDiferenca);
                            PontosSorte = 0;
                            NumItensEncontrados = NumItensEncontrados + 1;
                        }
                    }
                }
                //Encontrar donuts e dar ênfase se for mais de 100 donuts
                if (nGS.GoldFound != 0)
                {
                    MensagemOuro = "+" + Convert.ToString(nGS.GoldFound);
                    EncontradoOuro = true;
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
                LeveiDano = true;
                if (nGS.EnemyDamageSuffered < 2)
                {
                    MensagemAccaoMonstro = MensagemAccaoMonstro + " O inimigo acertou-te de raspão! ";
                }
                if (nGS.EnemyDamageSuffered > 1)
                {
                    MensagemAccaoMonstro = MensagemAccaoMonstro + " O inimigo acertou-te em cheio! ";
                }
            }

            //Sempre que houver combate acertar vida do monstro, fazer no fim para poder calcular dano que monstro levou.
            if ((nGS.FoundEnemy == true) || (nGS.FoundEnemy == false && UltimaAccao == PlayerAction.Attack))
                {
                    PontosVidaMonstro = nGS.EnemyHealthPoints;
                }
            
            //Se a accao for Inválida
            if (ResultadoAccao == Result.InvalidAction)
            {
                if (UltimaAccao == PlayerAction.SearchArea)
                {
                    MensagemAccao = "Essa acção não é válida. Já tinhas procurado esta sala.";
                }
                if (UltimaAccao == PlayerAction.GoForward)
                {
                    MensagemAccao = "Não podes andar para a frente.";
                }
                if (UltimaAccao == PlayerAction.GoBack)
                {
                    MensagemAccao = "Não podes andar para trás.";
                }
                if (UltimaAccao == PlayerAction.Flee)
                {
                    MensagemAccao = "Não podes fugir.";
                }
                if (UltimaAccao == PlayerAction.Attack)
                {
                    MensagemAccao = "Não podes atacar.";
                }
                if (UltimaAccao == PlayerAction.DrinkPotion)
                {
                    MensagemAccao = "Não podes beber cerveja agora.";
                }
                if (UltimaAccao == PlayerAction.Quit)
                {
                    MensagemAccao = "Não dá para desistir.";
                }
            }
            //Accao em jogo terminado
            if (ResultadoAccao == Result.GameHasEnded)
            {
                MensagemAccao = "!! Este jogo já terminou !!";
            }
            //Accao que terminou em vitória do jogo
            if (ResultadoAccao == Result.SuccessVictory)
            {
                MensagemAccao = "* * * Parabéns * * * !!! VENCESTE O JOGO !!!";
                if (UltimaAccao == PlayerAction.Flee)
                {
                    NumFugas = NumFugas + 1;
                }
            }

            PassagemTempo();

            //Calcular bonus de fim de jogo
            if (ResultadoAccao == Result.SuccessVictory || PontosVida <= 0)
            {
                CalcularBonus();
            }

        }

        
        public void PassagemTempo()
        {
            if ((TotalAreasExaminadas > 7) || (TotalAtaques > 7) || (TotalMover > 7))
            {
                PontosVida = PontosVida - 0.5;
                MensagemPassarTempo = "Cansaço: -0.5";
            }
            if (PontosVida <= 0)
            {
                MensagemAccao = " Temos pena mas morreste! Fica para a próxima...";
            }
        }

        public void CalcularBonus()
        {
            
            if (ResultadoAccao == Result.SuccessVictory)
            {
                BonusVitoria = 3000;
                if (Recuou == false)
                {
                    BonusRecuar = 400;
                }
                if (TotalAtaques == 0)
                {
                    BonusLutar = 800;
                }
                if (PontosVida < 0.5)
                {
                    BonusVida = 999;
                }
            }
            if (Chave == true)
            {
                BonusChave = 1000;
            }

            BonusPocao = PocoesVida * 750;
            BonusInimigo = NumInimigosDerrotados * 300;
            BonusItem = NumItensEncontrados * 100;

            Bonus = BonusVitoria + BonusRecuar + BonusLutar + BonusVida + BonusPocao + BonusChave + BonusInimigo + BonusItem;
            MoedasOuroTotal = MoedasOuro + Bonus;
            MensagemOuro = "Ganhaste um Bonus de " + Bonus;
        }

        public int CompareTo(object obj)
        {
            Jogo j = (Jogo)obj;

            if (MoedasOuro > j.MoedasOuro)
            {
                return 1;
            }
            else if (MoedasOuro < j.MoedasOuro)
            {
                return -1;
            }
            else
                return 0;
        }
    }
}
