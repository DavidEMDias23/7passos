using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioJogos
    {
        private static List<Jogo> jogos = new List<Jogo>();
        public static List<Jogo> ListaJogos
        {
            get { return jogos; }
        }

        public static void AdicionarJogo(Jogo newJogo)
        {
            jogos.Add(newJogo);
        }

        public static Jogo GetJogo(int gameid)
        {
            foreach (Jogo jogo in jogos)
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
