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

            if (perfilTipoEscolhido == "S")
            {

                PontosVida = 3;
                PontosAtaque = 3;
                PontosSorte = 3;
                PontosVidaMax = PontosVida;

                if (Autonomo)
                {
                    if (nomeEscolhido == "auto3")
                    {
                        Rondas = 3;
                    }
                    if (nomeEscolhido == "auto7")
                    {
                        Rondas = 7;
                    }
                    if (nomeEscolhido == "auto0")
                    {
                        Rondas = 50;
                    }
                }

            }

            if (perfilTipoEscolhido == "W")
            {

                PontosVida = 3;
                PontosAtaque = 2;
                PontosSorte = 4;
                PontosVidaMax = PontosVida;


            }

            if (perfilTipoEscolhido == "B")
            {

                PontosVida = 4;
                PontosAtaque = 3;
                PontosSorte = 2;
                PontosVidaMax = PontosVida;

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
                ResultadoAccaoSucesso(); //Chamado metodo para atualizar jogo consoante a accao
                VerificarItensEncontrados(); //Verificar itens encontrados
                if (MoedasOuroRecebidas > 0) //Deteta se foram encontradas moedas
                {
                    AtualizarMoedas(); //Atualizar moedas de ouro do modelo
                }
            }
            AccaoInvalida(); //Se a accao for Inválida
            AcertoVida(); //Fazer acerto de vida de combate
            PassagemTempo(); //Contabilizar acções e detetar cansaço
            DetetarSeGanhouJogo(); //Calcular bonus de fim de jogo
            if (Autonomo)  //Deteta se jogo está em modo autónomo
            {
                AccaoAutonomo(); //Metodo para decidir ação a tomar
            }
        }


        private void PassagemTempo()
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
                    MensagemAccao = " Ganhaste motivação extra para vencer o cansaço " + MensagemAccao;
                }
                if (PontosVida < 0)
                {
                    PontosVida = 0;
                }

            }

            if (PontosVida <= 0 && ResultadoAccao != Result.SuccessVictory)
            {
                MensagemAccao = " Temos pena mas morreste! Fica para a próxima... " + MensagemAccao;

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
            //Detetar se existe monstro na view
            if (Monstro)
            {
                if (PontosSorteMonstro <= PontosSorte)
                {
                    if (PontosAtaqueMonstro < 4)
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
                        if (Chave == false)
                        {
                            if (Sala > 5)
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
                                if (PontosVida < 2 && PocoesVida > 0)
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
                            if (PontosVida < 2.2 && PocoesVida > 0)
                            {
                                TomarAccao = PlayerAction.DrinkPotion;
                            }
                            else
                            {
                                TomarAccao = PlayerAction.Flee;
                            }
                        }

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
                if (Chave)
                {
                    TomarAccao = PlayerAction.GoForward;
                }
                else
                {
                    if (arraySalasExaminadas[Sala])
                    {
                        if (Sala < 7)
                        {
                            TomarAccao = PlayerAction.GoForward;
                        }
                        else
                        {
                            TomarAccao = PlayerAction.GoBack;
                        }
                    }
                    else
                    {
                        TomarAccao = PlayerAction.SearchArea;
                    }
                }

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

        private void DetetarSeGanhouJogo()
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
            
            if (ResultadoAccao != Result.SuccessVictory && PontosVida <= 0)
            {
                Terminado = true;
                ResultadoJogo = ResultadoJogo.Derrota;
                CalcularBonus();
            }
        }

        private void AcertoVida()
        {
            //Acertar vida do jogador
            if (DanoSofrido > 0)
            {
                if (PontosVida - DanoSofrido < 0)
                {
                    PontosVida = 0;
                }
                else
                {
                    PontosVida = Math.Round(PontosVida - DanoSofrido, 1);
                }
                if (DanoSofrido <= 1)
                {
                    MensagemAccaoMonstro = MensagemAccaoMonstro + " O inimigo viu-te de raspão! ";
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
        private void ResultadoAccaoSucesso()
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
                        Recuou = true;
                        Sala = Sala - 1;
                        //Detetar se apareceu monstro
                        if (Monstro)
                        {
                            MensagemAccao = MensagemAccao + " Deste de caras com um inimigo! ";
                        }
                        break;
                    }

                //Accao de Fuga com sucesso//
                case PlayerAction.Flee:
                    {
                        TotalMover = TotalMover + 1;
                        NumFugas = NumFugas + 1;
                        //Detetar se inimigo deu dano para meter a mensagem de acordo
                        if (DanoSofrido == 0) //Mensagem de contexto expecifica para fugir sem levar dano
                        {
                            MensagemAccaoFuga = MensagemAccaoFuga + " Escapaste por um triz! ";
                            MensagemVidaNeg = "Miss";
                        }
                        else //Mensagem de contexto expecifica para fugir mas levando dano
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
                        double danoDado = Math.Round(PontosVidaMonstro, 1) - Math.Round(PontosVidaMonstroAtuais, 1);

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
                        if (DanoSofrido == 0)
                        {
                            MensagemAccaoMonstro = MensagemAccaoMonstro + " Uff... o gajo falhou!";
                            MensagemVidaNeg = "Miss";
                        }
                        //Mensagem ataque personalizada
                        if (PerfilTipo == "S")
                        {
                            MensagemMeuAtaque = "Atiraste um gato!";
                        }
                        if (PerfilTipo == "B")
                        {
                            MensagemMeuAtaque = "Deste um arroto mortífero...";
                        }
                        if (PerfilTipo == "W")
                        {
                            MensagemMeuAtaque = "Mandaste-lhe com a calculadora.";
                        }
                        //Detetar se monstro morre
                        if (Monstro == false)
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
                        //Detetar se apareceu monstro
                        if (Monstro)
                        {
                            MensagemAccaoFuga = MensagemAccaoFuga + " O inimigo estava escondido! ";
                        }
                        if (Monstro == false && EncontradoChave == false && EncontradoItem == false && MoedasOuroRecebidas == 0 && EncontradoPocao == false)
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
                MensagemAccao = MensagemAccao + " Encontraste ITEM SURPRESA que: ";
                if (EfeitoVidaItem > 0 && PontosVida < 5)
                {
                    MensagemAccao = MensagemAccao + "deu-te vida! ";
                    if (PontosVida + EfeitoVidaItem <= 5)
                    {
                        MensagemVidaPos = "+" + EfeitoVidaItem;
                        PontosVida = PontosVida + EfeitoVidaItem;
                    }
                    else
                    {
                        MensagemVidaPos = "+" + EfeitoVidaItem;
                        PontosVida = 5;
                    }
                }
                else if (EfeitoVidaItem < 0)
                {
                    MensagemAccao = MensagemAccao + "Era leite azedo! ";
                    if (PontosVida + EfeitoVidaItem > 0)
                    {
                        MensagemVidaNeg = Convert.ToString(EfeitoVidaItem);
                        PontosVida = PontosVida + EfeitoVidaItem;
                    }
                    else
                    {
                        MensagemVidaNeg = Convert.ToString(EfeitoVidaItem);
                        PontosVida = 0;
                    }
                }

                if (EfeitoAtaqueItem > 0 && PontosAtaque < 5)
                {
                    MensagemAccao = MensagemAccao + "Aumentou o ataque! ";
                    if (PontosAtaque + EfeitoAtaqueItem <= 5)
                    {
                        MensagemAtaque = "+" + EfeitoAtaqueItem;
                        PontosAtaque = PontosAtaque + EfeitoAtaqueItem;
                    }
                    else
                    {
                        PontosAtaque = 5;
                        MensagemAtaque = "+" + EfeitoAtaqueItem;
                    }
                }
                else if (EfeitoAtaqueItem < 0)
                {
                    MensagemAccao = MensagemAccao + "Diminuiu o ataque! ";
                    if (PontosAtaque + EfeitoAtaqueItem >= 0)
                    {
                        PontosAtaque = PontosAtaque + EfeitoAtaqueItem;
                        MensagemAtaque = Convert.ToString(EfeitoAtaqueItem);
                    }
                    else
                    {
                        MensagemAtaque = "-" + EfeitoAtaqueItem;
                        PontosAtaque = 0;
                    }
                }

                if (EfeitoSorteItem > 0)
                {
                    EncontradoTrevo = true;
                    MensagemAccao = MensagemAccao + "Tinha um trevo 4 folhas! ";
                    if (PontosSorte + EfeitoSorteItem <= 5)
                    {
                        MensagemSorte = "+" + EfeitoSorteItem;
                        PontosSorte = PontosSorte + EfeitoSorteItem;
                    }
                    else
                    {
                        MensagemSorte = "+" + EfeitoSorteItem;
                        PontosSorte = 5;
                    }
                }
                else if (EfeitoSorteItem < 0)
                {
                    MensagemAccao = MensagemAccao + "Atiçou um gato preto. ";
                    EncontradoGato = true;
                    if (PontosSorte + EfeitoSorteItem >= 0)
                    {
                        MensagemSorte = Convert.ToString(EfeitoSorteItem);
                        PontosSorte = PontosSorte + EfeitoSorteItem;
                    }
                    else
                    {
                        MensagemSorte = Convert.ToString(EfeitoSorteItem);
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

