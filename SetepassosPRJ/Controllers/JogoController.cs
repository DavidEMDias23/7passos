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
        public IActionResult DadosJogo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DadosJogo(int gameid)
        {
            Jogo JogoAtual = RepositorioJogos.GetJogo(gameid);
            return View("DadosJogo", JogoAtual);
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
                Jogo JogoNovo = new Jogo(j.Nome, j.PerfilTipo);
                

                HttpClient client = NewGameHttpClient.Client;
                string path = "/api/NewGame";

                NovoJogoApiRequest req = new NovoJogoApiRequest(j.Nome, j.PerfilTipo);
                string json = JsonConvert.SerializeObject(req);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) { return Redirect("/"); }

                string json_r = await response.Content.ReadAsStringAsync();
                GameStateApi gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);

                JogoNovo.AtualizarJogo(gs);
                RepositorioJogos.AdicionarJogo(JogoNovo);
               
                return View("JogoIniciado", JogoNovo);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult AccaoJogo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AccaoJogo(int gameid, PlayerAction action)
        {
            Jogo JogoAtual = RepositorioJogos.GetJogo(gameid);
            HiScores ScoreAtual = RepositorioHiScoresdbContext.GetScore(gameid);

            if (action != PlayerAction.Quit)
            {
                HttpClient client = NewGameHttpClient.Client;
                string path = "/api/Play";

                AtualizarJogoApiRequest aj = new AtualizarJogoApiRequest(gameid, action);
                string json = JsonConvert.SerializeObject(aj);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) { return Redirect("/"); }

                string json_r = await response.Content.ReadAsStringAsync();
                GameStateApi gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);

                JogoAtual.AtualizarJogo(gs);
                
                
            }
            else
            {
                JogoAtual.MensagemAccao = "Desististe do Jogo";
                JogoAtual.CalcularBonus();
                JogoAtual.Desistiu = true;
                JogoAtual.ResultadoJogo = ResultadoJogo.Desistiu;
                JogoAtual.Terminado = true;
                
            }

            if(JogoAtual.Terminado)
            {
                HiScores NovoScore = new HiScores();
                NovoScore.AtualizarScores(JogoAtual);
                RepositorioHiScoresdbContext.AdicionarScore(NovoScore);
            }
            return View("JogoIniciado", JogoAtual);
        }

        [HttpGet]
        public IActionResult CriarJogoAutonomo()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarJogoAutonomo(string Nome)
        {
                JogoAutonomo JogoNovo = new JogoAutonomo(Nome);    

                HttpClient client = NewGameHttpClient.Client;
                string path = "/api/NewGame";

                NovoJogoApiRequest req = new NovoJogoApiRequest(JogoNovo.Nome, JogoNovo.PerfilTipo);
                string json = JsonConvert.SerializeObject(req);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) { return Redirect("/"); }

                string json_r = await response.Content.ReadAsStringAsync();
                GameStateApi gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);

                JogoNovo.AtualizarJogoAutonomo(gs);
                RepositorioJogosAutonomos.AdicionarJogo(JogoNovo);

                 return RedirectToAction("AccaoJogoAutonomo", new { gameid = JogoNovo.GameID, action = JogoNovo.TomarAccao });
        }

        [HttpGet]
        public async Task<IActionResult> AccaoJogoAutonomo(int gameid, PlayerAction action)
        {
            JogoAutonomo JogoAtual = RepositorioJogosAutonomos.GetJogo(gameid);
            HiScores ScoreAtual = RepositorioHiScoresdbContext.GetScore(gameid);
            
            if (JogoAtual.TomarAccao != PlayerAction.Quit && JogoAtual.Rondas > 0)
            {
                HttpClient client = NewGameHttpClient.Client;
                string path = "/api/Play";

                AtualizarJogoApiRequest aj = new AtualizarJogoApiRequest(gameid, action);
                string json = JsonConvert.SerializeObject(aj);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);
                if (!response.IsSuccessStatusCode) { return Redirect("/"); }

                string json_r = await response.Content.ReadAsStringAsync();
                GameStateApi gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);

                JogoAtual.AtualizarJogoAutonomo(gs);
                JogoAtual.Rondas = JogoAtual.Rondas - 1; 
                return RedirectToAction("AccaoJogoAutonomo", new { JogoAtual.GameID, JogoAtual.TomarAccao });
            }
            else
            {
                JogoAtual.CalcularBonus();
                JogoAtual.Desistiu = true;
                JogoAtual.ResultadoJogo = ResultadoJogo.Desistiu;
                JogoAtual.Terminado = true;
                HiScores NovoScore = new HiScores();
                NovoScore.AtualizarScores(JogoAtual);
                RepositorioHiScoresdbContext.AdicionarScore(NovoScore);
                return View("DadosJogoAutonomo", JogoAtual);
            }
            
        }
    }
}
