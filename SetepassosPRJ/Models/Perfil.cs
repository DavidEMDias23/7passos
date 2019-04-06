using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SetepassosPRJ.Models
{
    public class Perfil
    {
        private string nome;
        private int moedasOuro;
        private int pontosVida;
        private int pontosAtaque;
        private int pontosSorte;
        private int pocoesVida;
        private bool chave;

        [Required(ErrorMessage ="Introduza um nome")]
        public string Nome { get; set; }

        public int MoedasOuro { get; set; }
        public int PontosVida { get; set; }
        public int PontosAtaque { get; set; }
        public int PontosSorte { get; set; }
        public int PocoesVida { get; set; }
        public bool Chave { get; set; }

        
    }
}
