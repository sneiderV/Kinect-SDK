using System;
using System.IO;
using System.Net;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KinectTwitter
{
    /// <summary>
    /// Lógica de interacción para TweetK.xaml
    /// </summary>
    public partial class TweetK : UserControl
    {
        String urlImage;
        public TweetK(String nombre, String userID, String contentTweet, String fecha, String urlImg )
        {
            InitializeComponent();
            this.userName.Content = nombre;
            this.userId.Content = userID;
            this.contentTweet.Text = contentTweet;
            this.date.Content = fecha;
            urlImage = urlImg;
            pintarImagen();
        }

        private void pintarImagen()
        {
            if (urlImage != "")
            {/*
                WebClient w = new WebClient();
                byte[] imgByte = w.DownloadData(urlImage);
                MemoryStream stream = new MemoryStream(imgByte);
                System.Drawing.Image im = System.Drawing.Image.FromStream(stream);
                im.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);

                BitmapImage ix = new BitmapImage();
                ix.BeginInit();
                ix.CacheOption = BitmapCacheOption.OnLoad;
                ix.StreamSource = stream;
                ix.EndInit();

                img_tweet.Source = ix;
                

                Stream stremimg;
                HttpWebRequest req = ((HttpWebRequest)WebRequest.Create(urlImage));
                HttpWebResponse res = ((HttpWebResponse)req.GetResponse());
                stremimg = res.GetResponseStream();
                if (stremimg!=null)
                {
                    img_tweet = System.Drawing.Image.FromStream(stremimg);
                }
                */

                WebClient w = new WebClient();
                byte[] imgByte = w.DownloadData(urlImage);
                MemoryStream stream = new MemoryStream(imgByte);

                Image _img = new Image();
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.StreamSource = stream;
                
                //bi.UriSource = new System.Uri("");
                bi.EndInit();

                _img.Source = bi;
                //convr
                ImageBrush ib = new ImageBrush();
                ib.ImageSource = bi;

                rt_img.Fill = ib;
            }
        }

    }
}
