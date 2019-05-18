using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioJogo
    {
        private static List<Jogo>jogos = new List<Jogo>();

        public static List<Jogo> Jogos
        {
            get
            {
                return jogos;
            }
        }

        public static void AddJogos(Jogo novoJogo)
        {
            Jogos.Add(novoJogo);
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
