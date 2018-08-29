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

namespace Wpf_MVA_app
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        KinectSensor sensor;
        InfraredFrameReader irReader;
        ushort[] irData;
        byte[] irDataConverted;
        WriteableBitmap irBitmap;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            sensor = KinectSensor.GetDefault();
            irReader = sensor.InfraredFrameSource.OpenReader();
            FrameDescription fd = sensor.InfraredFrameSource.FrameDescription;//tamaño de los datos del infra rojo
            irData = new ushort[fd.LengthInPixels];
            irDataConverted = new byte[fd.LengthInPixels * 4]; //*4 para formato rgba
            irBitmap = new WriteableBitmap(fd.Width, fd.Height, 96.0, 96.0, PixelFormats.Bgr32, null);
            imageinfra.Source = irBitmap; 

            sensor.Open(); //se abre el flujo de datos
            irReader.FrameArrived += IrReader_FrameArrived; //se ejecuta cada vez que se recive un frame del sensor
        }

        private void IrReader_FrameArrived(object sender, InfraredFrameArrivedEventArgs e)
        {
            using(InfraredFrame irFrame = e.FrameReference.AcquireFrame())
            {
                if(irFrame != null)
                {
                    irFrame.CopyFrameDataToArray(irData);

                    for (int i = 0; i < irData.Length; i++)
                    {
                        byte intesity = (byte)(irData[i] >> 8);
                        irDataConverted[i * 4] = intesity;
                        irDataConverted[i * 4 + 1] = intesity;
                        irDataConverted[i * 4 + 2] = intesity;
                        irDataConverted[i * 4 + 3] = 255;
                        //se copia 4 veces por cada componente del pixel
                    }
                }
                irDataConverted.CopyTo(irBitmap,irBitmap.PixelHeight);
                irDataConverted.CopyTo(irBitmap);
                irBitmap.Invalidate();
            }
        }
    }
}
