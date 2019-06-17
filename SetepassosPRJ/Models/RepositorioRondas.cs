using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioRondas
    {
        private static List<RoundSummary> rondas = new List<RoundSummary>();
        public static List<RoundSummary> ListaRondas
        {
            get { return rondas; }
        }

        public static void AdicionarRonda(RoundSummary novaRonda)
        {
            rondas.Add(novaRonda);
        }

        public static RoundSummary GetRonda(int rondNumber)
        {
            foreach (RoundSummary ronda in rondas)
            {
                if (ronda.NumeroRonda == rondNumber)
                {
                    return ronda;
                }
            }
            return null;
        }
    }
}
