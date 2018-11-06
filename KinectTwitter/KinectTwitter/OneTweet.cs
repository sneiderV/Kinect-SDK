using System;
using System.Drawing;

namespace KinectTwitter
{
    class OneTweet
    {
        public OneTweet(String nom, String id, String cont, String dat, String urlImg)
        {
            userName = nom;
            userId = id;
            content = cont;
            date = dat;
            urlImage = urlImg;
        }


        public String userName { get; set; }
        public String userId { get; set; }
        public String content { get; set; }
        public String date { get; set; }
        public String urlImage { get; set; }
    }
}
