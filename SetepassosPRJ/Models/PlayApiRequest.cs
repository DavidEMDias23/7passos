using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public enum PlayerAction {GoForward,GoBack,SearchArea,DrinkPotion,Attack,Flee,Quit}

    public class PlayApiRequest
    {
        public int id { get; set; }
        public string key { get; set; }
        public PlayerAction PlayerAction { get; set; }

        public PlayApiRequest(int gameid, PlayerAction playeraction)
        {
            id = gameid;
            PlayerAction = playeraction;
            key = "54eac19e3f9543e1bdda45df80a117b9";
        }

    }


}
