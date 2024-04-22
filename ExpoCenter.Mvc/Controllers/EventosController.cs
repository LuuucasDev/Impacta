using AutoMapper;
using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Mvc.Models;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpoCenter.Mvc.Controllers
{
    public class EventosController : Controller
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly IMapper mapper;

        public EventosController(ExpoCenterDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public ActionResult Index()
        {
            return View(mapper.Map<List<EventoViewModel>>(dbContext.Eventos.Include(e => e.Participantes)));
        }

        public ActionResult Participantes(int eventoId)
        {
            var evento = dbContext.Eventos.Find(eventoId);

            var viewModel = mapper.Map<EventoViewModel>(evento);

            viewModel.Participantes = mapper.Map<List<ParticipanteGridViewModel>>(dbContext.Participantes.OrderBy(p => p.Nome));

            if (evento.Participantes != null)
            {
                foreach (var participante in evento.Participantes)
                {
                    viewModel.Participantes.Single(p => p.Id == participante.Id).Selecionado = true;
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Participantes(EventoViewModel viewModel)
        {
            var evento = dbContext.Eventos.Find(viewModel.Id);

            foreach (var participante in viewModel.Participantes)
            {
                if (participante.Selecionado)
                {
                    if (evento.Participantes.Any(p => p.Id == participante.Id))
                    {
                        continue;
                    }

                    evento.Participantes.Add(dbContext.Participantes.Single(p => p.Id == participante.Id));
                }
                else
                {
                    evento.Participantes.Remove(dbContext.Participantes.Single(p => p.Id == participante.Id));
                }
            }
            dbContext.Update(evento);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: EventosController/Details/5
        public ActionResult Details(int id)
        {
            var evento = dbContext.Eventos.SingleOrDefault(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(mapper.Map<EventoViewModel>(evento));
        }

        // GET: EventosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventoViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                dbContext.Eventos.Add(mapper.Map<Evento>(viewModel));
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController/Edit/5
        public ActionResult Edit(int id)
        {
            var evento = dbContext.Eventos.SingleOrDefault(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(mapper.Map<EventoViewModel>(evento));
        }

        // POST: EventosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventoViewModel viewModel)
        {
            try
            {
                if (id != viewModel.Id)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                dbContext.Update(mapper.Map<Evento>(viewModel));
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EventosController/Delete/5
        public ActionResult Delete(int id)
        {
            var evento = dbContext.Eventos.SingleOrDefault(e => e.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(mapper.Map<EventoViewModel>(evento));
        }

        // POST: EventosController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                var evento = dbContext.Eventos.SingleOrDefault(e => e.Id == id);

                if (evento == null)
                {
                    return NotFound();
                }

                dbContext.Eventos.Remove(evento);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
