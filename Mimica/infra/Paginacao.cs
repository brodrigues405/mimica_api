using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.infra {
    public class Paginacao {

        public int numeroPagina { get; set; }

        public int RegistroPoPagina { get; set; }

        public int TotalRegistros { get; set; }

        public int TotalPaginas { get; set; }
    }
}
