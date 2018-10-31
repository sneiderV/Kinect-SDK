using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectTwitter
{
    class OneTweet
    {
        public OneTweet(String nom, String id, String cont, String dat)
        {
            userName = nom;
            userId = id;
            content = cont;
            date = dat;

        }

        public string userName { get; set; }
        public string userId { get; set; }
        public string content { get; set; }
        public string date { get; set; }
    }
}
