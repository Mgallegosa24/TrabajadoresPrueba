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
            return View(provincia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Provincia Model)
        {
            await _context.Provincia.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new {id = Model.IdDepartamento});
        }

        public async Task<IActionResult> Edit(int id)
        {
            var provincia = await _context.Provincia.FindAsync(id);
            return View(provincia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Provincia Model)
        {
            var provinciaOld = await _context.Provincia.FindAsync(Model.Id);
            provinciaOld.NombreProvincia = Model.NombreProvincia;
            _context.Update(provinciaOld);
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new { id = Model.IdDepartamento });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var idDepartamento = 0;
            var provincia = await _context.Provincia.FindAsync(id);

            if (provincia != null) {
                idDepartamento = provincia.IdDepartamento;
                _context.Remove(provincia);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = idDepartamento });

        }
    }
}
