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
    /// Логика взаимодействия для SigInPage.xaml
    /// </summary>
    public partial class SigInPage : Page
    {
        public SigInPage()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.alohaFriendsClient.SignIn(tbPhoneNumber.Text, pbPassword.Password))
            {
                MainWindow._BaseFrame.Source = new Uri("Pages/MainPage.xaml", UriKind.RelativeOrAbsolute);
            }

        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            MainWindow._BaseFrame.Source = new Uri("Pages/SignUpPage.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
