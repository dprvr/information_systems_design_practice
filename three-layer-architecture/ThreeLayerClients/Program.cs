using BLL;
using Common;
using Entities;
using System;

namespace ThreeLayerClients
{
    class Program
    {
        static void Main(string[] args)
        {
            IClientsLogic clients = DependencyResolver.IClientsLogic;

            Console.WriteLine("GetAll:");
            foreach (Client client in clients.GetAll())
            {
                Console.WriteLine(client.ToString());
            }
            Console.WriteLine("##########GetAll\n");

            Console.WriteLine("Get[id = 2]:");
            Console.WriteLine(clients.GetClient(2));
            Console.WriteLine("##########Get\n");

            Console.WriteLine("GroupBy");
            foreach(var group in clients.GroupByCity(clients.GetAll()))
            {
                Console.WriteLine("Group[]::");
                foreach (var client in group)
                {
                    Console.WriteLine(client);
                }
            }            
            Console.WriteLine("##########GroupBy\n");

            Console.WriteLine("ADD[]:");
            Client client1 = clients.GetClient(3);
            client1.City = "Токио";           
            Console.WriteLine($"\nADDed = {client1}");
            clients.AddClient(client1);
            Console.WriteLine("GetAll:");
            foreach (Client client in clients.GetAll())
            {
                Console.WriteLine(client.ToString());
            }
            Console.WriteLine("##########GetAll");
            Console.WriteLine("##########ADD[]::\n");

            //Console.WriteLine("Delete[id = 5]:");
            //Console.WriteLine("GetAll:");
            //foreach (Client client in clients.GetAll())
            //{
            //    Console.WriteLine(client.ToString());
            //}
            //Console.WriteLine("##########GetAll");
            //clients.DeleteClient(6);
            //Console.WriteLine("##########After[]::");
            //Console.WriteLine("GetAll:");
            //foreach (Client client in clients.GetAll())
            //{
            //    Console.WriteLine(client.ToString());
            //}
            //Console.WriteLine("##########GetAll");
            //Console.WriteLine("##########ADD[]::\n");


            Console.WriteLine("Update[2]:");
            Client client2 = clients.GetClient(2);
            client2.City = "Токио";
            Console.WriteLine($"\nUpdating = {client2}");
            clients.UpdateClient(client2);
            Console.WriteLine("GetAll:");
            foreach (Client client in clients.GetAll())
            {
                Console.WriteLine(client.ToString());
            }
            Console.WriteLine("##########GetAll");
            Console.WriteLine("##########Update[]::\n");


            Console.WriteLine("@@@@@@@tap@@@@@@@");
            Console.ReadKey();
        }
    }
}
