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
using Microsoft.Kinect;
using Microsoft.Kinect.Wpf;
using Microsoft.Kinect.Wpf.Controls;

namespace ControlGestureHand
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            //especifico mi region 
            KinectRegion.SetKinectRegion(this, mikinectRegion);
            
            App app = ((App)Application.Current);
            
            //asigno mi kinect a mi region
            mikinectRegion.KinectSensor = KinectSensor.GetDefault();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Well done!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 20; i++)
            {
                var button = new Button { Content = i, Height=100, Width=100};

                int i1 = i;
                button.Click += (o, args) => MessageBox.Show("seleccionaste el botón #" + i1);

                scrollContent.Children.Add(button);

            }
        }
    }
}
