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

        }
        public void Insert(object obj){
            lock(locker){
                ClientsList.Add((Client)obj);
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("New client was successfully inserted");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        public void Select(object obj){
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
        public void Delete(object obj){
            lock(locker){
                foreach(var client in ClientsList){
                    if((int)obj == client.Id){
                        ClientsList.Remove(client);
                    }
                }
            }
        }
        
    }
}
