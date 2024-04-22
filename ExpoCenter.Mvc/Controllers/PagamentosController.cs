using ExpoCenter.Dominio.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpoCenter.Mvc.Controllers
{
    public class PagamentosController : Controller
    {
        private readonly IPagamentoRepositorio pagamentoRepositorio;

        public PagamentosController(IPagamentoRepositorio pagamentoRepositorio)
        {
            this.pagamentoRepositorio = pagamentoRepositorio;
        }

        public async Task<ActionResult> Index()
        {
            return View(await pagamentoRepositorio.Get());
        }

        // GET: PagamentosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PagamentosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PagamentosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagamentosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PagamentosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PagamentosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PagamentosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
