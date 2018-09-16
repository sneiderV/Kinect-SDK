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
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Resources;

namespace ControlGestureHand
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
            miKinect = KinectSensor.GetDefault();

            //especifico mi region 
            KinectRegion.SetKinectRegion(this, mikinectRegion);
            
            App app = ((App)Application.Current);
            
            //asigno mi kinect a mi region
            mikinectRegion.KinectSensor = miKinect;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var brush = new ImageBrush();
            for (int i = 1; i < 28; i++)
            {
                //var button = new Button { Content = i, Height=200, Width=200};
                var button = new Button();
                button.Name = "i"+i.ToString();
                              
                // si el build action de la imagen es resource
                string urIm = "Images/"+i+".jpg";
                Uri resourceUri = new Uri(urIm, UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);

                button.Height = temp.Height;
                button.Width = temp.Width;
                var brush = new ImageBrush();
                brush.ImageSource = temp;
                button.Background = brush;

                button.Click += Button_Click_Scroll_Item;
                miScrollContent.Children.Add(button);

                //si el build action de la imagen es Content
                /* var brush = new ImageBrush();
                   brush.ImageSource = new BitmapImage(new Uri("images/1.jpg",UriKind.Relative));
                   button.Background = brush;
                */
                              

                //button.Click += (o, args) => MessageBox.Show("seleccionaste el botón #" + i);
            }
        }

        //Handle de los botones del scroll
        private void Button_Click_Scroll_Item(object sender, RoutedEventArgs e)
        {
            String res = sender.ToString();
            Button b = sender as Button;
            String n = "no hay boton";
            if (b!=null)
            {
                n = b.Name.ToString();
            }
            tx_b_descripcion.Text = "cambio el botón: "+ n ;
        }

        //libero los recursos de la kinect 
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if(miKinect != null)
            {
                miKinect.Close();
                miKinect = null;
            }
        }
    }
}