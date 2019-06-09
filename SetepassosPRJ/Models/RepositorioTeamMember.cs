using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public static class RepositorioTeamMember
    {
        private static List<TeamMember> membros = new List<TeamMember>()
        {
            new TeamMember { NomeDaEquipa="CaptainMangi" , NomeDoMembro="Frederico Milheiro" , NumeroDoMembro=170323042 },
            new TeamMember { NomeDaEquipa="CaptainMangi" , NomeDoMembro="David Dias" , NumeroDoMembro=170323009 },
            new TeamMember { NomeDaEquipa="CaptainMangi" , NomeDoMembro="João Ribeiro" , NumeroDoMembro=170323004 }
        };
        public static List<TeamMember> ListaMembros
        {
            get { return membros; }
        }

        public static void AdicionarMembro(TeamMember novoMembro)
        {
            membros.Add(novoMembro);
        }

        public static TeamMember GetMembro(int numeroMembro)
        {
            foreach (TeamMember member in membros)
            {
                if (member.NumeroDoMembro == numeroMembro)
                {
                    return member;
                }
            }
            return null;
        }
    }
}
