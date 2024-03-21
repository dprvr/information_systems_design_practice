using System.Collections.Generic;
using System.Linq;
using Entities;
using DAL;
using System;

namespace BLL
{
    public class ClientsLogic : IClientsLogic
    {
        private IRepository<Client> db;

        public ClientsLogic(IRepository<Client> db)
        {
            this.db = db;
        }

        public void AddClient(Client client)
        {
            db.Create(client);
        }

        public void DeleteClient(int ClientID)
        {
            db.Delete(ClientID);
        }

        public IEnumerable<Client> GetAll()
        {
            return db.GetAll();
        }

        public Client GetClient(int ClientID)
        {
            return db.Get(ClientID);
        }

        public IEnumerable<IGrouping<string, Client>> GroupByCity(IEnumerable<Client> Clients)
        {            
            IEnumerable<IGrouping<string, Client>> CombinedClients = from client in Clients group client by client.City;
            return CombinedClients;
        }

        public IEnumerable<Client> SearchByLastName(string LastName)
        {            
            var Searched = GetAll().ToList().FindAll(client =>
            { return client.LastName.IndexOf(LastName, StringComparison.OrdinalIgnoreCase) >= 0; });
            return Searched;            
        }

        public void UpdateClient(Client NewClientsData)
        {
            db.Update(NewClientsData);
        }
    }
}
