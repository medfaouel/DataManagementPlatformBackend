using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace PFEmvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckDetailsController : Controller
    {
        private readonly DbContextApp _context;
        CheckDetailsController(DbContextApp context)
        {
            _context = context;
        }
       
    }
}
