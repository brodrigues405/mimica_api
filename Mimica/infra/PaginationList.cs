using Mimica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.infra {
    public class PaginationList<T> {

        public List<T> Results { get; set; } = new List<T>();
        public Paginacao paginacao { get; set; }

        public List<LinkDTO> links { get; set; } = new List<LinkDTO>();
    }
}
