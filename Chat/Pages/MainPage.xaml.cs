using Chat.SDK;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat.pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private AlohaFriendsClient client = MainWindow.alohaFriendsClient;
        public MainPage()
        {
            InitializeComponent();

            var items = client.GetUsers();

            lvContacts.ItemsSource = items;           
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            lvMessages.Items.Add(new { Message = tbMessage.Text, Time = DateTime.Now });
            tbMessage.Text = "";

            //client.ChatRoomJunction(5, 1);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var user = sender as ListViewItem;

            if (user != null)
            {                
                var room = client.CreateChatRoom(user.ToString(), SDK.ChatRoomType.UserToUserChat);
                var phoneNumber = user.Content.ToString();
                client.ChatRoomJunction(room, phoneNumber);
                client.ChatRoomJunction(room, client.getMyPhoneNumber());

            }
        }
    }
}
