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

namespace Chat.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();              


        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.alohaFriendsClient.SignUp(tbPhoneNumber.Text))
            {
                MainWindow._BaseFrame.Source = new Uri("pages/SigInPage.xaml", UriKind.RelativeOrAbsolute);
            }
        }
        
    }
}
