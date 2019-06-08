using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public enum PlayerAction { GoForward, GoBack, SearchArea, DrinkPotion, Attack, Flee, Quit, Null }

    public class AtualizarJogoApiRequest
    {
        public int id { get; set; }
        public string key { get; set; }
        public PlayerAction Playeraction { get; set; }

    public AtualizarJogoApiRequest(int gameid, PlayerAction playeraction)
    {
        id = gameid;
        key = "54eac19e3f9543e1bdda45df80a117b9";
        Playeraction = playeraction;
    }
  }
}
