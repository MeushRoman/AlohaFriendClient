using Chat.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Frame _BaseFrame;
        public static AlohaFriendsClient alohaFriendsClient = new AlohaFriendsClient();

        public MainWindow()
        {
            InitializeComponent();
            BaseFrame.Source = new Uri("pages/SigInPage.xaml", UriKind.RelativeOrAbsolute);
            _BaseFrame = BaseFrame;
        }
    }
}
