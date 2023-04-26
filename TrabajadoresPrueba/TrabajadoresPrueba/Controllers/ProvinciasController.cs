using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrabajadoresPrueba.Data;
using TrabajadoresPrueba.Modelos;

namespace TrabajadoresPrueba.Controllers
{
    public class ProvinciasController : Controller
    {
        private readonly DataContext _context;
        public ProvinciasController(DataContext dataContext)
        {
            _context = dataContext;
        }
        public async Task<IActionResult> Index(int id)
        {
            var provincias = await _context.Provincia.Where(t => t.IdDepartamento.Equals(id)).ToListAsync();
            var modelDepartamento = await _context.Departamento.FindAsync(id);
            ViewBag.Departamento = modelDepartamento;
            return View(provincias);
        }
        public IActionResult Create(int idDepartamento)
        {
            var provincia = new Provincia { IdDepartamento = idDepartamento };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Provincia Model)
        {
            await _context.Provincia.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new {id = Model.IdDepartamento});
        }
    }
}
