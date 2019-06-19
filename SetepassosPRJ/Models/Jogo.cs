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
            //estratégia Berserk
            if (Monstro)
            {
                if (CompararStatus() < 0)
                {
                    if (PontosVida < 2 && PocoesVida > 0)
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
                    if (PontosVida < 1.2 && PocoesVida > 0)
                    {
                        TomarAccao = PlayerAction.DrinkPotion;
                    }
                    else
                    {
                        TomarAccao = PlayerAction.Attack;
                    }
                }
            }
            else
            {
                if (arraySalasExaminadas[Sala])
                {
                    if (Sala == 7)
                    {
                        if (PontosVida % 0.5 != 0 && PontosVida > 1) //Caso vida não seja inteira e tivermos mais de 1 vamos tentar ganhar com menos de 0.5, desde que nao haja monstro na 6
                        {
                            TomarAccao = PlayerAction.GoBack;
                        }
                        else //Se vida for inteira ou não tivermos mais de 1 ou se existir monstro na sala 6 vamos avançar para ganhar.
                        {
                            TomarAccao = PlayerAction.GoForward;
                        }

                    }
                    else
                    {
                        if (DetetarCansaço())
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
                        else
                        {
                            TomarAccao = PlayerAction.GoForward;
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
                        TomarAccao = PlayerAction.SearchArea;
                    }
                }
            }

            // Estratégia ponderada.
            //if (Monstro) // Sempre que existe monstro na view
            //{
            //    if (Chave == false) //Se não temos a chave
            //    {
            //        if (CompararStatus() < -1 && Sala < 3) //Se a sala for inferior a 3 e os nossos status são muito inferiores aos do monstro, vamos fugir.
            //        {
            //            if (PontosVida < 1.8 && PocoesVida > 0) //ver se não arriscamos morrer ao fugir.
            //            {
            //                TomarAccao = PlayerAction.DrinkPotion;
            //            }
            //            else //fugir
            //            {
            //                TomarAccao = PlayerAction.Flee;
            //            }
            //        }
            //        else //Se os nossos status não forem mto inferiores ao do monstro na sala 1 ou 2, Ou se já estamos na sala 3 ou superior e sem chave temos de arriscar
            //        {
            //            if (CompararStatus() > 1 && DetetarCansaço() == false) //Antes de atacar vamos prevenir, se monstro for fraco e não tamos cansados.
            //            {
            //                if (PontosVida < 1 && PocoesVida > 0) // Podemos arriscar com 1 ponto de vida ou mais. Caso contrário bebemos poção.
            //                {
            //                    TomarAccao = PlayerAction.DrinkPotion;
            //                }
            //                else
            //                {
            //                    TomarAccao = PlayerAction.Attack;
            //                }
            //            }
            //            else // Se o monstro é forte ou se já tamos cansados
            //            {
            //                if (PontosVida <= 1.9 && PocoesVida > 0) // Já só podemos arriscar com 1.9 ou mais de vida.
            //                {
            //                    TomarAccao = PlayerAction.DrinkPotion;
            //                }
            //                else
            //                {
            //                    TomarAccao = PlayerAction.Attack;
            //                }
            //            }
            //        }
            //    }
            //    else //Se existe monstro na view mas já temos a chave
            //    {
            //        if (DetetarCansaço() == false) //Se nao tivermos cansados 
            //        {
            //            if (CompararStatus() > 2 && PontosVida > 2) // se o monstro for mais fraco e nós temos vida suficiente para arriscar
            //            {
            //                TomarAccao = PlayerAction.Attack; //atacamos
            //            }
            //            else //se o monstro é forte ou se não temos vida suficiente não vale a pena arriscar
            //            {
            //                if (PontosVida < 1.8 && PocoesVida > 0) //vamos garantir que conseguimos fugir, visto que já temos a chave
            //                {
            //                    TomarAccao = PlayerAction.DrinkPotion;
            //                }
            //                else
            //                {
            //                    TomarAccao = PlayerAction.Flee;
            //                }
            //            }
            //        }
            //        else //se já tamos cansados, apesar de termos a chave, não vamos arriscar atacar o monstro por mto fraco que seja, pois o cansaço pode levarnos a gastar poções, poções são bonus altos.
            //        {
            //            if (CompararStatus() > 2) //se o monstro for muito mais fraco podemos fugir desde que tenhamos 1.5 ou mais de vida.
            //            {
            //                if (PontosVida < 1.5 && PocoesVida > 0)
            //                {
            //                    TomarAccao = PlayerAction.DrinkPotion;
            //                }
            //                else
            //                {
            //                    TomarAccao = PlayerAction.Flee;
            //                }
            //            }
            //            else //Se o monstro não for muito mais fraco, visto que já temos a chave, vamos garantir que conseguimos fugir, temos de ter pelo menos 1.8.
            //            {
            //                if (PontosVida < 1.8 && PocoesVida > 0)
            //                {
            //                    TomarAccao = PlayerAction.DrinkPotion;
            //                }
            //                else
            //                {
            //                    TomarAccao = PlayerAction.Flee;
            //                }
            //            }
            //        }

            //    }
            //}
            //else //Se não existe monstro na view, tamos perante uma sala sem Monstro.
            //{
            //    if (arraySalasExaminadas[Sala] == false) //caso sala não tenha sido examinada
            //    {
            //        if (Chave) //Se já temos a chave numa sala não examinada não vamos arriscar examinar e perder vida.
            //        {
            //            if (Sala != 7) // Se a sala não for a última vamos garantir que conseguimos avançar sem morrer para o cansaço. Não vamos arriscar procurar.
            //            {
            //                if (Sala < 5) //antes da sala 5 não avale a pena arriscar para não ativar cansaço.
            //                {
            //                    if (DetetarCansaço() == false)
            //                    {
            //                        TomarAccao = PlayerAction.GoForward;
            //                    }
            //                    else
            //                    {
            //                        if (PontosVida <= 0.5 && PocoesVida > 0)
            //                        {
            //                            TomarAccao = PlayerAction.DrinkPotion;
            //                        }
            //                        else
            //                        {
            //                            TomarAccao = PlayerAction.GoForward;
            //                        }
            //                    }
            //                }
            //                else
            //                {
            //                    if (DetetarCansaço() == false) //podemos arriscar
            //                    {
            //                        if (PontosVida > 3) //so nestas condições
            //                        {
            //                            TomarAccao = PlayerAction.SearchArea;
            //                        }
            //                        else
            //                        {
            //                            TomarAccao = PlayerAction.GoForward;
            //                        }
            //                    }
            //                    else //se já tamos no cansaço não vamos arriscar
            //                    {
            //                        if (PontosVida <= 0.5 && PocoesVida > 0)
            //                        {
            //                            TomarAccao = PlayerAction.DrinkPotion;
            //                        }
            //                        else
            //                        {
            //                            TomarAccao = PlayerAction.GoForward;
            //                        }
            //                    }
            //                }
            //            }
            //            else //se tivermos na ultima sala, visto que já temos chave, não vamos arriscar examinar sala e perder.
            //            {
            //                if (PontosVida % 0.5 != 0 && PontosVida > 1 && DetetarMonstroSala6() == false) //Caso vida não seja inteira e tivermos mais de 1 vamos tentar ganhar com menos de 0.5, desde que nao haja monstro na 6
            //                {
            //                    TomarAccao = PlayerAction.GoBack;
            //                }
            //                else //Se vida for inteira ou não tivermos mais de 1 ou se existir monstro na sala 6 vamos avançar para ganhar.
            //                {
            //                    TomarAccao = PlayerAction.GoForward;
            //                }
            //            }
            //        }
            //        else //Estamos numa sala não examinada, sem chave. Temos de arriscar examinar.
            //        {
            //            if (PontosVida < 1.8 && PocoesVida > 0) //prevenir morrer para itens que dão dano.
            //            {
            //                TomarAccao = PlayerAction.DrinkPotion;
            //            }
            //            else
            //            {
            //                TomarAccao = PlayerAction.SearchArea;
            //            }
            //        }
            //    }
            //    else //Se estamos numa sala que já foi examinada e não tem monstro
            //    {
            //        if (Chave) //já temos a chave, vamos tentar ganhar.
            //        {
            //            if (Sala == 7) // Se estamos na ultima sala, com chave vamos tentar ganhar.
            //            {
            //                if (PontosVida % 0.5 != 0 && PontosVida > 1 && DetetarMonstroSala6() == false) //Caso vida não seja inteira e tivermos mais de 1 vamos tentar ganhar com menos de 0.5, desde que nao haja monstro na 6
            //                {
            //                    TomarAccao = PlayerAction.GoBack;
            //                }
            //                else //Se vida for inteira ou não tivermos mais de 1 ou se existir monstro na sala 6 vamos avançar para ganhar.
            //                {
            //                    TomarAccao = PlayerAction.GoForward;
            //                }

            //            }
            //            else //Se estamos numa sala que não a 7, já foi examinada e temos a chave, vamos avançar.
            //            {
            //                if (DetetarCansaço() == false)
            //                {
            //                    TomarAccao = PlayerAction.GoForward;
            //                }
            //                else
            //                {
            //                    if (PontosVida <= 0.5 && PocoesVida > 0) // prevenir morrer para cansaço)
            //                    {
            //                        TomarAccao = PlayerAction.DrinkPotion;
            //                    }
            //                    else
            //                    {
            //                        TomarAccao = PlayerAction.GoForward;
            //                    }
            //                }
            //            }
            //        }
            //        else //Se ainda não temos chave
            //        {
            //            if (Sala == 7) //estamos na sala 7, já foi examinada e não temos chave, deixámos a chave numa das duas primeiras salas.
            //            {
            //                TomarAccao = PlayerAction.GoBack; //Vamos suicidar para o cansaço, visto que é preferivel suicidar com os pontos das poções do que tentar ir buscar a chave.
            //            }
            //            else // Se estamos numa sala que não é a 7, e não temos chave, e a sala já foi examinada, vamos avançar.
            //            {
            //                if (arraySalasExaminadas[7]) //Visto que a 7 já foi examinada, é porque estamos a tentar suicidar para cansaço.
            //                {
            //                    TomarAccao = PlayerAction.GoForward;
            //                }
            //                else //Se 7 ainda não foi examinada vamos prevenir morrer para cansaço e continuar a avançar.
            //                {
            //                    if (DetetarCansaço() && PontosVida <= 0.5 && PocoesVida > 0)
            //                    {
            //                        TomarAccao = PlayerAction.DrinkPotion;
            //                    }
            //                    else
            //                    {
            //                        TomarAccao = PlayerAction.GoForward;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private bool DetetarMonstroSala6() //Este método para autónomo é usado na sala 7. Sempre que a ultima acção foi um flee, garantidamente existe monstro na sala 6.
        {
            if (UltimaAccao == PlayerAction.Flee)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double CompararStatus() //método para autónomo que ajuda a calcular as diferenças de status (Sorte e Ataque) entre jogador e monstro para tomar melhores decisões.
        {
            double statusMonstro = 0;
            double statusPlayer = 0;
            double diferencaStatus = 0;
            statusMonstro = PontosSorteMonstro + PontosAtaqueMonstro;
            if (PontosAtaqueMonstro == 5) //Vamos ter cuidado especial com monstros de 5 de ataque porque dão muito dano.
            {
                statusMonstro = statusMonstro + 2;
            }

            statusPlayer = PontosSorte + PontosAtaque;
            if (PontosAtaque > 3 && PontosSorte > 1 && PontosAtaqueMonstro < 3) //Situações em que temos 4 ou mais de ataque temos de aproveitar
            {
                statusPlayer = statusPlayer + 2;
            }
            diferencaStatus = statusPlayer - statusMonstro;
            return diferencaStatus; //Quanto mais positivo melhor os status do player comparativamente aos do monstro, e vice versa.
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
                    if (PontosVida + EfeitoVidaItem < 5) //Atualizar vida
                    {
                        PontosVida = PontosVida + EfeitoVidaItem;
                    }
                    else //Atualizar vida
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

