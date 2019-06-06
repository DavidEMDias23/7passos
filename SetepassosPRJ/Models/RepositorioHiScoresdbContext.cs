using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioHiScoresdbContext
    {
       // private static List<HiScores> score = new List<HiScores>();
        public static List<HiScores> ListaScores
        {

            get {
                HiScoresDbContext context = new HiScoresDbContext();
                List<HiScores> scores = context.Scores.ToList();
                return scores;
                }
        }

        public static void AdicionarScore(HiScores novoScore)
        {
            HiScoresDbContext context = new HiScoresDbContext();
            context.Scores.Add(novoScore);
            context.SaveChanges();
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
                jogo.Posicao = rank;
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
            foreach (HiScores jogo in ListaScores)
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
