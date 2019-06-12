using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioHiScores
    {
        private static List<HiScores> score = new List<HiScores>();
        public static List<HiScores> ListaScores
        {
            get { return score; }
        }

        public static void AdicionarScore(HiScores novoScore)
        {
            score.Add(novoScore);
        }

      
        public static List<HiScores> GetTop(int n)
        {

            List<HiScores> jogosTerminados = new List<HiScores>();
            foreach (HiScores jogo in ListaScores)
            {
                if (jogo.Terminado == true)
                {
                    jogosTerminados.Add(jogo);
                }
            }
            jogosTerminados.Sort();
            jogosTerminados.Reverse();

            List<HiScores> jogosTop = new List<HiScores>();
            int rank = 1;
            foreach (HiScores jogo in jogosTerminados)
            {
                jogosTop.Add(jogo);
                rank++;
                if (jogosTop.Count == n)
                {
                    break;
                }
            }
            return jogosTop;
        }


        public static HiScores GetScore(int gameid)
        {
            foreach (HiScores jogo in score)
            {
                if (jogo.GameID == gameid)
                {
                    return jogo;
                }
            }
            return null;
        }
    }
}
