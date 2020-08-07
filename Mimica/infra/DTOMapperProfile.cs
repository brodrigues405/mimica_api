using AutoMapper;
using Mimica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.infra {
    public class DTOMapperProfile : Profile{

        public DTOMapperProfile() {
            CreateMap<Palavra, PalavraDTO>();
            CreateMap<PaginationList<Palavra>, PaginationList<PalavraDTO>>();
        }

    }
}
