using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class HiScores : IComparable
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int NumFugas { get; set; }
        public int NumInimigosDerrotados { get; set; }
        public int NumItensEncontrados { get; set; }
        public int PocoesObtidas { get; set; }
        public int PocoesUsadas { get; set; }
        public int GameID { get; set; }
        public int MoedasOuroTotal { get; set; }
        public ResultadoJogo ResultadoJogo { get; set; }
        public int TotalAreasExaminadas { get; set; }
        public bool Chave { get; set; }
        public bool Terminado { get; set; }


        public void AtualizarScores(Jogo j)
        {
            Chave = j.Chave;
            Nome = j.Nome;
            NumFugas = j.NumFugas;
            NumInimigosDerrotados = j.NumInimigosDerrotados;
            NumItensEncontrados = j.NumItensEncontrados;
            PocoesObtidas = j.PocoesObtidas;
            PocoesUsadas = j.PocoesUsadas;
            GameID = j.GameID;
            MoedasOuroTotal = j.MoedasOuroTotal;
            ResultadoJogo = j.ResultadoJogo;
            TotalAreasExaminadas = j.TotalAreasExaminadas;
            Terminado = j.Terminado;
        }

        public int CompareTo(object obj)
        {
            HiScores j = (HiScores)obj;

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
