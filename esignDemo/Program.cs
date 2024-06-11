using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using esignDemo.Data;
using esignDemo.Services;

namespace esignDemo
{
    class Program
    {
                public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        // static async Task Main(string[] args)
        // {
        //     using (var db = new AppDbContext())
        //     {
        //         var customer = db.Customers.FirstOrDefault();

        //         if (customer == null)
        //         {
        //             Console.WriteLine("No se encontró ningún cliente.");
        //             return;
        //         }

        //         var documentGenerator = new DocumentGenerator();
        //         string documentPath = documentGenerator.GenerateDocument(customer);

        //         var signNowService = new SignNowService();

        //         string accessToken = await signNowService.GetAccessToken();

        //         if (!string.IsNullOrEmpty(accessToken))
        //         {
        //             string documentId = await signNowService.UploadDocument(accessToken, documentPath);

        //             if (!string.IsNullOrEmpty(documentId))
        //             {
        //                 await signNowService.SendDocumentForSignature(accessToken, documentId, customer.Email, customer.Name);
        //                 Console.WriteLine("Documento enviado para firma.");
        //             }
        //         }
        //     }
        // }
    }
}