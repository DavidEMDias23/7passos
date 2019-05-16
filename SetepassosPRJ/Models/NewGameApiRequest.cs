﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class NewGameApiRequest
    {
        public string PlayerName { get; set; }
        public string PlayerClass { get; set; }
        public string Teamkey { get; set; }

        public NewGameApiRequest(string playername, string playerclass, string teamkey)
        {
            PlayerName = playername;
            PlayerClass = playerclass;
            Teamkey = teamkey;
        }
    }
}
