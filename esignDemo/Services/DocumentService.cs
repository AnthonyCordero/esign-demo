using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using esignDemo.Data;

namespace esignDemo.Services
{
   public class DocumentService
   {
      private readonly AppDbContext _dbContext;
      private readonly DocumentGenerator _documentGenerator;
      private readonly SignNowService _signNowService;

      public DocumentService(AppDbContext dbContext, DocumentGenerator documentGenerator, SignNowService signNowService)
      {
         _dbContext = dbContext;
         _documentGenerator = documentGenerator;
         _signNowService = signNowService;
      }

      public async Task ProcessDocumentAsync()
      {
         var customer = _dbContext.Customers.FirstOrDefault();

         if (customer == null)
         {
            Console.WriteLine("No se encontró ningún cliente.");
            return;
         }

         string documentPath = _documentGenerator.GenerateDocument(customer);
         string accessToken = await _signNowService.GetAccessToken();

         if (!string.IsNullOrEmpty(accessToken))
         {
            string documentId = await _signNowService.UploadDocument(accessToken, documentPath);

               if (!string.IsNullOrEmpty(documentId))
               {
                  await _signNowService.SendDocumentForSignature(accessToken, documentId, customer.Email, customer.Name);
                  Console.WriteLine("Documento enviado para firma.");
               }
         }
      }
   }
}
