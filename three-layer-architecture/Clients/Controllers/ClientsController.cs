using BLL;
using Clients.Models;
using Entities;
using System;
using System.Web.Mvc;

namespace Clients.Controllers
{
    public class ClientsController : Controller
    {
        IClientsLogic ClientsLogic = Common.DependencyResolver.IClientsLogic;
        // GET: Clients

        [HttpGet]
        public ActionResult Index()
        {
            ClientsViewModel clientsViewModel = new ClientsViewModel(ClientsLogic.GetAll());                        
            return View(clientsViewModel);
        }

        [HttpPost]
        [Route(Name = "Index")]
        public ActionResult Index(ClientsViewModel clientsViewModel)
        {
            clientsViewModel.IE_Clients = ClientsLogic.GetAll();
            if (!String.IsNullOrEmpty(clientsViewModel.SearchString))
            {                
                clientsViewModel.IE_Clients = ClientsLogic.SearchByLastName(clientsViewModel.SearchString);
            }           
            if (clientsViewModel.IsChecked)
            {
                clientsViewModel.IG_Clients = ClientsLogic.GroupByCity(clientsViewModel.IE_Clients);                
            }          
            return View(clientsViewModel);
        }


        [HttpGet]
        public ActionResult AddClient()
        {
            return View();
        }

        [HttpPost]
        [Route(Name = "AddClient")]
        public ActionResult AddClient(Client client)
        {
            if (ModelState.IsValid)
            {
                ClientsLogic.AddClient(client);
                return RedirectToAction("Index");
            }
            return View(client);

        }

        [HttpGet]
        public ActionResult DeleteClient(int ID)
        {
            ClientsLogic.DeleteClient(ID);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditClient(int id)
        {
            Client EditableClient = ClientsLogic.GetClient(id);
            if (EditableClient == null)
            {
                return HttpNotFound();
            }
            return View(EditableClient);
        }

        [HttpPost]
        public ActionResult EditClient(Client EditableClient)
        {
            if (ModelState.IsValid)
            {
                ClientsLogic.UpdateClient(EditableClient);
                return RedirectToAction("Index");
            }
            return View(EditableClient);
        }

        [HttpGet]
        public ActionResult DetailsClient(int ID)
        {           
            Client client = ClientsLogic.GetClient(ID);
            if(client == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(client);
            }
        }
        



    }
}