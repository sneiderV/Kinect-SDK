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

namespace FourStreamsKinect_v2
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor sKinect;
        MultiSourceFrameReader sReader; 

        public MainWindow()
        {
            InitializeComponent();
        }

        //asignamos el kinect que este conectado
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sKinect = KinectSensor.GetDefault();

            if (sKinect != null)
            {
                sKinect.Open();
                sReader = sKinect.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared | FrameSourceTypes.Body);
                sReader.MultiSourceFrameArrived += SReader_MultiSourceFrameArrived; //se usa para cada frame que llega
            }
            else
            {
                MessageBox.Show("No se reconoce un kinect");
            }

        }

        private void SReader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            //obtener la referencia del multi-frame
            var reference = e.FrameReference.AcquireFrame();

            //abrir el frame de color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                }
            }

            //abrir el frame de profundidad
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                }
            }

            //abrir el frame de infrarrojo
            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                }
            }

            //abrir el frame del cuerpo
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {

                }
            }
        }

        private void Body_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Infrared_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Depth_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Camera_Click(object sender, RoutedEventArgs e)
        {

        }

       
    }
}
