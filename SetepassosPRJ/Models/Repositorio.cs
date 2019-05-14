using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class Repositorio
    {
        private static List<Perfil> perfil = new List<Perfil>();

        public static List<Perfil> Perfil
        {
            get
            {
                return perfil;
            }
        }

        public static void AddPerfil(Perfil perfil)
        {
            Perfil.Add(perfil);
        }
    }
}
