using System;
using System.Collections.Generic;
using System.Threading;

namespace HomeWork_29_04
{
    class Program
    {
        static object locker = new Object(); 
        public static List<Client> ClientsList = new List<Client>();
        static void Main(string[] args)
        {
            TimerCallback start = new TimerCallback(Difference);
            Thread insertThread = new Thread( new ParameterizedThreadStart (Insert));
            Thread selectThread = new Thread(new ThreadStart(Select));
            Thread deleteThread = new Thread(new ParameterizedThreadStart(Delete));
            Thread updateThread = new Thread(new ParameterizedThreadStart(Update));
            Thread selectByIdThread = new Thread(new ParameterizedThreadStart(SelectById));
            System.Console.WriteLine("Hello! Welcom to client server!\nHere you can");
            while(true){
            System.Console.WriteLine("1.Insert client\n2.Select all clients\n3.Select client bi id\n4.delete client\n5.update clients balance");
            switch(Console.ReadLine()){
                case "1": {
                    Client client = new Client();
                    System.Console.Write("enter firstname: ");
                    client.Firstname = Console.ReadLine();
                    System.Console.Write("enter middlename: ");
                    client.Middlename = Console.ReadLine();
                    System.Console.Write("enter lastname: ");
                    client.Lastname = Console.ReadLine();
                    System.Console.Write("enter balance: ");
                    client.Balance = decimal.Parse(Console.ReadLine());
                    insertThread.Start(client);
                };break;
                case "2": selectThread.Start();break;
                case "3": {
                    System.Console.Write("enter id: ");
                    object id = Console.ReadLine();
                    selectByIdThread.Start(id);
                };break;
                case "4": {
                    System.Console.Write("enter id: ");
                    object id = Console.ReadLine();
                    deleteThread.Start(id);
                };break;
                case "5": {
                    System.Console.Write("enter id: ");
                    object id = Console.ReadLine();
                    decimal oldBalance = 0, newBalance = 0;
                    foreach(var client in ClientsList){
                        if (client.Id == (int)id)
                          oldBalance = client.Balance;
                    }
                    updateThread.Start(id);
                    foreach(var client in ClientsList){
                    if((int)id == client.Id){
                        newBalance = client.Balance;
                    }
                    else break;
                    decimal[] arr = {oldBalance, newBalance, (decimal)id};
                    Timer timer = new Timer(start, arr, 2000, 20000);
                    }
                };break;
            }
            System.Console.WriteLine("Do you want to continue?y/n");
            if(Console.ReadLine() == "y") continue;
            else break;
            }
            System.Console.WriteLine("Bye");
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
                foreach(var client in ClientsList){
                    if((int)obj == client.Id){
                        System.Console.WriteLine(client.Id);
                        System.Console.WriteLine(client.Firstname);
                        System.Console.WriteLine(client.Middlename);
                        System.Console.WriteLine(client.Lastname);
                        System.Console.WriteLine(client.Balance);
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Client with such id doesn'n exist");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        public static void Select(){
            lock(locker){
                foreach(var client in ClientsList){                    
                    System.Console.WriteLine(client.Id);
                    System.Console.WriteLine(client.Firstname);
                    System.Console.WriteLine(client.Middlename);
                    System.Console.WriteLine(client.Lastname);
                    System.Console.WriteLine(client.Balance);
                }
            }
        }
        public static void Delete(object obj){
            lock(locker){
                foreach(var client in ClientsList){
                    if((int)obj == client.Id){
                        ClientsList.Remove(client);
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Client with such id was removed");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else{
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Client with such id doesn'n exist");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        public static void Update(object obj){
            lock(locker){
                foreach(var client in ClientsList){
                    if((int)obj == client.Id){
                        System.Console.Write("Enter new balance: ");
                        client.Balance = decimal.Parse(Console.ReadLine());
                    }
                    else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Client with such id doesn'n exist");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
        public static void Difference(object obj){
            decimal[] balance = (decimal[])obj;
            if (balance[0] > balance[1]){
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine($"Id: {balance[3]}");
                System.Console.WriteLine($"Past Balance: {balance[0]}");
                System.Console.WriteLine($"New Balance: {balance[1]}");
                System.Console.WriteLine($"Status: {balance[0] - balance[1]}");
            }
            else if(balance[0] < balance[1]){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($"Id: {balance[3]}");
                System.Console.WriteLine($"Past Balance: {balance[0]}");
                System.Console.WriteLine($"New Balance: {balance[1]}");
                System.Console.WriteLine($"Status: +{balance[0] - balance[1]}");
            }
        }
    }
}
