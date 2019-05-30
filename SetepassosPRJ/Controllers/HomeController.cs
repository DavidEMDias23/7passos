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
            List<Jogo> melhoresJogos = RepositorioJogos.GetTop(3);
            return View(melhoresJogos);
        }

        public IActionResult Apresentacao()
        {
            return View();
        }

        public IActionResult HiScores()
        {

            List<Jogo> melhoresJogos = RepositorioJogos.GetTop(10);
            return View(melhoresJogos);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
