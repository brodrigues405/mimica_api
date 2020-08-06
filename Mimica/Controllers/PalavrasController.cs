using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mimica.infra;
using Mimica.Models;
using Mimica.Repositories.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.Controllers {
    [Route("api/palavras")]
    public class PalavrasController : ControllerBase {

        //private readonly MimicaContext _context;
        private readonly IPalavraRepository _repositoty;
        private readonly IMapper _mapper;
        public PalavrasController(IPalavraRepository repository, IMapper mapper) {
            _repositoty = repository;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public IActionResult ObterTodas(DateTime? data, int? pagNumero, int? pagQtdRegistro) {

            var item = _repositoty.ObterPalavras(data, pagNumero, pagQtdRegistro);

            if (pagNumero != null) {

                if (pagNumero > item.paginacao.TotalPaginas) {
                    return NotFound();
                }

            }
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(item.paginacao));
            return Ok(item.ToList());
        }





        //[Route("{id}", Name = "PalavraPorID")]
        [HttpGet("{id}", Name = "ObterPalavra")]
        public IActionResult Obter(int id) {

            try {
                var v = _repositoty.Obter(id);
                if (v == null) {
                    return NotFound();
                    //return StatusCode(404);
                } else {
                    PalavraDTO pDTO = _mapper.Map<Palavra, PalavraDTO>(v);
                    pDTO.links = new List<LinkDTO>();
                    pDTO.links.Add(new LinkDTO("self", Url.Link("ObterPalavra", new { id = pDTO.Id }), "GET"));
                    pDTO.links.Add(new LinkDTO("update", Url.Link("AtualizarPalavra", new { id = pDTO.Id }), "PUT"));
                    pDTO.links.Add(new LinkDTO("delete", Url.Link("removerPalavra", new { id = pDTO.Id }), "DELETE"));

                    return Ok(pDTO);



                }
            } catch (Exception ex) {
                return StatusCode(500, "Internal Server Erro" + ex.Message);
            }

        }

        [Route("")]
        [HttpPost]
        public IActionResult Cadastrar([FromBody] Palavra palavra) {

            try {
                if (palavra == null) {
                    return BadRequest("Parametro Nulo!");
                } else if (!ModelState.IsValid) {
                    return BadRequest("Dado Inválido!");
                } else {
                    _repositoty.Cadastrar(palavra);
                    return Created($"/api/palavras/ {palavra.Id}", palavra);
                    //return CreatedAtRoute("PalavraPorID", new { id = palavra.Id }, palavra);
                }

            } catch (Exception ex) {

                return StatusCode(500, "Internal Server Erro" + ex.Message);
            }
        }

        //[Route("{id}")]
        [HttpPut("{id}", Name = "AtualizarPalavra")]
        public IActionResult Atualizar(int id, [FromBody] Palavra palavra) {
            palavra.Id = id;
            try {
                if (palavra == null) {
                    return BadRequest();
                } else if (!ModelState.IsValid) {
                    return BadRequest();
                } else {
                    //var obj = _context.palavras.Find(id);
                    var obj = _repositoty.Obter(id);
                    if (obj == null) {
                        return NotFound();
                    }
                    palavra.Id = id;
                    _repositoty.Atualizar(palavra);
                    return NoContent();
                }

            } catch (Exception ex) {

                return StatusCode(500, "Solicitação invalida" + ex.Message);
            }

        }

        //[Route("{id}")]
        [HttpDelete("{id}", Name = "removerPalavra")]
        public IActionResult Deletar(int id) {
            var palavra = _repositoty.Obter(id);

            try {
                if (palavra == null) {
                    return NotFound();
                }
            } catch (Exception ex) {

                return StatusCode(500, "Erro Interno: " + ex.Message);
            }
            _repositoty.Deletar(id);
            return NoContent();

        }




    }
}
