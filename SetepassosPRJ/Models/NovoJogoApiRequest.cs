using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class NovoJogoApiRequest
    {
            [Required(ErrorMessage = "Por favor introduza o seu nome")]
            public string PlayerName { get; set; }
            public string PlayerClass { get; set; }
            public string TeamKey { get; set; }

        public NovoJogoApiRequest(string playername, string playerclass)
        {
            PlayerName = playername;
            PlayerClass = playerclass;
            TeamKey = "54eac19e3f9543e1bdda45df80a117b9";
        }
    }
}
