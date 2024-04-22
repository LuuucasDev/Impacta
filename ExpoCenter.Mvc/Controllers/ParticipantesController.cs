using AutoMapper;
using ExpoCenter.Dominio.Entidades;
using ExpoCenter.Mvc.Helpers;
using ExpoCenter.Mvc.Models;
using ExpoCenter.Repositorios.SqlServer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ExpoCenter.Dominio.Entidades.PerfilUsuario; 

namespace ExpoCenter.Mvc.Controllers
{
    [Authorize]
    public class ParticipantesController : Controller
    {
        private readonly ExpoCenterDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ILogger<ParticipantesController> logger;

        public ParticipantesController(ExpoCenterDbContext dbContext, IMapper mapper, ILogger<ParticipantesController> logger)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.logger = logger;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            logger.LogInformation("Entrou no Index");

            return View(mapper.Map<List<ParticipanteViewModel>>(dbContext.Participantes));
        }

        // GET: ParticipantesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParticipantesController/Create
        public ActionResult Create()
        {
            var viewModel = new ParticipanteCreateViewModel();

            viewModel.Eventos = mapper.Map<List<EventoGridViewModel>>(dbContext.Eventos);

            return View(viewModel);
        }

        // POST: ParticipantesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParticipanteCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var participante = mapper.Map<Participante>(viewModel);

                participante.Eventos = new List<Evento>();

                foreach (var evento in viewModel.Eventos.Where(e => e.Selecionado))
                {
                    participante.Eventos.Add(dbContext.Eventos.Find(evento.Id));
                }

                dbContext.Participantes.Add(participante);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //[Authorize(Roles = "Administrador, Gerente")]
        [AuthorizeRole(Administrador, Gerente, Supervisor, Agente)]
        public ActionResult Edit(int id)
        {
            //var participante = dbContext.Participantes.Include(p => p.Eventos).SingleOrDefault(p => p.Id == id);
            var participante = dbContext.Participantes.SingleOrDefault(p => p.Id == id);

            if (participante == null)
            {
                return NotFound();
            }

            var viewModel = mapper.Map<ParticipanteCreateViewModel>(participante);

            viewModel.Eventos = mapper.Map<List<EventoGridViewModel>>(dbContext.Eventos);

            if (participante.Eventos != null)
            {
                foreach (var evento in participante.Eventos)
                {
                    viewModel.Eventos.Single(e => e.Id == evento.Id).Selecionado = true;
                } 
            }

            return View(viewModel);
        }

        // POST: ParticipantesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParticipanteCreateViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var participante = dbContext.Participantes.Find(viewModel.Id);

                if (participante == null)
                {
                    return NotFound();
                }

                dbContext.Entry(participante).CurrentValues.SetValues(viewModel);

                foreach (var evento in viewModel.Eventos)
                {
                    if (evento.Selecionado)
                    {
                        if (participante.Eventos.Any(e => e.Id == evento.Id))
                        {
                            continue;
                        }
                        participante.Eventos.Add(dbContext.Eventos.Find(evento.Id));
                    }
                    else
                    {
                        participante.Eventos.Remove(dbContext.Eventos.Find(evento.Id));
                    }
                }

                dbContext.Update(participante);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Policy = "ParticipantesExcluir")]
        public ActionResult Delete(int id)
        {
            //if (!User.HasClaim("Participantes", "Excluir"))
            //{
            //    return new ForbidResult();
            //}

            return View();
        }

        // POST: ParticipantesController/Delete/5
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
