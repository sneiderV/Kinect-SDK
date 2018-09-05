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
        IList<Body> bodies;
        Mode tipoFrame = Mode.Color;

        public MainWindow()
        {
            InitializeComponent();
        }

        //tipos de frames
        public enum Mode
        {
            Color, Depth, Infrared, Body
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
                if(i_camera.Visibility == Visibility.Hidden)
                {
                    i_camera.Visibility = Visibility.Visible;
                }
                if (frame != null)
                {
                    if (tipoFrame == Mode.Color)
                    {
                        i_camera.Source = ReadersFrames.ToBitmap(frame);
                    }
                }
            }

            //abrir el frame de profundidad
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (i_camera.Visibility == Visibility.Hidden)
                {
                    i_camera.Visibility = Visibility.Visible;
                }
                if (frame != null)
                {
                    if (tipoFrame == Mode.Depth)
                    {
                        i_camera.Source = ReadersFrames.ToBitmap(frame);
                    }
                }
            }

            //abrir el frame de infrarrojo
            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {
                if (i_camera.Visibility == Visibility.Hidden)
                {
                    i_camera.Visibility = Visibility.Visible;
                }
                if (frame != null)
                {
                    if (tipoFrame == Mode.Infrared)
                    {
                        i_camera.Source = ReadersFrames.ToBitmap(frame);
                    }
                }
            }

            //abrir el frame del cuerpo
            using (var frame = reference.BodyFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    c_body.Children.Clear(); //borramos cada esqueleto que va pasando

                    if (tipoFrame == Mode.Body)
                    {
                        i_camera.Visibility = Visibility.Hidden; //escondemos el fondo
                        bodies = new Body[frame.BodyFrameSource.BodyCount];

                        frame.GetAndRefreshBodyData(bodies); // ???

                        foreach (var bo in bodies)
                        {
                            if (bo != null)
                            {
                                if (bo.IsTracked)
                                {
                                    // Dibujar las articulaciones...
                                    c_body.DrawSkeleton(bo);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Body_Click(object sender, RoutedEventArgs e)
        {
            tipoFrame = Mode.Body;
        }

        private void Infrared_Click(object sender, RoutedEventArgs e)
        {
            tipoFrame = Mode.Infrared;
        }

        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            tipoFrame = Mode.Depth;
        }

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            tipoFrame = Mode.Color;
        }

       
    }
}
