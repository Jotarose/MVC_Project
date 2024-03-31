using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Amigos.DataAccessLayer;
using Amigos.Models;
using Microsoft.AspNetCore.Localization;

namespace Amigos.Controllers
{
    public class AmigoController : Controller
    {
        private readonly AmigoDBContext _context;

        public AmigoController(AmigoDBContext context)
        {
            _context = context;
        }

        // GET: Amigo - Filtrado por distancia
        public async Task<IActionResult> Index(string longitud, string latitud, string distancia)
        {
            if (_context.Amigos == null)
            {
                return Problem("Entity set Amigos is null.");
            }

            var amigos = from m in _context.Amigos select m;

            List<String> selectedNames = new List<String>();

            if (!String.IsNullOrEmpty(longitud) && !String.IsNullOrEmpty(latitud) && (!String.IsNullOrEmpty(distancia)))
            {
                /*
                 * Si las variables contienen algo, entro aqui
                 */
                Double _distancia = Convert.ToDouble(distancia);
                Double _longitud = Convert.ToDouble(longitud);
                Double _latitud = Convert.ToDouble(latitud);

                Double amigoLongitud = 0;
                Double amigoLatitud = 0;

                Double amigoDistancia = 0;
                Double x = 0;
                Double y = 0;

                foreach (var amigo in amigos)
                {
                    // Los tengo que convertir a doubles para que funcione bien
                    amigoLatitud = Convert.ToDouble(amigo.lati);
                    amigoLongitud = Convert.ToDouble(amigo.longi);

                    x = (_latitud - amigoLatitud) * (_latitud - amigoLatitud);
                    y = (_longitud - amigoLongitud) * (_longitud - amigoLongitud);

                    amigoDistancia = Math.Sqrt(x + y);

                    if (amigoDistancia < _distancia)
                    {
                        if (amigo.name != null)
                        {
                            selectedNames.Add(amigo.name);
                        }
                    }
                }
                amigos = amigos.Where(s => selectedNames.Contains(s.name!));
            }
            return View(await amigos.ToListAsync());

        }

        //POST - Selección de lenguaje
        [HttpPost]
        public IActionResult ManejadorIdioma(string idioma, string urlRetorno)
        {
            //Creación de cookies en base al idioma seleccionado
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(idioma)),
                new CookieOptions { Expires = DateTime.Now.AddDays(1) });

            //return RedirectToAction(nameof(Index));
            return LocalRedirect(urlRetorno);
        }

        // GET: Amigo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Amigos == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (amigo == null)
            {
                return NotFound();
            }

            return View(amigo);
        }

        // GET: Amigo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,name,longi,lati")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(amigo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        // GET: Amigo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Amigos == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigos.FindAsync(id);
            if (amigo == null)
            {
                return NotFound();
            }
            return View(amigo);
        }

        // POST: Amigo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,name,longi,lati")] Amigo amigo)
        {
            if (id != amigo.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(amigo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AmigoExists(amigo.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(amigo);
        }

        // GET: Amigo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Amigos == null)
            {
                return NotFound();
            }

            var amigo = await _context.Amigos
                .FirstOrDefaultAsync(m => m.ID == id);
            if (amigo == null)
            {
                return NotFound();
            }

            return View(amigo);
        }

        // POST: Amigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Amigos == null)
            {
                return Problem("Entity set 'AmigoDBContext.Amigos'  is null.");
            }
            var amigo = await _context.Amigos.FindAsync(id);
            if (amigo != null)
            {
                _context.Amigos.Remove(amigo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AmigoExists(int id)
        {
            return (_context.Amigos?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
