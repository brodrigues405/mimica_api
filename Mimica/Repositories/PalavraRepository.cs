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

        public PaginationList<Palavra> ObterPalavras(DateTime? data, int? pagNumero, int? pagQtdRegistro) {
            

            var lista = new PaginationList<Palavra>();
            var item = _context.palavras.AsNoTracking().AsQueryable();
            if (data.HasValue) {
                item = item.Where(a => a.Criado > data.Value || a.Atualizado > data.Value);
            }

            if (pagNumero.HasValue) {
                var qtdRegistros = item.Count();
                item = item.Skip((pagNumero.Value - 1) * pagQtdRegistro.Value).Take(pagQtdRegistro.Value);
                  
                var paginacao = new Paginacao();
                paginacao.numeroPagina = pagNumero.Value;
                paginacao.RegistroPoPagina = pagQtdRegistro.Value;
                paginacao.TotalRegistros = qtdRegistros;
                paginacao.TotalPaginas = (int)Math.Ceiling((double)qtdRegistros / pagQtdRegistro.Value);
                
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
