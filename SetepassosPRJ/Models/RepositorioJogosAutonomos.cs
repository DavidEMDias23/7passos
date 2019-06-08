using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioJogosAutonomos
    {
        private static List<JogoAutonomo> jogos = new List<JogoAutonomo>();
        public static List<JogoAutonomo> ListaJogos
        {
            get { return jogos; }
        }

        public static void AdicionarJogo(JogoAutonomo newJogo)
        {
            jogos.Add(newJogo);
        }

        public static JogoAutonomo GetJogo(int gameid)
        {
            foreach (JogoAutonomo jogo in jogos)
            {
                if (jogo.GameID == gameid)
                {
                    return jogo;
                }
            }
            return null;
        }

        //public static List<Jogo> GetTop(int n)
        //{

        //    List<Jogo> jogosTerminados = new List<Jogo>();
        //    foreach (Jogo jogo in ListaJogos)
        //    {
        //        if (jogo.Terminado == true)
        //        {
        //            jogosTerminados.Add(jogo);
        //        }
        //    }
        //    jogosTerminados.Sort();
        //    jogosTerminados.Reverse();

        //    List<Jogo> jogosTop = new List<Jogo>();
        //    int rank = 1;
        //    foreach (Jogo jogo in jogosTerminados)
        //    {
        //        jogo.Posicao = rank;
        //        jogosTop.Add(jogo);
        //        rank++;
        //        if (jogosTop.Count == n)
        //        {
        //            break;
        //        }
        //    }
        //    return jogosTop;
        //}

    }
}
