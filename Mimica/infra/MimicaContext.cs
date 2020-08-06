using Microsoft.EntityFrameworkCore;
using Mimica.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mimica.infra {
    public class MimicaContext : DbContext{

        public MimicaContext(DbContextOptions<MimicaContext> options) : base(options) {

        }
        public DbSet<Palavra> palavras { get; set; }
    }
}
