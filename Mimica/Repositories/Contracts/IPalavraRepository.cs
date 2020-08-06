using Mimica.infra;
using Mimica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.Repositories.Contracts {
    public interface IPalavraRepository {

        PaginationList<Palavra> ObterPalavras(ParamObterPalavras query);
        Palavra Obter(int id);

        void Cadastrar(Palavra palavra);

        void Atualizar(Palavra palavra);

        void Deletar(int id);



    }
}
