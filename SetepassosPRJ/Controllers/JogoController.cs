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
                Jogo JogoNovo = new Jogo(j.Nome, j.PerfilTipo, j.Autonomo);

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

                if (JogoNovo.Autonomo == false)
                {
                    return View("JogoIniciado", JogoNovo);
                }
                // SE FOR AUTONOMO fazer o ciclo while
                else
                {
                    while (gs.RoundNumber < JogoNovo.Rondas && 
                            (JogoNovo.Terminado == false))
                    {

                            HttpClient clientAuton = NewGameHttpClient.Client;
                            path = "/api/Play";

                            AtualizarJogoApiRequest ajAuton = new AtualizarJogoApiRequest(JogoNovo.GameID, JogoNovo.TomarAccao);
                            json = JsonConvert.SerializeObject(ajAuton);

                            request = new HttpRequestMessage(HttpMethod.Post, path);
                            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                            response = await clientAuton.SendAsync(request);
                            if (!response.IsSuccessStatusCode) { return Redirect("/"); }

                            json_r = await response.Content.ReadAsStringAsync();
                            gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);

                            JogoNovo.AtualizarJogo(gs);
                            //RoundSummary rs = new RoundSummary(JogoNovo, gs.RoundNumber);
                            //RepositorioRondas.AdicionarRonda(rs);
                    }
                    if (gs.RoundNumber == JogoNovo.Rondas)
                    {
                        client = NewGameHttpClient.Client;
                        path = "/api/Play";

                        AtualizarJogoApiRequest aja = new AtualizarJogoApiRequest(JogoNovo.GameID, PlayerAction.Quit);
                        json = JsonConvert.SerializeObject(aja);

                        request = new HttpRequestMessage(HttpMethod.Post, path);
                        request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                       response = await client.SendAsync(request);
                        if (!response.IsSuccessStatusCode) { return Redirect("/"); }

                        json_r = await response.Content.ReadAsStringAsync();
                        gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);


                        JogoNovo.CalcularBonus();
                        JogoNovo.Desistiu = true;
                        JogoNovo.ResultadoJogo = ResultadoJogo.Desistiu;
                        JogoNovo.Terminado = true;
                    }
                    return View("DadosJogo", JogoNovo);
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CriarJogoAutonomo()
        {
            return View();
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



                JogoAtual.MensagemAccao = "Desististe do Jogo";
                JogoAtual.CalcularBonus();
                JogoAtual.Desistiu = true;
                JogoAtual.ResultadoJogo = ResultadoJogo.Desistiu;
                JogoAtual.Terminado = true;

            }

            if (JogoAtual.Terminado && JogoAtual.Autonomo == false)
            {
                HiScores NovoScore = new HiScores();
                NovoScore.AtualizarScores(JogoAtual);
                RepositorioHiScoresdbContext.AdicionarScore(NovoScore);
            }
            if (JogoAtual.Autonomo == false)
            {
                return View("JogoIniciado", JogoAtual);
            }
            else
            {
                return View("JogoIniciadoAutonomo", JogoAtual);
            }
        }
    }
}
