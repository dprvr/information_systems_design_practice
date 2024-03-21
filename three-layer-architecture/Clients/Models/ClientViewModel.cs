using Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Clients.Models
{
    public class ClientsViewModel
    {
        public IEnumerable<Client> IE_Clients { get; set; }
        public IEnumerable<IGrouping<string, Client>> IG_Clients { get; set; }
        public bool IsChecked { get; set; }
        [StringLength(20)]
        public string SearchString { get; set; }

        public ClientsViewModel()
        {
            IsChecked = false;
        }

        public ClientsViewModel(IEnumerable<Client> clients)
        {
            IE_Clients = clients;
            IG_Clients = null;
            IsChecked = false;
            SearchString = "";
        }



    }
}