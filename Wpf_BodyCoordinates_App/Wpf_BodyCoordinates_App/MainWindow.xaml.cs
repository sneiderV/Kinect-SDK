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
namespace Wpf_BodyCoordinates_App
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor miKinect = null;
        BodyFrameReader bodyFrameReader = null;
        Body[] bodies = null;


        public MainWindow()
        {
            InitializeComponent();
            initialiseKinect();
        }

        public void initialiseKinect()
        {
            miKinect = KinectSensor.GetDefault();

            if (miKinect != null)
            {
                //prendemos el kinect
                miKinect.Open();
            }
            else
            {
                MessageBox.Show("El kinect no se reconoce");
            }

            bodyFrameReader = miKinect.BodyFrameSource.OpenReader();

            if(bodyFrameReader != null)
            {
                bodyFrameReader.FrameArrived += BodyFrameReader_FrameArrived;
            }


        }

        private void BodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataRecived = false;

            using(BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (bodies == null)
                    {
                        bodies = new Body[bodyFrame.BodyCount];
                    }
                    bodyFrame.GetAndRefreshBodyData(bodies);
                    dataRecived = true;
                }

                if (dataRecived)
                {
                    foreach(Body body in bodies)
                    {
                        if (body.IsTracked)
                        {
                            IReadOnlyDictionary<JointType, Joint> joints = body.Joints;
                            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();

                            Joint midSpine = joints[JointType.SpineMid];

                            float ms_distance_x = midSpine.Position.X;
                            float ms_distance_y = midSpine.Position.Y;
                            float ms_distance_z = midSpine.Position.Z;

                           tb_spineX.Text = ms_distance_x.ToString("#.##");
                            tb_spineY.Text = ms_distance_z.ToString("#.##");
                            tb_spineZ.Text = ms_distance_y.ToString("#.##");

                            Console.WriteLine("Pos en X : "+ms_distance_x.ToString("#.##"));
                            Console.WriteLine("Pos en Y : " + ms_distance_x.ToString("#.##"));
                            Console.WriteLine("Pos en Z : " + ms_distance_x.ToString("#.##"));
                        }
                    }
                }
            }
        }
    }
}
