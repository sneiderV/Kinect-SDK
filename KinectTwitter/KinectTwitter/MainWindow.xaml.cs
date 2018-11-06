using System;
using System.IO;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using Tweetinvi;
using System.Globalization;
using System.Threading;

namespace KinectTwitter
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /*Twitter*/
        const string CONSUMER_KEY = "a2mVCms5rkDNxgwUEFdy4oOfE";
        const string CONSUMER_SECRET = "ahwcSlg5ENtjmIP820NptGP1QItCJJHYY0SxcvXOIkHEsZmBfd";
        const string ACCESS_TOKEN = "889371522648993792-33Ol8hQUocGSUV8hYEPKv4GHULn15Dw";
        const string ACCESS_TOKEN_SECRET = "WQfH4NZYdyEK9D11R0u0ibKnfQ9m5NPXM1IobkLPl0nJh";

        KinectSensor miKinect;

       
        ArrayList listaTweets;

        //Hilo para refrescar la pantalla
        Thread hiloRefresh;

        public MainWindow()
        {
            InitializeComponent();

            miKinect = KinectSensor.GetDefault();

            //especifico mi region 
            KinectRegion.SetKinectRegion(this, mikinectRegion);
            App app = ((App)Application.Current);

            //asigno mi kinect a mi region
            mikinectRegion.KinectSensor = miKinect;

            //autenticación con twitter
            Auth.SetUserCredentials(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);

            //Ejecutando el streaming de tweets en background
            BackgroundWorker streamBack = new BackgroundWorker();
            streamBack.DoWork += streamingTweets;
            //streamBack.RunWorkerCompleted += mostrarTweets();
            streamBack.RunWorkerAsync();

            listaTweets = new ArrayList();

            

            //manejo del hilo 
         /*   ThreadStart ts = new ThreadStart(pintarTweets);
            hiloRefresh = new Thread(ts);
           hiloRefresh.Start();
           */             
        }

        /// <summary>
        /// Se ejecuta cuando se carga la aplicación 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tweetsIniciales();
        }

        /// <summary>
        /// Pinto unas ventanas en blanco para dar inicio a la aplicación 
        /// </summary>
        public void tweetsIniciales()
        {
            for (int i = 0; i < 3; i++)
            {
                var t = new TweetK("Uniandes", "@Uniandes", "ISIS Celebrando los 50 años", "1 Nov","");
                miScrollContent.Children.Add(t);
            }
        }


        public void pintarTweets()
        {
           
            if (listaTweets.Count > 0)
            {
                Console.WriteLine("entre al if");
                
                listaTweets.Reverse();
                miScrollContent.Children.Clear();
                for (int i = 0; i < listaTweets.Count; i++)
                {
                    //Console.WriteLine(listaTweets[i]);
                    var tw = (OneTweet)listaTweets[i];
                    var t = new TweetK(tw.userName, tw.userId, tw.content, tw.date, tw.urlImage);
                    miScrollContent.Children.Add(t);
                }
            }
            else
            {
                Console.WriteLine("---> no hay tweets aún");
            }
                       
        }

        /// <summary>
        /// Hace el streaming de los tweets con el hashtag #KinectUniandes
        /// </summary>
        /// <param name="o"></param>
        /// <param name="args"></param>
        private void streamingTweets(object o, DoWorkEventArgs args)
        {
            ArrayList twetts = new ArrayList();
            //strean
            var stream = Tweetinvi.Stream.CreateFilteredStream();
            //palabra a seguir
            String hashtag = "#KinectUniandes";
            stream.AddTrack(hashtag);
            //manejo de tweets
            Console.WriteLine("-----------> Escuchando tweets para "+hashtag);
            stream.MatchingTweetReceived += (sender, arg) =>
            {
                Console.WriteLine("Entra ---> "+arg);
                String dat = arg.Tweet.CreatedAt.DayOfWeek.ToString()+" "+ arg.Tweet.CreatedAt.Day+" "+ CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(arg.Tweet.CreatedAt.Month);
                String nom = arg.Tweet.CreatedBy.Name;
                String usId = "@" + arg.Tweet.CreatedBy.ScreenName;
                String cont = arg.Tweet.FullText;
                String urlImagen = "";

                if (arg.Tweet.Media.Count>0)
                {
                    urlImagen = arg.Tweet.Media[0].MediaURL;
                }

                //var t = new TweetK(nom,usId,cont,dat);
                var t = new OneTweet(nom,usId,cont,dat,urlImagen);
                
                twetts.Add(arg.Tweet);

                listaTweets.Add(t);
            };
            stream.StartStreamMatchingAllConditions();
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            Console.WriteLine("actualizare");
            pintarTweets();
        }

        //Console.WriteLine("1");

    }
}
/*
fecha: arg.Tweet.CreatedAt.DayOfWeek (int)
nombre: arg.Tweet.CreatedBy.Name (String)
userId: arg.Tweet.CreatedBy.ScreenName (String)+@
content: arg.Tweet.FullText (String)
 */
