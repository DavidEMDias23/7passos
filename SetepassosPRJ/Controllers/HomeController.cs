using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SetepassosPRJ.Models;

namespace SetepassosPRJ.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Apresentacao()
        {
            return View();
        }

        public IActionResult HiScores()
        {
            List<Jogo> jogos = RepositorioJogos.ListaJogos;
            jogos.Sort();
            jogos.Reverse();
            int rank = 1;
            foreach(Jogo jg in jogos)
            {
                jg.Posicao = rank;
                rank++;
            }
            return View(jogos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
