using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using esignDemo.Services;

namespace esignDemo.Controllers
{
    public class DocumentController : Controller
    {
        private readonly DocumentService _documentService;

        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            await _documentService.ProcessDocumentAsync();
            return View();
        }
    }
}
