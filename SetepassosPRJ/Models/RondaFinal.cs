using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class RondaFinal
    {
        public List<RoundSummary> rsList { get; set; }
        public int GameID { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumFugas { get; set; }
        public int NumItensEncontrados { get; set; }
        public bool Chave { get; set; }
        public int PocoesUsadas { get; set; }
        public int NumeroRondasJogadas { get; set; }
        public int MoedasOuroTotal { get; set; }
        public ResultadoJogo ResultadoJogo { get; set; }
        public int TotalMover { get; set; }
        public int TotalAreasExaminadas { get; set; }
        public int TotalAtaques { get; set; }
        public double PercentagemAreasInvestigadas { get; set; }


        public RondaFinal()
        {

        }
        public RondaFinal(Jogo j)
        {
            GameID = j.GameID;
            NumInimigosDerrotados = j.NumInimigosDerrotados;
            NumFugas = j.NumFugas;
            NumItensEncontrados = j.NumItensEncontrados;
            Chave = j.Chave;
            PocoesUsadas = j.PocoesUsadas;
            NumeroRondasJogadas = j.RondaAtual;
            MoedasOuroTotal = j.MoedasOuroTotal;
            ResultadoJogo = j.ResultadoJogo;
            TotalMover = j.TotalMover;
            TotalAreasExaminadas = j.TotalAreasExaminadas;
            TotalAtaques = j.TotalAtaques;
            PercentagemAreasInvestigadas = (TotalAreasExaminadas * 100) / 7;
        }
    }
}
