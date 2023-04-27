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
        public IActionResult Create(int idProvincia)
        {
            var provincia = new Distrito { IdProvincia = idProvincia };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Distrito Model)
        {
            await _context.Distrito.AddAsync(Model);   //Insert into
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new { id = Model.IdProvincia });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var distrito = await _context.Distrito.FindAsync(id);
            return View(distrito);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Distrito Model)
        {
            var distritoOld = await _context.Distrito.FindAsync(Model.Id);
            distritoOld.NombreDistrito = Model.NombreDistrito;
            _context.Update(distritoOld);
            await _context.SaveChangesAsync();     //Commit a la bd
            return RedirectToAction("Index", new { id = Model.IdProvincia});
        }

        public async Task<IActionResult> Delete(int id)
        {
            var idProvincia = 0;
            var distrito = await _context.Distrito.FindAsync(id);

            if (distrito != null)
            {
                idProvincia = distrito.IdProvincia;
                _context.Remove(distrito);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = idProvincia });

        }
    }
}
