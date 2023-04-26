using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Modelos;

namespace TrabajadoresPrueba.Controllers
{
    public class DistritosController : Controller
    {
        private readonly DataContext _context;
        public DistritosController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<IActionResult> Index(int id)
        {
            var distrito =await _context.Distrito.Where(x => x.IdProvincia.Equals(id)).ToListAsync();
            var modelProvincia = await _context.Provincia.FindAsync(id);
            ViewBag.Provincia = modelProvincia;
            var departamentos = await _context.Departamento.FindAsync(modelProvincia.IdDepartamento);
            ViewBag.Departamento = departamentos;
            return View(distrito);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departamento Model)
        {
            _context.AddAsync(Model);   //Insert into
            _context.SaveChanges();     //Commit a la bd
            return RedirectToAction(nameof(Index));
        }
    }
}
