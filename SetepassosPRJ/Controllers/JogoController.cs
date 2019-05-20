﻿using System;
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
            HttpClient client = NewGameHttpClient.Client;
            string path = "/api/Play";

            Jogo novoJogo = RepositorioJogos.GetJogo(gameid);

            AtualizarJogoApiRequest aj = new AtualizarJogoApiRequest(gameid, action);
            string json = JsonConvert.SerializeObject(aj);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, path);
            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode) { return Redirect("/"); }

            string json_r = await response.Content.ReadAsStringAsync();
            GameStateApi gs = JsonConvert.DeserializeObject<GameStateApi>(json_r);
         

            novoJogo.AtualizarJogo(gs);

            return View("JogoIniciado", novoJogo);
        }
    }
}
