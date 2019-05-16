using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class PlayApiRequest
    {
        public int GameId { get; set; }
        public string TeamKey { get; set; }
        public int PlayerAction { get; set; }

        public PlayApiRequest(int gameid, string teamkey, int playeraction)
        {
            GameId = gameid;
            TeamKey = teamkey;
            PlayerAction = playeraction;
        }
    }
}
