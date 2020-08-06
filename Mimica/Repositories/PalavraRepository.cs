using Microsoft.EntityFrameworkCore;
using Mimica.infra;
using Mimica.Models;
using Mimica.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.Repositories {
    public class PalavraRepository : IPalavraRepository {

        private readonly MimicaContext _context;

        public PalavraRepository(MimicaContext context) {
            _context = context;
        }

        public PaginationList<Palavra> ObterPalavras(ParamObterPalavras query) {
            

            var lista = new PaginationList<Palavra>();
            var item = _context.palavras.AsNoTracking().AsQueryable();
            if (query.Data.HasValue) {
                item = item.Where(a => a.Criado > query.Data.Value || a.Atualizado > query.Data.Value);
            }

            if (query.PagNumero.HasValue) {
                var qtdRegistros = item.Count();
                item = item.Skip((query.PagNumero.Value - 1) * query.PagQtdRegistro.Value).Take(query.PagQtdRegistro.Value);
                  
                var paginacao = new Paginacao();
                paginacao.numeroPagina = query.PagNumero.Value;
                paginacao.RegistroPoPagina = query.PagQtdRegistro.Value;
                paginacao.TotalRegistros = qtdRegistros;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)qtdRegistros / query.PagQtdRegistro.Value);
                
            lista.paginacao = paginacao;
            }
            lista.AddRange(item.ToList());
            return lista;

        }

        public Palavra Obter(int id) {
            return _context.palavras.Find(id);
        }

        public void Cadastrar(Palavra palavra) {
            _context.palavras.Add(palavra);
            _context.SaveChanges();
        }

        public void Atualizar(Palavra palavra) {
            _context.palavras.Update(palavra);
            _context.SaveChanges();
        }

        public void Deletar(int id) {
            var obj = Obter(id);
            obj.Ativo = false;
            _context.palavras.Update(obj);
            _context.SaveChanges();
        }

        
    }
}
