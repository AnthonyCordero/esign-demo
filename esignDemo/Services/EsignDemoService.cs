using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SignNowApp.Services
{
    public class SignNowService
    {
        private const string ClientId = "YOUR_CLIENT_ID"; // Reemplaza con tu client ID
        private const string ClientSecret = "YOUR_CLIENT_SECRET"; // Reemplaza con tu client secret
        private const string Username = "YOUR_EMAIL"; // Reemplaza con tu correo electrónico
        private const string Password = "YOUR_PASSWORD"; // Reemplaza con tu contraseña
        private const string ApiBaseUrl = "https://api.signnow.com";

        public async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                var requestBody = new
                {
                    grant_type = "password",
                    client_id = ClientId,
                    client_secret = ClientSecret,
                    username = Username,
                    password = Password
                };

                var response = await client.PostAsync($"{ApiBaseUrl}/oauth2/token",
                    new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                return tokenResponse.access_token;
            }
        }

        public async Task<string> UploadDocument(string accessToken, string filePath)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/pdf");
                form.Add(fileContent, "file", "document.pdf");

                var response = await client.PostAsync($"{ApiBaseUrl}/document", form);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var documentResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                return documentResponse.id;
            }
        }

        public async Task SendDocumentForSignature(string accessToken, string documentId, string recipientEmail, string recipientName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var requestBody = new
                {
                    to = new[]
                    {
                        new
                        {
                            email = recipientEmail,
                            role_id = "signer",
                            role = "Signer",
                            order = 1,
                            name = recipientName
                        }
                    },
                    document_id = documentId,
                    from = Username,
                    subject = "Firma electrónica de contrato",
                    message = "Por favor firme el contrato adjunto."
                };

                var response = await client.PostAsync($"{ApiBaseUrl}/document/{documentId}/invite",
                    new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}