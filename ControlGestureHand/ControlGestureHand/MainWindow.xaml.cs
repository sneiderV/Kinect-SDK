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
using Newtonsoft.Json;

namespace ControlGestureHand
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        KinectSensor miKinect;
        List <String> dataDescrip;

        public MainWindow()
        {
            InitializeComponent();
            miKinect = KinectSensor.GetDefault();

            //especifico mi region 
            KinectRegion.SetKinectRegion(this, mikinectRegion);
            
            App app = ((App)Application.Current);
            
            //asigno mi kinect a mi region
            mikinectRegion.KinectSensor = miKinect;

           // leerJson();
        }
/*
        struct MyFile
        {
            public String directory;
            public ImageInfo[] images;
        }

         struct ImageInfo
        {
            public String filename;
            public String title;
        }

        private void leerJson()
        {

            //string urIm = "Data/dataImages.json";
            //Uri resourceUri = new Uri(urIm, UriKind.Relative);

            //var stream = File.OpenRead("Data/dataImages.json");

            //String p = @"..\..\..\Data\dataImages.json";
            //MyFile f = JsonConvert.DeserializeObject<MyFile>("Data/dataImages.json");

        }*/

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //var brush = new ImageBrush();
            for (int i = 1; i < 28; i++)
            {
                //var button = new Button { Content = i, Height=200, Width=200};
                var button = new Button();
                button.Name = "i"+i.ToString();
                button.ContentStringFormat = i.ToString();
                              
                // si el build action de la imagen es resource
                string urIm = "Images/"+i+".jpg";
                Uri resourceUri = new Uri(urIm, UriKind.Relative);
                StreamResourceInfo streamInfo = Application.GetResourceStream(resourceUri);
                BitmapFrame temp = BitmapFrame.Create(streamInfo.Stream);

                //ajuste del tamaño 
                button.Height = 600;
                button.Width = (button.Height * temp.Width) / temp.Height;
                
                
                
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

                //Console.WriteLine("-> tamaño imagen " + i + "   ancho: "+button.Width +" alto "+button.Height);
                //button.Click += (o, args) => MessageBox.Show("seleccionaste el botón #" + i);

/*
-> tamaño imagen 1   ancho: 245,76 alto 334,08
->tamaño imagen 2   ancho: 215,68 alto 328
->tamaño imagen 3   ancho: 341,12 alto 232,64
->tamaño imagen 4   ancho: 456,32 alto 320
->tamaño imagen 5   ancho: 455,68 alto 321,92
->tamaño imagen 6   ancho: 481,92 alto 338,88
->tamaño imagen 7   ancho: 576,64 alto 397,76
->tamaño imagen 8   ancho: 581,12 alto 377,28
->tamaño imagen 9   ancho: 577,28 alto 384,64
->tamaño imagen 10   ancho: 632,96 alto 337,6
->tamaño imagen 11   ancho: 697,6 alto 472
->tamaño imagen 12   ancho: 706,56 alto 467,52 ****
->tamaño imagen 13   ancho: 781,44 alto 509,76 ****
->tamaño imagen 14   ancho: 632,96 alto 888,32
->tamaño imagen 15   ancho: 976 alto 769,6
->tamaño imagen 16   ancho: 976,64 alto 770,56
->tamaño imagen 17   ancho: 975,36 alto 769,6
->tamaño imagen 18   ancho: 960,64 alto 780,16
->tamaño imagen 19   ancho: 970,88 alto 770,24
->tamaño imagen 20   ancho: 768,64 alto 967,04
->tamaño imagen 21   ancho: 776,32 alto 962,56
->tamaño imagen 22   ancho: 945,28 alto 749,12
->tamaño imagen 23   ancho: 961,28 alto 780,48
->tamaño imagen 24   ancho: 967,68 alto 768
->tamaño imagen 25   ancho: 955,52 alto 768,64
->tamaño imagen 26   ancho: 773,76 alto 964,16
->tamaño imagen 27   ancho: 766,08 alto 973,44
*/
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
                //n = b.Name.ToString();
                n = b.ContentStringFormat;
            }
            tx_b_descripcion.Text = "changed the button: "+ n ;
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