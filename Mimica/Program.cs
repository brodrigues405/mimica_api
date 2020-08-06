using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Mimica {
    public class Program {
        public static void Main(string[] args) {

            CreateWebHostBuilder(args).Build().Run();
        }

        

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
// 5 grupos  status code
// 1xx informativos
// 2xx sucesso 200 - 201 - 204
// 3xx redirecionamentos
// 4xx erro cliente 400 - 401 - 403 - 404
// 5xx erro servidor