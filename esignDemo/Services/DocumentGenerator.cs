using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using esignDemo.Models;

namespace esignDemo.Services
{
    public class DocumentGenerator
    {
        public string GenerateDocument(Customer customer)
        {
            string outputPath = $"GeneratedDocuments/contract_{customer.Id}.pdf";

            // Create directories if they don't exist
            Directory.CreateDirectory("GeneratedDocuments");

            using (PdfWriter writer = new PdfWriter(outputPath))
            using (PdfDocument pdfDoc = new PdfDocument(writer))
            using (Document document = new Document(pdfDoc))
            {
                // Add title
                document.Add(new Paragraph("Contrato de Prestación de Servicios")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20));

                // Add a space
                document.Add(new Paragraph("\n"));

                // Add customer details
                document.Add(new Paragraph($"Nombre: {customer.Name}"));
                document.Add(new Paragraph($"Email: {customer.Email}"));
                document.Add(new Paragraph($"Dirección: {customer.Address}"));
                document.Add(new Paragraph($"Teléfono: {customer.PhoneNumber}"));

                // Add a space
                document.Add(new Paragraph("\n"));

                // Add contract body
                string contractBody = "Este contrato establece los términos y condiciones bajo los cuales " +
                    $"{customer.Name} (en adelante, 'El Cliente') recibirá servicios de nuestra empresa. " +
                    "Las partes acuerdan lo siguiente: \n\n" +
                    "1. Descripción del servicio: [Descripción del servicio].\n" +
                    "2. Duración del contrato: [Duración del contrato].\n" +
                    "3. Pago y facturación: [Detalles del pago y facturación].\n" +
                    "4. Obligaciones del cliente: [Obligaciones del cliente].\n" +
                    "5. Obligaciones del proveedor: [Obligaciones del proveedor].\n" +
                    "6. Terminación del contrato: [Condiciones para la terminación del contrato].\n" +
                    "7. Otros términos y condiciones: [Otros términos y condiciones].\n\n" +
                    "Firmado:\n\n" +
                    "__________________________\n" +
                    "Proveedor\n\n" +
                    "__________________________\n" +
                    "Cliente";

                document.Add(new Paragraph(contractBody));

                document.Close();
            }

            return outputPath;
        }
    }
}
