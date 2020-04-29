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
            Thread selectThread = new Thread(new ParameterizedThreadStart(Select));
            Thread deleteThread = new Thread(new ParameterizedThreadStart(Delete));
            Thread updateThread = new Thread(new ParameterizedThreadStart(Update));

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
        public static void Select(object obj){
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
                        System.Console.Write("enter firstname: ");
                        client.Firstname = Console.ReadLine();
                        System.Console.Write("enter middlename: ");
                        client.Middlename = Console.ReadLine();
                        System.Console.Write("enter lastname: ");
                        client.Lastname = Console.ReadLine();
                        System.Console.Write("enter balance: ");
                        client.Balance = decimal.Parse(Console.ReadLine());
                    }
                }
            }
        }
    }
}
