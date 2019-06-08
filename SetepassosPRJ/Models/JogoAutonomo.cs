using System;
using System.ComponentModel.DataAnnotations;
namespace SetepassosPRJ.Models
{
    public class JogoAutonomo : Jogo
    {

        public PlayerAction TomarAccao { get; set; }
        public int Rondas { get; set; }

        public JogoAutonomo(string nomeEscolhido) : base(nomeEscolhido, "S")
        {
            if (Nome == "Auto3")
            {
                Rondas = 3;
            }
            if (Nome == "Auto7")
            {
                Rondas = 7;
            }
            if (Nome == "Auto0")
            {
                Rondas = 40;
            }

        }

        public void AtualizarJogoAutonomo(GameStateApi nGS)
        {

            MoedasOuro = MoedasOuro + nGS.GoldFound;
            GameID = nGS.GameID;
            Monstro = nGS.FoundEnemy;
            PontosAtaqueMonstro = nGS.EnemyAttackPoints;
            PontosSorteMonstro = nGS.EnemyLuckPoints;
            DanoSofrido = nGS.EnemyDamageSuffered;
            UltimaAccao = nGS.Action;
            ResultadoAccao = nGS.Result;
            LeveiDano = false;
            EncontradoItem = false;
            EncontradoChave = false;
            EncontradoTrevo = false;
            EncontradoGato = false;
            EncontradoPocao = false;
            EncontradoOuro = false;
            TomarAccao = PlayerAction.Null;
            


            //Acoes com Sucesso//
            if (ResultadoAccao == Result.Success)
            {
                //Movimento sucesso
                if (UltimaAccao == PlayerAction.GoForward)
                {
                    TotalMover = TotalMover + 1;
                    Sala = Sala + 1;

                }
                if (UltimaAccao == PlayerAction.GoBack)
                {
                    TotalMover = TotalMover + 1;
                    Recuou = true;
                    Sala = Sala - 1;
                    //Detetar se apareceu monstro
                }
                if (UltimaAccao == PlayerAction.Flee)
                {
                    TotalMover = TotalMover + 1;
                    NumFugas = NumFugas + 1;
                    if (Sala < 7)
                    {
                        Sala = Sala + 1;
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
                    //Detetar se monstro morre
                    if (Monstro == false)
                    {
                        NumInimigosDerrotados = NumInimigosDerrotados + 1;
                    }

                }

                //Examinar Area sucesso
                if (UltimaAccao == PlayerAction.SearchArea)
                {
                    TotalAreasExaminadas = TotalAreasExaminadas + 1;
                    arraySalasExaminadas[Sala] = true;
                }


                //Beber Pocao Sucesso
                if (UltimaAccao == PlayerAction.DrinkPotion)
                {
                    PocoesUsadas = +1;
                    PocoesVida = PocoesVida - 1;
                    if (PontosVida < 3)
                    {
                        PontosVida = 3;
                    }
                }
            }

            //Encontrar Items
            if (Chave == false)
            {
                if (nGS.FoundKey == true)
                {
                    Chave = nGS.FoundKey;
                    EncontradoChave = true;
                }
            }
            if (nGS.FoundPotion == true)
            {
                PocoesObtidas = PocoesObtidas + 1;
                EncontradoPocao = true;
                if (PocoesVida < 3)
                {
                    PocoesVida = PocoesVida + 1;
                }
            }
            if (nGS.FoundItem == true)
            {

                EncontradoItem = true;
                NumItensEncontrados = NumItensEncontrados + 1;
                if (nGS.ItemHealthEffect > 0 && PontosVida < 5)
                {

                    if (PontosVida + nGS.ItemHealthEffect <= 5)
                    {
                        PontosVida = PontosVida + nGS.ItemHealthEffect;
                    }
                    else
                    {
                        PontosVida = 5;

                    }
                }
                else if (nGS.ItemHealthEffect < 0)
                {
                    PontosVida = PontosVida + nGS.ItemHealthEffect;
                }

                if (nGS.ItemAttackEffect > 0 && PontosAtaque < 5)
                {

                    if (PontosAtaque + nGS.ItemAttackEffect <= 5)
                    {
                        PontosAtaque = PontosAtaque + nGS.ItemAttackEffect;
                    }
                    else
                    {
                        PontosAtaque = 5;

                    }
                }
                else if (nGS.ItemAttackEffect < 0)
                {

                    if (PontosAtaque + nGS.ItemAttackEffect > 0)
                    {
                        PontosAtaque = PontosAtaque + nGS.ItemAttackEffect;
                    }
                    else
                    {
                        PontosAtaque = 0;
                    }
                }


                if (nGS.ItemLuckEffect > 0)
                {
                    if (PontosSorte + nGS.ItemLuckEffect <= 5)
                    {

                        PontosSorte = PontosSorte + nGS.ItemLuckEffect;
                    }
                    else
                    {
                        PontosSorte = 5;
                    }
                }
                else if (nGS.ItemLuckEffect < 0)
                {
                    if (PontosSorte + nGS.ItemLuckEffect >= 0)
                    {
                        PontosSorte = PontosSorte + nGS.ItemLuckEffect;
                    }
                    else
                    {
                        PontosSorte = 0;
                    }
                }
                //Fazer acerto de vida quando levamos dano de inimigo
                if (DanoSofrido != 0)
                {
                    PontosVida = Math.Round(PontosVida - DanoSofrido, 1);
                }

                //Sempre que houver combate acertar vida do monstro, fazer no fim para poder calcular dano que monstro levou.
                if (UltimaAccao == PlayerAction.Attack)
                {
                    PontosVidaMonstro = Math.Round(nGS.EnemyHealthPoints, 1);
                }


                //Detetar se existe monstro na view
                if (Monstro == true)
                {
                    if (PontosSorteMonstro < PontosSorte)
                    {
                        if (PontosAtaqueMonstro < 4)
                        {
                            if (PontosVida > 1)
                            {
                                TomarAccao = PlayerAction.Attack;
                            }
                            else
                            {
                                if (PocoesVida > 0)
                                {
                                    TomarAccao = PlayerAction.DrinkPotion;
                                }
                                else
                                {
                                    TomarAccao = PlayerAction.Flee;
                                }
                            }
                        }
                        else
                        {
                            TomarAccao = PlayerAction.Flee;
                        }
                    }
                    else
                    {
                        if (PontosAtaqueMonstro < 3)
                        {
                            if (PontosVida > 1.5)
                            {
                                TomarAccao = PlayerAction.Attack;
                            }
                            else
                            {
                                if (PocoesVida > 0)
                                {
                                    TomarAccao = PlayerAction.DrinkPotion;
                                }
                                else
                                {
                                    TomarAccao = PlayerAction.Flee;
                                }
                            }
                        }
                        else
                        {
                            TomarAccao = PlayerAction.Flee;
                        }
                    }
                }
                else
                {
                    if (Chave == true)
                    {
                        TomarAccao = PlayerAction.GoForward;
                    }
                    else
                    {
                        if (arraySalasExaminadas[Sala] == true)
                        {
                            if (Sala < 7)
                            {
                                TomarAccao = PlayerAction.GoForward;
                            }
                            else
                            {
                                TomarAccao = PlayerAction.Quit;
                            }
                        }
                        else
                        {
                            TomarAccao = PlayerAction.SearchArea;
                        }
                    }

                }
            }

        


            //Accao em jogo terminado
            if (ResultadoAccao == Result.GameHasEnded)
            {
                ResultadoAccao = Result.GameHasEnded;
                Terminado = true;
            }

            PassagemTempo();

            //Calcular bonus de fim de jogo
            if (ResultadoAccao == Result.SuccessVictory)
            {
                Terminado = true;
                ResultadoJogo = ResultadoJogo.Vitoria;
                CalcularBonus();
                if (UltimaAccao == PlayerAction.Flee)
                {
                    NumFugas = NumFugas + 1;
                }
            }

            else if (ResultadoAccao != Result.SuccessVictory && PontosVida <= 0)
            {
                ResultadoJogo = ResultadoJogo.Derrota;
                Terminado = true;
                CalcularBonus();
            }


        }


        public override void PassagemTempo()
        {
            if ((TotalAreasExaminadas > 7) || (TotalAtaques > 7) || (TotalMover > 7))
            {
                PontosVida = PontosVida - 0.5;

            }
            if (PontosVida <= 0 && ResultadoAccao != Result.SuccessVictory)
            {

            }
            if (PontosVida <= 0 && ResultadoAccao == Result.SuccessVictory)
            {
                PontosVida = PontosVida + 0.5;
            }
        }

        public override void CalcularBonus()
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

        }



        public override int CompareTo(object obj)
        {
            JogoAutonomo j = (JogoAutonomo)obj;

            if (MoedasOuroTotal > j.MoedasOuroTotal)
            {
                return 1;
            }
            else if (MoedasOuroTotal < j.MoedasOuroTotal)
            {
                return -1;
            }
            else
                return 0;
        }
    }
}
