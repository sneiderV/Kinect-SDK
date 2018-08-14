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

namespace DepthCameraApp
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor myKinect;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (KinectSensor.KinectSensors.Count == 0)
            {
                MessageBox.Show("No se detecta ningun Kinect", "Visor de Camara");
                Application.Current.Shutdown();
            }

            try
            {
                myKinect = KinectSensor.KinectSensors.FirstOrDefault();
                myKinect.DepthStream.Enable();
                myKinect.Start();
                myKinect.DepthFrameReady += MyKinect_DepthFrameReady;
            }
            catch
            {
                MessageBox.Show("Fallo al inicializar kinect","visor de kinect");
                Application.Current.Shutdown();
            }
        }

        short[] datosDistancia = null;
        byte[] colorImagenDistancia = null;
        WriteableBitmap bitmapImagenDistancia = null; //permirte ver los frames

        private void MyKinect_DepthFrameReady(object sender, DepthImageFrameReadyEventArgs e)
        {
            using (DepthImageFrame frameDistancia = e.OpenDepthImageFrame())
            {
                if (frameDistancia == null) return;

                if (datosDistancia == null)
                    datosDistancia = new short[frameDistancia.PixelDataLength];

                if (colorImagenDistancia == null)
                    colorImagenDistancia = new byte[frameDistancia.PixelDataLength * 4];// uno por cada color 4

                frameDistancia.CopyPixelDataTo(datosDistancia);
                int posColorImagenDistancia = 0;
                for (int i = 0; i < frameDistancia.PixelDataLength; i++)
                {
                    int valorDistancia = datosDistancia[i] >> 3; //omitir los 3 bits destinados a esqueletos

                    if (valorDistancia == myKinect.DepthStream.UnknownDepth)
                    {
                        colorImagenDistancia[posColorImagenDistancia++] = 0; //azul
                        colorImagenDistancia[posColorImagenDistancia++] = 0; //verde
                        colorImagenDistancia[posColorImagenDistancia++] = 255; //rojo
                    }

                    else if (valorDistancia == myKinect.DepthStream.TooFarDepth)
                    {
                        colorImagenDistancia[posColorImagenDistancia++] = 255; //azul
                        colorImagenDistancia[posColorImagenDistancia++] = 0; //verde
                        colorImagenDistancia[posColorImagenDistancia++] = 0; //rojo
                    }

                    else if (valorDistancia == myKinect.DepthStream.TooNearDepth)
                    {
                        colorImagenDistancia[posColorImagenDistancia++] = 0; //azul
                        colorImagenDistancia[posColorImagenDistancia++] = 255; //verde
                        colorImagenDistancia[posColorImagenDistancia++] = 0; //rojo
                    }

                    //distancia fuera del rango anterior
                    else
                    {
                        byte byteDistancia = (byte)(255 - (valorDistancia >> 5));
                        colorImagenDistancia[posColorImagenDistancia++] = byteDistancia; //azul
                        colorImagenDistancia[posColorImagenDistancia++] = byteDistancia; //verde
                        colorImagenDistancia[posColorImagenDistancia++] = byteDistancia; //rojo
                    }

                    posColorImagenDistancia++; //color de transparencia                  
                }

                if(bitmapImagenDistancia == null)
                {
                    this.bitmapImagenDistancia = new WriteableBitmap(
                        frameDistancia.Width,
                        frameDistancia.Height,
                        96,
                        96,
                        PixelFormats.Bgr32,
                        null);
                    ctrImage_mostrarImagenes.Source = bitmapImagenDistancia;
                }

                this.bitmapImagenDistancia.WritePixels(
                    new Int32Rect(0, 0, frameDistancia.Width, frameDistancia.Height),
                    colorImagenDistancia,
                    frameDistancia.Width * 4,
                    0
                    );
            }
        }
    }
}
