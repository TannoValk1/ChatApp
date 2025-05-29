using System;
using ConsoleChatApp.Net;

namespace ConsoleChatApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Server moto = new Server();

            Console.Write("Enter Name: ");
            string Username = Console.ReadLine();
            moto.ConnectToServer(Username);
            Console.Write("Enter Message: ");

            moto.msgReceivedEvent += () =>
            {
                var msg = moto.PacketReader.ReadMessage();
                Console.WriteLine("Received: " + msg);
                Console.Write("Enter Message: ");
            };

            while (true)
            {
                string Msg = Console.ReadLine();
                moto.SendMessageToServer(Msg);
            }
        }
    }
}