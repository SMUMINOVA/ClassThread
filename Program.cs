using System;
using System.Collections.Generic;
using System.Threading;

namespace HomeWork_29_04
{
    class Program
    {
        static object locker = new Object(); 
        public static List<Client> ClientsList = new List<Client>();
        public static int id{get;set;} = 0;
        static void Main(string[] args)
        {
            TimerCallback start = new TimerCallback(Difference);
            Thread insertThread = new Thread( new ParameterizedThreadStart (Insert));
            Thread selectThread = new Thread(new ThreadStart(Select));
            Thread deleteThread = new Thread(new ParameterizedThreadStart(Delete));
            Thread updateThread = new Thread(new ParameterizedThreadStart(Update));
            Thread selectByIdThread = new Thread(new ParameterizedThreadStart(SelectById));
            Client firstClient = new Client("John", "James", "Peter", id++);
            firstClient.ClientsBalance = new Balance();
            firstClient.ClientsBalance.OldBalance = 1200;
            firstClient.ClientsBalance.NewBalance = firstClient.ClientsBalance.OldBalance;
            ClientsList.Add(firstClient);
            firstClient = new Client("Billy", "Markus", "Bob", id++);
            firstClient.ClientsBalance = new Balance();
            firstClient.ClientsBalance.OldBalance = 7300;
            firstClient.ClientsBalance.NewBalance = firstClient.ClientsBalance.OldBalance;
            ClientsList.Add(firstClient);
            firstClient = new Client("Lara", "James", "Markus", id++);
            firstClient.ClientsBalance = new Balance();
            firstClient.ClientsBalance.OldBalance = 3800;
            firstClient.ClientsBalance.NewBalance = firstClient.ClientsBalance.OldBalance;
            ClientsList.Add(firstClient);
            foreach(var client in ClientsList){                    
                System.Console.WriteLine($"Id: {client.Id}");
                System.Console.WriteLine($"Firstname: {client.Firstname}");
                System.Console.WriteLine($"Middlename: {client.Middlename}");
                System.Console.WriteLine($"Lastname: {client.Lastname}");
                System.Console.WriteLine($"Balance: {client.ClientsBalance.OldBalance}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("\nHello! Welcom to client server!\nHere you can");
            Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine("1.Insert client\n2.Select all clients\n3.Select client by id\n4.delete client\n5.update clients balance");
            string choice = Console.ReadLine();
            switch(choice){
                case "1": {
                    System.Console.Write("enter firstname: ");
                    firstClient.Firstname = Console.ReadLine();
                    System.Console.Write("enter middlename: ");
                    firstClient.Middlename = Console.ReadLine();
                    System.Console.Write("enter lastname: ");
                    firstClient.Lastname = Console.ReadLine();
                    System.Console.Write("enter balance: ");
                    firstClient.ClientsBalance.OldBalance = decimal.Parse(Console.ReadLine());
                    firstClient.ClientsBalance.NewBalance = firstClient.ClientsBalance.OldBalance;
                    firstClient.Id = id++;
                    insertThread.Start(firstClient);
                };break;
                case "2": {
                    selectThread.Start();
                    };break;
                case "3": {
                    System.Console.Write("enter id: ");
                    object id = Console.ReadLine();
                    selectByIdThread.Start(id);
                };break;
                case "4": {
                    System.Console.Write("enter id: ");
                    object id = Console.ReadLine();
                    deleteThread.Start(id);
                    deleteThread.Join();
                    selectThread.Start();
                };break;
                case "5": {
                    System.Console.Write("enter id: ");
                    int id = int.Parse(Console.ReadLine());
                    updateThread.Start(id);
                    Thread.Sleep(5000);
                    foreach(var client in ClientsList){
                        if (client.Id == id)
                        firstClient = client;
                    }
                    decimal[] arr = {firstClient.ClientsBalance.OldBalance, firstClient.ClientsBalance.NewBalance, (decimal)id};
                    Timer timer = new Timer(start, arr, 0, 1000);
                    firstClient.ClientsBalance.OldBalance = firstClient.ClientsBalance.NewBalance;
                };break;
            }
            System.Console.WriteLine("bye");
            Console.ReadLine();
        }
        public static void Insert(object obj){
            lock(locker){
                ClientsList.Add((Client)obj);
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("New client was successfully inserted");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public static void SelectById(object obj){
            lock(locker){
                int id = int.Parse(obj.ToString());
                foreach(var client in ClientsList){
                    if(id == client.Id){
                        System.Console.WriteLine(client.Id);
                        System.Console.WriteLine(client.Firstname);
                        System.Console.WriteLine(client.Middlename);
                        System.Console.WriteLine(client.Lastname);
                        System.Console.WriteLine(client.ClientsBalance.OldBalance);
                    }
                }
            }
        }
        public static void Select(){
            lock(locker){
                foreach(var client in ClientsList){                    
                    System.Console.WriteLine($"Id: {client.Id}");
                    System.Console.WriteLine($"Firstname: {client.Firstname}");
                    System.Console.WriteLine($"Middlename: {client.Middlename}");
                    System.Console.WriteLine($"Lastname: {client.Lastname}");
                    System.Console.WriteLine($"Balance: {client.ClientsBalance.OldBalance}");
                }
            }
        }
        public static void Delete(object obj){
            lock(locker){
                int id = int.Parse(obj.ToString());
                foreach(Client client in ClientsList){
                    if(id == client.Id){
                        ClientsList.Remove(client);
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Client with such id was removed");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        public static void Update(object obj){
            lock(locker){
                int id = int.Parse(obj.ToString());
                foreach(var client in ClientsList){
                    if(id == client.Id){
                        System.Console.Write("Enter new balance: ");
                        client.ClientsBalance.NewBalance = decimal.Parse(Console.ReadLine());
                    }  
                }
            }
        }
        public static void Difference(object obj){
            decimal[] balance = (decimal[])obj;
            if (balance[0] > balance[1]){
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Id: {balance[2]}");
                System.Console.WriteLine($"Past Balance: {balance[0]}");
                System.Console.WriteLine($"New Balance: {balance[1]}");
                System.Console.WriteLine($"Status: {balance[1] - balance[0]}");
                Console.ForegroundColor = ConsoleColor.White;

            }
            else if(balance[0] < balance[1]){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Id: {balance[2]}");
                System.Console.WriteLine($"Past Balance: {balance[0]}");
                System.Console.WriteLine($"New Balance: {balance[1]}");
                System.Console.WriteLine($"Status: + {balance[1] - balance[0]}");
                
                Console.ForegroundColor = ConsoleColor.White;
            }
            balance[0] = balance[1];
        }
    }
}
