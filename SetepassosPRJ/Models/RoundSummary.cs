using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class RoundSummary
    {
        //Dados Ronda
        public int NumeroRonda { get; set; }
        public PlayerAction DecisaoTomada { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumFugas { get; set; }
        public int NumItensEncontrados { get; set; }
        public bool Chave { get; set; }
        public int MoedasOuro { get; set; }
        public int PontosVida { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public int NumeroRondasJogadas { get; set; }
        public int MoedasOuroTotal { get; set; }
        public ResultadoJogo ResultadoJogo { get; set; }
        public int TotalMover { get; set; }
        public int TotalAreasExaminadas { get; set; }
        public int PocoesUsadas { get; set; }
        public int TotalAtaques { get; set; }

    }
}
