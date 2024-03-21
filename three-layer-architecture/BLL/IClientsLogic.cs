using Entities;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public interface IClientsLogic
    {
        void AddClient(Client client);
        void UpdateClient(Client NewClientsDate);
        void DeleteClient(int ClientID);
        Client GetClient(int ClientID);
        IEnumerable<Client> GetAll();
        IEnumerable<Client> SearchByLastName(string LastName);
        IEnumerable<IGrouping<string, Client>> GroupByCity(IEnumerable<Client> Clients);
    }
}
