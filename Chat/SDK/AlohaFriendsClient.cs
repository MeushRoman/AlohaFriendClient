using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using WebSocketSharp;

using System.Text;
using System.Threading;

namespace Chat.SDK
{
    public enum ChatRoomType
    {
        UserToUserChat,
        GroupChat
    }

    public class AlohaFriendsClient
    {
        private string _sessionId;
        private string _phoneNumber;
        private readonly Uri _urlBase = new Uri("http://localhost:15100/");        

        public string getMyPhoneNumber()
        {
            return _phoneNumber;
        }
        public bool SignUp(string phoneNumber)
        {
            var obj = new
            {
                PhoneNumber = phoneNumber
            };

            var query = _urlBase + "app/sign-up";

            var jsonObject = JsonConvert.SerializeObject(obj);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(query, content).Result;

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    string body = response.Content.ReadAsStringAsync().Result;
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
        public bool SignIn(string phoneNumber, string password)
        {
            var obj = new
            {
                PhoneNumber = phoneNumber,
                Password = password
            };

            var query = _urlBase + "app/sign-in";

            var jsonObject = JsonConvert.SerializeObject(obj);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(query, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string body = response.Content.ReadAsStringAsync().Result;
 
                    _sessionId = body;
                    _phoneNumber = phoneNumber;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public int CreateChatRoom(string title, ChatRoomType type)
        {
            var obj = new
            {
                Title = title,
                Type = type,
                SessionId = _sessionId
            };

            var query = _urlBase + "app/new-chat-room";

            var jsonObject = JsonConvert.SerializeObject(obj);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(query, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return Int32.Parse(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return -1;
                }
            }
        }
        public bool ChatRoomJunction(int chatRoomId, string userPhoneNumber)
        {
            var obj = new
            {             
                SessionId = _sessionId,
                ChatRoomId = chatRoomId,
                UserPhoneNumber = userPhoneNumber
            };

            var query = _urlBase + "app/junction";

            var jsonObject = JsonConvert.SerializeObject(obj);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(query, content).Result;
 
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<string> GetUsers()
        {
            var obj = new
            {
                SessionId = _sessionId                
            };

            var query = _urlBase + "app/get-users";
            var jsonObject = JsonConvert.SerializeObject(obj);

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(query, content).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var body = response.Content.ReadAsStringAsync().Result;
                    var users = JsonConvert.DeserializeObject<List<string>>(body);

                    return users;
                }
                else
                {
                    return null;
                }
            }
           
        }

        static void OnMessage(object sender, MessageEventArgs args)
        {
            Console.WriteLine(args.Data);
        }
        public void SendMessage(int chatRoom, string body)
        {

            var obj = new
            {
                ChatRoom = chatRoom,
                Body = body
            };

            var jsonObject = JsonConvert.SerializeObject(obj);

            using (var ws = new WebSocket("ws://localhost:15200"))
            {
                ws.OnMessage += (sender, e) => OnMessage(sender, e);

                ws.Connect();

                ws.Send(body);                
            }
        }
    }
}
