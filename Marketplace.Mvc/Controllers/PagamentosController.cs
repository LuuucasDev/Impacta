using Marketplace.Mvc.Models;
using Marketplace.Repositorios.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Marketplace.Mvc.Controllers
{
    public class PagamentosController : Controller
    {
        //ToDo parametrizar o BaseAddress
        private readonly PagamentoRepositorio pagamentoRepositorio = new PagamentoRepositorio("http://localhost:56719//api");
        // GET: Pagamentos
        public async Task<ActionResult> Index(Guid? guidCartao)
        {
            if (!guidCartao.HasValue)
            {
                return View (new List<PagamentoIndexViewModel>());
            }

            TempData["guidCartao"] = guidCartao;

            return View(PagamentoIndexViewModel.Mapear(await pagamentoRepositorio.ObterPorCartao(guidCartao.Value)));
        }

        // GET: Pagamentos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pagamentos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pagamentos/Create
        [HttpPost]
        public async Task<ActionResult> Create(PagamentoCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var pagamentoResponse = await pagamentoRepositorio.Post(PagamentoCreateViewModel.Mapear(viewModel));

                if (pagamentoResponse.Status != (int)StatusPagamento.PagamentoOK)
                {
                    ModelState.AddModelError("", pagamentoResponse.MensagemStatus);

                    return View(viewModel);
                }

                return RedirectToAction("Index", new {guidCartao = TempData["guidCartao"]});
            }
            catch
            {
                return View();
            }
        }

        // GET: Pagamentos/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pagamentos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pagamentos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pagamentos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
