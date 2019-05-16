using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class Repositorio
    {
        private static List<Jogo> perfil = new List<Jogo>();

        public static List<Jogo> Perfil
        {
            get
            {
                return perfil;
            }
        }

        public static void AddPerfil(Jogo perfil)
        {
            Perfil.Add(perfil);
        }
    }
}
