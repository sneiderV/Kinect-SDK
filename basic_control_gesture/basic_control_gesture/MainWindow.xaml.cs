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
using Microsoft.Kinect.Wpf.Controls;

namespace basic_control_gesture
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor miKinect;
        MultiSourceFrameReader sReader; 

        public MainWindow()
        {    
            InitializeComponent();

            KinectRegion.SetKinectRegion(this, kinectRegion);

            App app = ((App)Application.Current);
            app.KinectRegion = kinectRegion;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            miKinect = KinectSensor.GetDefault();

            if (miKinect!=null)
            {
                miKinect.Open();
                sReader = miKinect.OpenMultiSourceFrameReader(FrameSourceTypes.Body);

                 
            }
        }
    }
}
