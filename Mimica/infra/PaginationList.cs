using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.infra {
    public class PaginationList<T> : List<T> {
        public Paginacao paginacao { get; set; }
    }
}
