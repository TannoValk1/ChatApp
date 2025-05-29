using ConsoleChatApp.Net;
using ConsoleChatApp.MVVM.Model;
using ConsoleChatApp.Net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ConsoleChatApp.MVVM.ViewModel
{
    class MainViewModel
    {
        public ObservableCollection<UserModel1> Users { get; set; }
        public ObservableCollection<string> Messages { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        private Server _server;
        public MainViewModel()
        {
            Users = new ObservableCollection<UserModel1>();
            Messages = new ObservableCollection<string>();

            _server = new Server();
            _server.connectedEvent += UserConnected;
            _server.msgReceivedEvent += MessageRecived;
            _server.userDisconnectEvent += RemoveUser;
        }

        private void RemoveUser()
        {
            var uid = _server.PacketReader.ReadMessage();
            var user = Users.Where(x => x.UID == uid).FirstOrDefault();
            Users.Remove(user);
        }

        private void MessageRecived()
        {
            var msg = _server.PacketReader.ReadMessage();
            Messages.Add(msg);
        }

        private void UserConnected()
        {
            var user = new UserModel1
            {
                Username = _server.PacketReader.ReadMessage(),
                UID = _server.PacketReader.ReadMessage(),
            };

            if (!Users.Any(x => x.UID == user.UID))
            {
                Users.Add(user);
            }
        }
    }
}