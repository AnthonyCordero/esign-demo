using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using esignDemo.Services;

namespace esignDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly DocumentService _documentService;
        public HomeController(DocumentService documentService)
        {
            _documentService = documentService;
        }
        public async Task<IActionResult> Index()
        {
            await _documentService.ProcessDocumentAsync();
            return View();
        }

        [HttpPost]
        public IActionResult GenerateDocument()
        {
            // Lógica para generar el documento
            TempData["Message"] = "Documento generado y enviado para firma.";
            return RedirectToAction("Index");
        }
    }
}