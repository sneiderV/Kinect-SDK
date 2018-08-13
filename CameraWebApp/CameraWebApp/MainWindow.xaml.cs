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

namespace CameraWebApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor miKinect;

        public MainWindow()
        {
            InitializeComponent();
        }

        //metodo que se ejecuta cuando iniciemos el proyecto
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            miKinect = KinectSensor.KinectSensors[0]; //el kinect que se esta usando FirstorDefault()
            miKinect.Start();
            miKinect.ColorStream.Enable(); //permite el envio de datos desde la kinet en RGB 30Frames x Seg
            miKinect.AllFramesReady += MiKinect_AllFramesReady;
        }

        //cuando el EventHandler capture datos los muestra en el ctrImage
        private void MiKinect_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            // se elimina cada vez que se captura nuevo datos
            using (ColorImageFrame frameImage = e.OpenColorImageFrame())
            {
                if (frameImage == null) return;

                //datos que se reciben de la Kinect
                byte[] datosColor = new byte[frameImage.PixelDataLength];

                frameImage.CopyPixelDataTo(datosColor);

                //mostramos en el crtImage del XAML
                //ancho, alto, dpi horizontal, dpi verticales, formato de los pixels, paleta del bitmap, array de bytes que tienen la imagen, Strite
                ctrImg_mostrarImagenes.Source = BitmapSource.Create(
                    frameImage.Width, frameImage.Height,
                    96,
                    96,
                    PixelFormats.Bgr32,
                    null,
                    datosColor,
                    frameImage.Width * frameImage.BytesPerPixel
                    );


            }  
        }
    }
}
