using System;
using System.ComponentModel.DataAnnotations;
namespace SetepassosPRJ.Models
{
    public enum ResultadoJogo { Derrota, Desistiu, Vitoria }
    public class Jogo : IComparable
    {
        [Required(ErrorMessage = "Por favor introduza o seu nome")]
        public string Nome { get; set; }
        public string PerfilTipo { get; set; }
        public int MoedasOuro { get; set; }
        public int MoedasOuroRecebidas { get; set; } // Moedas ouro recebidas pelo GameState
        public double PontosVida { get; set; }
        public double PontosVidaMax { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public bool Chave { get; set; }
        public int Sala { get; set; }
        public bool Desistiu { get; set; }
        public bool Terminado { get; set; }
        public int Rondas { get; set; }
        public int RondaAtual { get; set; }

        public bool Monstro { get; set; }
        public bool ItemSurpresa { get; set; }
        public double PontosVidaMonstro { get; set; }
        public double PontosVidaMonstroAtuais { get; set; }
        public int PontosAtaqueMonstro { get; set; }
        public int PontosSorteMonstro { get; set; }

        public int NumFugas { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumItensEncontrados { get; set; }
        public int PocoesObtidas { get; set; }
        public int PocoesUsadas { get; set; }
        public int GameID { get; set; }

        public int TotalMover { get; set; }
        public int TotalAtaques { get; set; }
        public int TotalAreasExaminadas { get; set; }
        public bool Recuou { get; set; }
        public int Bonus { get; set; }
        public int MoedasOuroTotal { get; set; }
        public bool EncontradoItem { get; set; }
        public bool EncontradoTrevo { get; set; }
        public bool EncontradoGato { get; set; }
        public bool EncontradoPocao { get; set; }
        public bool EncontradoChave { get; set; }
        public double DanoSofrido { get; set; }
        public int EfeitoVidaItem { get; set; }
        public int EfeitoAtaqueItem { get; set; }
        public int EfeitoSorteItem { get; set; }

        public Result ResultadoAccao { get; set; }
        public PlayerAction TomarAccao { get; set; }

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
        public string MensagemSalasExaminadas { get; set; }

        public int BonusVitoria { get; set; }
        public int BonusChave { get; set; }
        public int BonusPocao { get; set; }
        public int BonusInimigo { get; set; }
        public int BonusItem { get; set; }
        public int BonusRecuar { get; set; }
        public int BonusLutar { get; set; }
        public int BonusVida { get; set; }

        public bool Autonomo { get; set; }


        public bool[] arraySalasExaminadas = new bool[8];

        public PlayerAction UltimaAccao { get; set; }

        public ResultadoJogo ResultadoJogo { get; set; }



        public Jogo(string nomeEscolhido, string perfilTipoEscolhido, bool modoEscolhido)
        {
            Nome = nomeEscolhido;
            PerfilTipo = perfilTipoEscolhido;
            Autonomo = modoEscolhido;
            MoedasOuro = 0;
            Sala = 0;
            PocoesVida = 1;
            PocoesObtidas = 0;
            PocoesUsadas = 0;
            TotalMover = -1; //Metemos -1 porque ao criar jogo API devolve GOFORWARD.
            TotalAtaques = 0;
            TotalAreasExaminadas = 0;

            switch (PerfilTipo)
            {
                case "S":
                    PontosVida = 3;
                    PontosAtaque = 3;
                    PontosSorte = 3;
                    PontosVidaMax = PontosVida;
                    if (Autonomo)
                    {
                        switch (nomeEscolhido)
                        {
                            case "auto3":
                                Rondas = 3;
                                break;
                            case "auto7":
                                Rondas = 7;
                                break;
                            case "auto0":
                                Rondas = 50;
                                break;
                        }
                    }
                    break;
                case "W":
                    PontosVida = 3;
                    PontosAtaque = 2;
                    PontosSorte = 4;
                    PontosVidaMax = PontosVida;
                    break;
                case "B":
                    PontosVida = 4;
                    PontosAtaque = 3;
                    PontosSorte = 2;
                    PontosVidaMax = PontosVida;
                    break;
            }
        }

        // Atualizar o estado do Jogo
        public void AtualizarJogo(GameStateApi nGS)
        {
            ApagarMensagensDeContexto();  // Apagar as mensagens de contexto da ultima atualização
            ResetItensEncontrados(); //Atualizar variaveis locais
            AtualizarVariaveisDoJogo(nGS); //Atualizar variáveis do jogo vindas do GameState
            if (ResultadoAccao == Result.Success)  // Atualizar jogo sempre que existem accoes com Sucesso//
            {
                AtualizarAccaoSucesso(); //Chamado metodo para atualizar jogo consoante a accao
                VerificarItensEncontrados(); //Verificar itens encontrados
                if (MoedasOuroRecebidas > 0) //Deteta se foram encontradas moedas
                {
                    AtualizarMoedas(); //Atualizar moedas de ouro do modelo
                }
            }
            AccaoInvalida(); //Se a accao for inválida para debug
            AcertarVida(); //Fazer acerto de vida de combate
            PassarTempo(); //Contabilizar acções e detetar cansaço
            DetetarSeJogoAcabou(); //Detetar se jogo acabou
            if (Autonomo)  //Deteta se jogo está em modo autónomo
            {
                AccaoAutonomo(); //Metodo para decidir ação a tomar
            }
        }


        private void PassarTempo()
        {
            if ((TotalAreasExaminadas > 7) || (TotalAtaques > 7) || (TotalMover > 7))
            {
                
                PontosVida = PontosVida - 0.5;
                MensagemPassarTempo = "Cansaço: -0.5";
                
                //Na última jogada (GoForward ou Flee na última sala, para a vitória) não se aplica cansaço, qualquer que seja a vida.
                if (ResultadoAccao == Result.SuccessVictory) // Mandar mensagem especifica no caso de vencer com cansaço
                {
                    PontosVida = PontosVida + 0.5;
                    MensagemPassarTempo = "0";
                }
                if (PontosVida < 0) // Não deixar vida negativa
                {
                    PontosVida = 0;
                }

            }
        }

        public bool DetetarCansaço()
        {
            if ((TotalAreasExaminadas > 7) || (TotalAtaques > 7) || (TotalMover > 7))
            {
                return true;
            }
            else
            {
                return false;
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
            if (Chave)
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

        public void SalasExaminadas()
        {
            MensagemSalasExaminadas = "";
            for (int i = 1; i <= 7; i++)
            {
                if (arraySalasExaminadas[i])
                {
                    MensagemSalasExaminadas = MensagemSalasExaminadas + " " + Convert.ToString(i);
                }
            }
        }

        public void AccaoAutonomo() //Metodo para estratégia do jogo autonomo
        {
            // Estratégia que apenas tem em conta a nossa vida para beber poção, ataca todos os monstros, e procura todas as áreas, tenta ganhar com menos de 0.5 quando possível.
            if (Monstro)
            {
                if (PontosAtaqueMonstro > 3) //Monstro Forte
                {
                    if (Chave) //Se já tivermos a chave
                    {
                        if (DetetarCansaço() == false)
                        {
                            if (PontosVida > 2) //Caso vida seja maior que 2, arriscamos tentar matar o monstro
                            {
                                TomarAccao = PlayerAction.Attack;
                            }
                            else //Se não temos muita vida não vale a pena arriscar
                            {
                                if (PontosVida < 1.5 && PocoesVida > 0)
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
                            if (PontosVida < 1.5 && PocoesVida > 0)
                            {
                                TomarAccao = PlayerAction.DrinkPotion;
                            }
                            else
                            {
                                TomarAccao = PlayerAction.Flee;
                            }
                        }
                    }
                    else //Se não temos chave
                    {
                        if (PontosVida <= 2 && PocoesVida > 0) //Não arriscar morrer
                        {
                            TomarAccao = PlayerAction.DrinkPotion;

                        }
                        else //Atacar
                        {
                            TomarAccao = PlayerAction.Attack;
                        }
                    }
                }
                else
                {
                    if (Chave) //Se tivermos chave e o monstro tem menos de 4 de força (é fraco)
                    {
                        if (DetetarCansaço() == false) //Se já temos a chave e não estamos a ser afetados pelo cansaço
                        {
                            if (PontosVida > 1.6) //Caso vida seja maior que 1.6, arriscamos tentar matar o monstro
                            {
                                TomarAccao = PlayerAction.Attack;
                            }
                            else //Se não temos muita vida não vale a pena arriscar
                            {
                                if (PontosVida <= 1.1 && PocoesVida > 0)
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
                            if (PontosVida <= 1.9 && PocoesVida > 0)
                            {
                                TomarAccao = PlayerAction.DrinkPotion;
                            }
                            else
                            {
                                TomarAccao = PlayerAction.Flee;
                            }
                        }
                    }
                    else //Se não temos chave e o monstro tem menos de 4 de força (é fraco)
                    {
                        if (DetetarCansaço() == false)
                        {
                            if (PontosVida <= 1.3 && PocoesVida > 0)
                            {
                                TomarAccao = PlayerAction.DrinkPotion;
                            }
                            else
                            {
                                TomarAccao = PlayerAction.Attack;
                            }
                        }
                        else
                        {
                            if (PontosVida <= 1.7 && PocoesVida > 0)
                            {
                                TomarAccao = PlayerAction.DrinkPotion;
                            }
                            else
                            {
                                TomarAccao = PlayerAction.Attack;
                            }
                        }
                    }

                }
            }
            else
            {
                if (arraySalasExaminadas[Sala] == false) //caso não exista monstro e sala não tenha sido examinada
                {
                    if (Chave == false)
                    {
                        if (PontosVida < 1 && PocoesVida > 0) //se tivermos pouca vida, bebemos primeiro poção
                        {
                            TomarAccao = PlayerAction.DrinkPotion;
                        }
                        else //examinamos area
                        {
                            TomarAccao = PlayerAction.SearchArea;
                        }
                    }
                    else
                    {
                        if (Sala == 7) //Se tivermos na ultima sala e não existe monstro é porque já temos a chave
                        {
                            if (PontosVida % 1 != 0 && PontosVida > 1 && DetetarMonstroSala6() == false) //Caso vida não seja inteira e tivermos mais de 1 vamos tentar ganhar com menos de 0.5, desde que nao haja monstro na 6
                            {
                                TomarAccao = PlayerAction.GoBack;
                            }
                            else //Se vida for inteira ou não tivermos mais de 1 ou se existir monstro na sala 6 vamos avançar para ganhar.
                            {
                                TomarAccao = PlayerAction.GoForward;
                            }
                        }
                        else //Avançamos sempre que sala está vazia
                        {
                            if (Sala < 6)
                            {
                                if (DetetarCansaço() == false)
                                {
                                    if (PontosVida < 1.2 && PocoesVida > 0) //se tivermos pouca vida, bebemos primeiro poção
                                    {
                                        TomarAccao = PlayerAction.DrinkPotion;
                                    }
                                    else //examinamos area
                                    {
                                        TomarAccao = PlayerAction.SearchArea;
                                    }
                                }
                                else
                                {
                                    if (PontosVida <= 0.5)
                                    {
                                        TomarAccao = PlayerAction.DrinkPotion;
                                    }
                                    else
                                    {
                                        TomarAccao = PlayerAction.GoForward;
                                    }
                                }
                            }
                            else
                            {
                                if (PontosVida <= 0.5 && PocoesVida > 0)
                                {
                                    TomarAccao = PlayerAction.DrinkPotion;
                                }
                                else
                                {
                                    TomarAccao = PlayerAction.GoForward;
                                }
                            }
                        }
                    }
                }
                else //Se sala já foi examinada
                {
                    if (Sala == 7) //Se tivermos na ultima sala e não existe monstro é porque já temos a chave
                    {
                        if (PontosVida % 1 != 0 && PontosVida > 1 && DetetarMonstroSala6() == false) //Caso vida não seja inteira e tivermos mais de 1 vamos tentar ganhar com menos de 0.5, desde que nao haja monstro na 6
                        {
                            TomarAccao = PlayerAction.GoBack;
                        }
                        else //Se vida for inteira ou não tivermos mais de 1 ou se existir monstro na sala 6 vamos avançar para ganhar.
                        {
                            TomarAccao = PlayerAction.GoForward;
                        }
                    }
                    else //Avançamos sempre que sala está vazia
                    {
                        if (PontosVida <= 0.5 && PocoesVida > 0) //Evitar morrer de cansaço
                        {
                            TomarAccao = PlayerAction.DrinkPotion;
                        }
                        else
                        {
                            TomarAccao = PlayerAction.GoForward;
                        }
                    }
                }
            }
        }


        // Estratégia que tem em conta várias condicionantes. Ganha mais vezes que a anterior, consegue melhor score médio, não consegue scores maximos tão altos como a anterior.

        //if (Monstro)
        //{
        //    if (Sala != 7 && arraySalasExaminadas[Sala + 1] == false) //Estamos a andar para a frente
        //    {
        //        if (PontosSorteMonstro < 4)
        //        {
        //            if (PontosAtaqueMonstro < 3)
        //            {
        //                if (PontosVida > 1.6)
        //                {
        //                    TomarAccao = PlayerAction.Attack;
        //                }
        //                else
        //                {
        //                    if (PocoesVida > 0)
        //                    {
        //                        TomarAccao = PlayerAction.DrinkPotion;
        //                    }
        //                    else
        //                    {
        //                        TomarAccao = PlayerAction.Flee;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                if (Chave == false)
        //                {
        //                    if (Sala > 4)
        //                    {
        //                        if (PontosVida < 1.8 && PocoesVida > 0)
        //                        {
        //                            TomarAccao = PlayerAction.DrinkPotion;
        //                        }
        //                        else
        //                        {
        //                            TomarAccao = PlayerAction.Attack;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (PontosVida < 1.6 && PocoesVida > 0)
        //                        {
        //                            TomarAccao = PlayerAction.DrinkPotion;
        //                        }
        //                        else
        //                        {
        //                            TomarAccao = PlayerAction.Flee;
        //                        }
        //                    }
        //                }
        //                else // se já tenho chave
        //                {
        //                    if (PontosVida < 1.8 && PocoesVida > 1) // Se tiver mais de 1 poção bebe para tentar matar o monstro caso contrário é preferivel guardar poção
        //                    {
        //                        TomarAccao = PlayerAction.DrinkPotion;
        //                    }
        //                    else
        //                    {
        //                        if (PontosVidaMonstro >= 1) // não vale a pena tentar matar
        //                        {
        //                            TomarAccao = PlayerAction.Flee;
        //                        }
        //                        else // se vida do monstro for 1 ou menos, tentamos matar
        //                        {
        //                            TomarAccao = PlayerAction.Attack;
        //                        }
        //                    }
        //                }

        //            }
        //        }
        //        else
        //        {
        //            if (Sala < 3)
        //            {
        //                if (PontosAtaqueMonstro < 4)
        //                {
        //                    if (PontosVida > 1.7)
        //                    {
        //                        TomarAccao = PlayerAction.Attack;
        //                    }
        //                    else
        //                    {
        //                        if (PocoesVida > 0)
        //                        {
        //                            TomarAccao = PlayerAction.DrinkPotion;
        //                        }
        //                        else
        //                        {
        //                            TomarAccao = PlayerAction.Flee;
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    TomarAccao = PlayerAction.Flee;
        //                }
        //            }
        //            else
        //            {
        //                if (PontosVida > 1.8)
        //                {
        //                    TomarAccao = PlayerAction.Attack;
        //                }
        //                else
        //                {
        //                    if (PocoesVida > 0)
        //                    {
        //                        TomarAccao = PlayerAction.DrinkPotion;
        //                    }
        //                    else
        //                    {
        //                        if (PontosVidaMonstro <= 1)
        //                        {
        //                            TomarAccao = PlayerAction.Attack;
        //                        }
        //                        else
        //                        {
        //                            TomarAccao = PlayerAction.Flee;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else //Se a sala seguinte já foi examinada ou se estamos na 7 e temos monstro na view caso não tenhamos chave vamos tentar matar o monstro
        //    {
        //        if (Chave == false) //confirmar que a chave não apareceu
        //        {
        //            if (PontosVida < 1.8 && PocoesVida > 0)
        //            {
        //                TomarAccao = PlayerAction.DrinkPotion;
        //            }
        //            else
        //            {
        //                TomarAccao = PlayerAction.Attack;
        //            }
        //        }
        //        else //se já temos chave é porque apareceu
        //        {
        //            if (PontosVidaMonstro < 1 && PontosVida > 1.9)
        //            {
        //                TomarAccao = PlayerAction.Attack; //vamos tentar mesmo assim matar este monstro
        //            }
        //            else
        //            {
        //                TomarAccao = PlayerAction.Flee; //vamos tentar ganhar, não vale a pena gastarmos poção porque dá-nos bonus
        //            }
        //        }
        //    }
        //}
        //else
        //{
        //    if (Chave)
        //    {
        //        if (Sala == 7)
        //        {
        //            if (PontosVida % 1 != 0 && PontosVida > 1) //Caso vida não seja inteira e tivermos mais de 1 vamos tentar ganhar com menos de 0.5
        //            {
        //                if (DetetarMonstroSala6()) //Se existir monstro na sala 6 não vale a pena tentar.
        //                {
        //                    TomarAccao = PlayerAction.GoForward;
        //                }
        //                else //Vamos recuar e avançar até termos menos de 0.5 de vida.
        //                {
        //                    TomarAccao = PlayerAction.GoBack;
        //                }
        //            }
        //            else
        //            {
        //                TomarAccao = PlayerAction.GoForward;
        //            }
        //        }
        //        else
        //        {
        //            if (TomarAccao == PlayerAction.Attack && arraySalasExaminadas[Sala] == false && arraySalasExaminadas[7] == false) //se a ultima acao foi atacar, acabámos de matar um monstro e nunca tivémos na sala 7 podemos arriscar procurar a sala
        //            {
        //                TomarAccao = PlayerAction.SearchArea;
        //            }
        //            else //se não acabámos de matar um mosntro ou se já tivemos na sala 7 não vale a pena arriscar encontrar um monstro visto que já temos chave.
        //            {
        //                TomarAccao = PlayerAction.GoForward;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (arraySalasExaminadas[Sala]) //se esta sala já foi examinada
        //        {
        //            if (Sala < 7)
        //            {
        //                if (arraySalasExaminadas[Sala + 1]) //se já examinámos a sala seguinte  e não temos chave é porque tamos a tentar recuar e procurar
        //                {
        //                    TomarAccao = PlayerAction.GoBack;
        //                }
        //                else // se nunca examinámos a sala seguinte é porque não estamos a tentar recuar
        //                {
        //                    TomarAccao = PlayerAction.GoForward;
        //                }
        //            }
        //            else //se na sala 7 não temos chave, deixámos numa sala com monstro temos de recuar
        //            {
        //                TomarAccao = PlayerAction.GoBack;
        //            }
        //        }
        //        else
        //        {
        //            TomarAccao = PlayerAction.SearchArea;
        //        }
        //    }
        //}


        private bool DetetarMonstroSala6()
        {
            if (TomarAccao == PlayerAction.Flee)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual int CompareTo(object obj) // Verificar se é preciso e se é virtual???
        {
            Jogo j = (Jogo)obj;

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

        private void ApagarMensagensDeContexto() //Metodo que reseta as mensagens de contexto
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
        }
    
        private void AccaoInvalida()  //Metodo para detetar se accao foi invalida ou em jogo terminado para efeitos de debug.
        {
            //Accao invalida
            if (ResultadoAccao == Result.InvalidAction)
            {
               MensagemAccao = "Accao Invalida: " + UltimaAccao;
            }
            //Accao em jogo terminado
            if (ResultadoAccao == Result.GameHasEnded)
            {
                MensagemAccao = "Jogo terminado: " + UltimaAccao;
            }
        }

        private void DetetarSeJogoAcabou()
        {
            if (ResultadoAccao == Result.SuccessVictory)
            {
                Terminado = true;
                ResultadoJogo = ResultadoJogo.Vitoria;
                CalcularBonus();
                MensagemAccao = "* * * Parabéns * * * !!! VENCESTE O JOGO !!!";
                switch (UltimaAccao) // Atualizar numero de fugas ou movimentos efetuados no caso de terminar em vitória
                {
                    case PlayerAction.Flee:
                        NumFugas = NumFugas + 1;
                        break;
                    case PlayerAction.GoForward:
                        TotalMover = TotalMover + 1;
                        break;
                }
            }
            
            if (ResultadoAccao != Result.SuccessVictory && PontosVida == 0)
            {
                Terminado = true;
                ResultadoJogo = ResultadoJogo.Derrota;
                CalcularBonus();
                MensagemAccao = MensagemAccao + " Temos pena mas morreste! Fica para a próxima... ";
            }
        }

        private void AcertarVida()
        {
            //Acertar vida do jogador
            if (DanoSofrido > 0)
            {
                if (PontosVida - DanoSofrido < 0)
                    PontosVida = 0;
                else
                {
                    PontosVida = Math.Round(PontosVida - DanoSofrido, 1);
                }

                if (DanoSofrido <= 1)
                {
                    MensagemAccaoMonstro = MensagemAccaoMonstro + " O inimigo acertou-te de raspão! ";
                }
                if (DanoSofrido > 1)
                {
                    MensagemAccaoMonstro = MensagemAccaoMonstro + " O inimigo acertou-te em cheio! ";
                }
                MensagemVidaNeg = "-" + Convert.ToString(DanoSofrido);
            }
            

            //Sempre que houver combate acertar vida do monstro
            if ((Monstro) || (Monstro == false && UltimaAccao == PlayerAction.Attack))
            {
                PontosVidaMonstro = Math.Round(PontosVidaMonstroAtuais, 1);
            }
        }

        //Metodo para atualizar variaveis do jogo
        private void AtualizarVariaveisDoJogo(GameStateApi nGS)
        {
            RondaAtual = nGS.RoundNumber;
            MoedasOuroRecebidas = nGS.GoldFound;
            GameID = nGS.GameID;
            Monstro = nGS.FoundEnemy;
            PontosAtaqueMonstro = nGS.EnemyAttackPoints;
            PontosSorteMonstro = nGS.EnemyLuckPoints;
            DanoSofrido = nGS.EnemyDamageSuffered;
            UltimaAccao = nGS.Action;
            ResultadoAccao = nGS.Result;
            PontosVidaMonstroAtuais = nGS.EnemyHealthPoints;
            EncontradoPocao = nGS.FoundPotion;
            EncontradoChave = nGS.FoundKey;
            EncontradoItem = nGS.FoundItem;
            EfeitoAtaqueItem = nGS.ItemAttackEffect;
            EfeitoVidaItem = nGS.ItemHealthEffect;
            EfeitoSorteItem = nGS.ItemLuckEffect;
        }
        private void ResetItensEncontrados()
        {
            EncontradoItem = false;
            EncontradoChave = false;
            EncontradoTrevo = false;
            EncontradoGato = false;
            EncontradoPocao = false;

        }

        //Metodo para beber poção
        private void BeberPocao()
        {
               PocoesUsadas = PocoesUsadas + 1;
                PocoesVida = PocoesVida - 1;
                MensagemPocao = "-1";
                if (PontosVida < PontosVidaMax)
                {
                    MensagemVidaPos = "+" + (PontosVidaMax - PontosVida);
                    MensagemAccao = " Bebeste uma imperial! ";
                    PontosVida = PontosVidaMax;
                }
                else
                {
                    MensagemAccao = " Bêbado! A tua vida já estava cheia... ";
                }
        }

        //Método para realizar accao
        private void AtualizarAccaoSucesso()
        {
            switch (UltimaAccao)
            {
                //Accao avançar com sucesso//
                case PlayerAction.GoForward:
                    {
                        TotalMover = TotalMover + 1;
                        Sala = Sala + 1;

                        //Detetar se apareceu monstro
                        if (Monstro)
                        {
                            MensagemAccao = MensagemAccao + " Esta sala tem um inimigo! ";
                        }
                        break;
                    }

                //Accao GoBack com sucesso//
                case PlayerAction.GoBack:
                    {
                        TotalMover = TotalMover + 1;
                        Sala = Sala - 1;
                        //Detetar se apareceu monstro
                        if (Monstro)
                        {
                            MensagemAccao = MensagemAccao + " Deste de caras com o inimigo! ";
                        }
                        if (Recuou == false)
                        {
                            Recuou = true;
                        }
                        break;
                    }

                //Accao de Fuga com sucesso//
                case PlayerAction.Flee:
                    {
                        TotalMover = TotalMover + 1;
                        NumFugas = NumFugas + 1;
                        //Detetar se inimigo deu dano para meter a mensagem de acordo
                        if (DanoSofrido == 0) //Mensagens de contexto para fugir sem levar dano
                        {
                            MensagemAccaoFuga = MensagemAccaoFuga + " Escapaste por um triz! ";
                            MensagemVidaNeg = "Miss"; 
                        }
                        else //Mensagem de contexto para fugir mas levando dano
                        {
                            MensagemAccaoFuga = MensagemAccaoFuga + " Fugiste mas ainda te bateu. ";
                        }
                        //Detetar se na sala para onde fugimos tem inimigo
                        if (Monstro)
                        {
                            MensagemAccao = MensagemAccao + " Azar...Fugiste para uma sala com outro inimigo! "; 
                        }
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
                        break;
                    }
                case PlayerAction.DrinkPotion:
                    {
                        BeberPocao();
                        break;
                    }

                //Atacar sucesso
                case PlayerAction.Attack:
                    {
                        TotalAtaques = TotalAtaques + 1;
                        double danoDado = Math.Round(PontosVidaMonstro, 1) - Math.Round(PontosVidaMonstroAtuais, 1); //danoDado ao inimigo

                        //Ataque acertou ou falhou, atualizar mensagens de contexto de acordo
                        if (danoDado > 0)
                        {
                            MensagemDano = "-" + Convert.ToString(danoDado) + " Hit";
                        }
                        else if (danoDado == 0)
                        {
                            MensagemDano = "MISS";
                        }
                        //Detetar se inimigo falhou ataque, atualizar mensagens de contexto de acordo
                        if (DanoSofrido == 0)
                        {
                            MensagemAccaoMonstro = MensagemAccaoMonstro + " Uff... o gajo falhou!";
                            MensagemVidaNeg = "Miss";
                        }
                        //Mensagem ataque personalizada
                        switch (PerfilTipo)
                        {
                            case "S":
                                MensagemMeuAtaque = "Atiraste um gato!";
                                break;
                            case "B":
                                MensagemMeuAtaque = "Deste um arroto mortífero...";
                                break;
                            case "W":
                                MensagemMeuAtaque = "Mandaste-lhe com a calculadora.";
                                break;
                        }
                        if (Monstro == false) //Detetar se monstro morre
                        {

                            NumInimigosDerrotados = NumInimigosDerrotados + 1;
                            MensagemAccaoMonstro = MensagemAccaoMonstro + " Mataste o inimigo!!! ";
                        }
                        break;
                    }

                //Examinar Area sucesso
                case PlayerAction.SearchArea:
                    {
                        TotalAreasExaminadas = TotalAreasExaminadas + 1;
                        arraySalasExaminadas[Sala] = true;
                        SalasExaminadas();
                        if (Monstro) //Detetar se apareceu monstro
                        {
                            MensagemAccaoFuga = MensagemAccaoFuga + " O inimigo estava escondido! ";
                        }
                        if (Monstro == false && EncontradoChave == false && EncontradoItem == false && MoedasOuroRecebidas == 0 && EncontradoPocao == false) //Se não apareceu nada
                        {
                            MensagemAccao = MensagemAccao + " Examinaste a área mas não encontraste nada. ";
                        }
                        break;
                    }
            }
        }

        //Método para detetar itens encontrados
        private void VerificarItensEncontrados()
        {
            if (Chave == false)
            {
                if (EncontradoChave)
                {
                    Chave = EncontradoChave;
                    MensagemChave = " Found it! ";
                    MensagemAccao = " Encontraste a Chave! ";
                }
            }
            if (EncontradoPocao)
            {
                PocoesObtidas = PocoesObtidas + 1;
                if (PocoesVida < 3)
                {
                    PocoesVida = PocoesVida + 1;
                    MensagemAccao = MensagemAccao + " Encontraste uma cerveja! ";
                    MensagemPocao = "+1";
                }
                else
                {
                    MensagemAccao = MensagemAccao + " Encontraste uma cerveja mas não era DuFf e mandaste fora. ";
                }
            }
            if (EncontradoItem)
            {
                NumItensEncontrados = NumItensEncontrados + 1;
                MensagemAccao = MensagemAccao + " Encontraste ITEM SURPRESA: ";
                if (EfeitoVidaItem > 0 && PontosVida < 5)
                {
                    MensagemAccao = MensagemAccao + "deu-te vida! ";
                    MensagemVidaPos = "+" + EfeitoVidaItem;
                    if (PontosVida + EfeitoVidaItem < 5) //Atualizar mensagens de contexto e vida
                    {
                        PontosVida = PontosVida + EfeitoVidaItem;
                    }
                    else //Atualizar mensagens de contexto e vida
                    {
                        PontosVida = 5;
                    }
                }
                else if (EfeitoVidaItem < 0)
                {
                    MensagemAccao = MensagemAccao + "Era leite azedo! ";
                    MensagemVidaNeg = Convert.ToString(EfeitoVidaItem);
                    if (PontosVida + EfeitoVidaItem > 0)
                    {
                        PontosVida = PontosVida + EfeitoVidaItem;
                    }
                    else
                    {
                        PontosVida = 0;
                    }
                }

                if (EfeitoAtaqueItem > 0 && PontosAtaque < 5)
                {
                    MensagemAccao = MensagemAccao + "Aumentou o ataque! ";
                    MensagemAtaque = "+" + EfeitoAtaqueItem;
                    if (PontosAtaque + EfeitoAtaqueItem < 5)
                    {
                        PontosAtaque = PontosAtaque + EfeitoAtaqueItem;
                    }
                    else
                    {
                        PontosAtaque = 5;
                    }
                }
                else if (EfeitoAtaqueItem < 0)
                {
                    MensagemAccao = MensagemAccao + "Diminuiu o ataque! ";
                    MensagemAtaque = Convert.ToString(EfeitoAtaqueItem);
                    if (PontosAtaque + EfeitoAtaqueItem > 0)
                    {
                        PontosAtaque = PontosAtaque + EfeitoAtaqueItem;
                    }
                    else
                    {
                        PontosAtaque = 0;
                    }
                }

                if (EfeitoSorteItem > 0)
                {
                    EncontradoTrevo = true;
                    MensagemAccao = MensagemAccao + "Tinha um trevo de 4 folhas! ";
                    MensagemSorte = "+" + EfeitoSorteItem;
                    if (PontosSorte + EfeitoSorteItem < 5)
                    {
                        PontosSorte = PontosSorte + EfeitoSorteItem;
                    }
                    else
                    {
                        PontosSorte = 5;
                    }
                }
                else if (EfeitoSorteItem < 0)
                {
                    MensagemAccao = MensagemAccao + "Atiçou um gato preto. ";
                    EncontradoGato = true;
                    MensagemSorte = Convert.ToString(EfeitoSorteItem);
                    if (PontosSorte + EfeitoSorteItem > 0)
                    {
                        PontosSorte = PontosSorte + EfeitoSorteItem;
                    }
                    else
                    {
                        PontosSorte = 0;
                    }
                }
            }
        }
        private void AtualizarMoedas()
        {
            //Encontrar donuts e dar ênfase se for mais de 100 donuts
                MensagemOuro = "+" + Convert.ToString(MoedasOuroRecebidas);
                MoedasOuro = MoedasOuro + MoedasOuroRecebidas;
                if (MoedasOuroRecebidas > 100)
                {
                    MensagemPlim = "Nham Nham.";
                }
        }
    }
}

