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
                RepositorioJogo.AddJogos(novoJogo);
                HttpClient client = MyHTTPClient.Client;
                string path = "/api/NewGame";
                
                NewGameApiRequest req = new NewGameApiRequest(j.Nome, j.PerfilTipo);
                string json = JsonConvert.SerializeObject(req);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode) { return Redirect("/"); }
                string json_r = await response.Content.ReadAsStringAsync();
                GameStateResponse gs = JsonConvert.DeserializeObject<GameStateResponse>(json_r);

                
                novoJogo.GameID = gs.GameID;
                RepositorioGameID.AddGameId(gs.GameID);
                novoJogo.AtualizarJogo(gs);

                return View("JogoIniciado", novoJogo);

            }
            else
            {
                return View();

            }
        }

        [HttpGet]
        public IActionResult Jogada()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Jogada(int gameid, PlayerAction action)
        {

            Jogo novoJogo = RepositorioJogo.GetJogo(gameid);
            
            HttpClient client = MyHTTPClient.Client;
            string path = "/api/Play";

            PlayApiRequest req = new PlayApiRequest(gameid,action);
            string json = JsonConvert.SerializeObject(req);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode) { return Redirect("/"); }
            string json_r = await response.Content.ReadAsStringAsync();
            GameStateResponse gs = JsonConvert.DeserializeObject<GameStateResponse>(json_r);
            novoJogo.Sala = novoJogo.Sala + 1;
          
            return View("JogoIniciado",novoJogo);
        }



        public IActionResult JogoMonstro()
        {
            return View();
        }
    }
}
