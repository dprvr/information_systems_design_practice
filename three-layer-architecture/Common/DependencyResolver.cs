using BLL;
using DAL;
using Entities;

namespace Common
{
    public class DependencyResolver
    {
        public static IRepository<Client> IClientsRepository { get; set; } = new ClientsRepository();
        public static IClientsLogic IClientsLogic { get; set; } = new ClientsLogic(IClientsRepository);
       
    }
}
