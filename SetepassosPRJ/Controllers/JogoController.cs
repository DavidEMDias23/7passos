using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SetepassosPRJ.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SetepassosPRJ.Controllers
{
    public class JogoController : Controller
    {
        
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
        public async Task<IActionResult> CriarJogo(NovoJogo j)
        {
            if (ModelState.IsValid)
            {
                Jogo novoJogo = new Jogo(j.Nome, j.PerfilTipo);
                HttpClient client = MyHTTPClient.Client;
                string path = "/api/NewGame";
                string teamkey = "54eac19e3f9543e1bdda45df80a117b9";
                NewGameApiRequest req = new NewGameApiRequest(j.Nome, j.PerfilTipo, teamkey);
                string json = JsonConvert.SerializeObject(req);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) { return Redirect("/"); }
                string json_r = await response.Content.ReadAsStringAsync();
                GameStateResponse gs = JsonConvert.DeserializeObject<GameStateResponse>(json_r);

                novoJogo.AtualizarJogo(gs);

                return View("JogoIniciado", novoJogo);

            }
            else
            {
                return View();

            }
        }

       
        public IActionResult JogoMonstro()
        {
            return View();
        }
    }
}
