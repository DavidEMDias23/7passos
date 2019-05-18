using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioGameID
    {
        private static List<int> gameid = new List<int>();

        public static List<int> Gameid
        {
            get
            {
                return gameid;
            }
        }

        public static void AddGameId(int gameid)
        {
            Gameid.Add(gameid);
        }

        public static int GetGameId(int gameid)
        {
            return gameid;
        }

    }
}
