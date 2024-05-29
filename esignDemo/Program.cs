using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SignNowApp.Data;
using SignNowApp.Services;

namespace SignNowApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                var customer = db.Customers.FirstOrDefault();

                if (customer == null)
                {
                    Console.WriteLine("No se encontró ningún cliente.");
                    return;
                }

                var signNowService = new SignNowService();

                string accessToken = await signNowService.GetAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    string documentId = await signNowService.UploadDocument(accessToken, "path/to/your/document.pdf");

                    if (!string.IsNullOrEmpty(documentId))
                    {
                        await signNowService.SendDocumentForSignature(accessToken, documentId, customer.Email, customer.Name);
                        Console.WriteLine("Documento enviado para firma.");
                    }
                }
            }
        }
    }
}