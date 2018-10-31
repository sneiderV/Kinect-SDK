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

        public String userName { get; set; }
        public String userId { get; set; }
        public String content { get; set; }
        public String date { get; set; }
    }
}
