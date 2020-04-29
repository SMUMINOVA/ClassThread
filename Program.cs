using System;
using System.Collections.Generic;
using System.Threading;

namespace HomeWork_29_04
{
    class Program
    {
        static object locker = new Object(); 
        static List<Client> ClientsList;
        static void Main(string[] args)
        {
            Thread insertThread = new Thread( new ParameterizedThreadStart (Insert));
            Thread selectThread = new Thread(new ThreadStart(Select));
            Thread deleteThread = new Thread(new ParameterizedThreadStart(Delete));
            Thread updateThread = new Thread(new ParameterizedThreadStart(Update));
            Thread selectByIdThread = new Thread(new ParameterizedThreadStart(SelectById));
            System.Console.WriteLine("Hello! Welcom to client server!\nHere you can");
            start:
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
                    decimal CatchBalance = 0;
                    foreach(var client in ClientsList){
                        if (client.Id == (int)id)
                          CatchBalance = client.Balance;
                    }
                    updateThread.Start(id);
                };break;
            }
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
                        System.Console.WriteLine("Client with such id don'n exist");
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
                }
            }
        }
    }
}
