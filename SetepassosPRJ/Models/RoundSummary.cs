using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class RoundSummary
    {
        //Dados Ronda
        public int GameID { get; set; }
        public int NumeroRonda { get; set; }
        public PlayerAction DecisaoTomada { get; set; }
        public int Posicao { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumFugas { get; set; }
        public int NumItensEncontrados { get; set; }
        public bool Chave { get; set; }
        public int MoedasOuro { get; set; }
        public double PontosVida { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public int PocoesUsadas { get; set; }
        public int NumeroRondasJogadas { get; set; }
        public int MoedasOuroTotal { get; set; }
        public ResultadoJogo ResultadoJogo { get; set; }
        public int TotalMover { get; set; }
        public int TotalAreasExaminadas { get; set; }
        public int TotalAtaques { get; set; }
        public double PercentagemAreasInvestigadas { get; set; }

        public RoundSummary(Jogo j, int rondaAtual)
        {
            GameID = j.GameID;
            NumeroRonda = rondaAtual;
            DecisaoTomada = j.TomarAccao;
            Posicao = j.Sala;
            NumInimigosDerrotados = j.NumInimigosDerrotados;
            NumFugas = j.NumFugas;
            NumItensEncontrados = j.NumItensEncontrados;
            Chave = j.Chave;
            MoedasOuro = j.MoedasOuro;
            PontosVida = j.PontosVida;
            PontosAtaque = j.PontosAtaque;
            PontosSorte = j.PontosSorte;
            PocoesVida = j.PocoesVida;
            PocoesUsadas = j.PocoesUsadas;
            NumeroRondasJogadas = rondaAtual;
            MoedasOuroTotal = j.MoedasOuroTotal;
            ResultadoJogo = j.ResultadoJogo;
            TotalMover = j.TotalMover;
            TotalAreasExaminadas = j.TotalAreasExaminadas;
            TotalAtaques = j.TotalAtaques;
            PercentagemAreasInvestigadas = (TotalAreasExaminadas * 100) / 7;
        }
    }
}
