using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.Models {
    public class Palavra {

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public int Pontuacao { get; set; }

        public bool Ativo { get; set; }

        public DateTime Criado { get; set; }

        public DateTime? Atualizado { get; set; }

    }
}
