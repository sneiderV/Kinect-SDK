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

namespace KinectTwitter
{
    /// <summary>
    /// Lógica de interacción para TweetK.xaml
    /// </summary>
    public partial class TweetK : UserControl
    {
        public TweetK(String nombre, String userID, String contentTweet, String fecha )
        {
            InitializeComponent();
            this.userName.Content = nombre;
            this.userId.Content = userID;
            this.contentTweet.Text = contentTweet;
            this.date.Content = fecha;
        }
    }
}
