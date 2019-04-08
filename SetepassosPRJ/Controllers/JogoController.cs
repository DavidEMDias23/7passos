using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SetepassosPRJ.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SetepassosPRJ.Controllers
{
    public class JogoController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CriarJogo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarJogo(Perfil NovoPerfil)
        {
            if (ModelState.IsValid)
            {
                NovoPerfil = new Perfil
                {
                    Nome = NovoPerfil.Nome,
                    MoedasOuro = 0,
                    PontosAtaque = 4,
                    PontosSorte = 3,
                    PontosVida = 2,
                    PocoesVida = 1,
                    Chave = false
                };
                return View("JogoIniciado", NovoPerfil);
            }
            else
                return View();
        }

       
        public IActionResult JogoMonstro()
        {
            return View();
        }
    }
}
