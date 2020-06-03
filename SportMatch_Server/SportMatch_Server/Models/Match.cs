using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportMatch_1.Models
{
    public class Match: Trainer
    {
        float matchRating;
        int requestCode;
        
        public float MatchRating { get => matchRating; set => matchRating = value; }
        public int RequestCode { get => requestCode; set => requestCode = value; }

        public Match():base() { }
        public Match(string fn, string ln, string em, string ph1, string ph2, string gen, string pas, string abm, int pr, string dateofBirth, string img1, float rate ,float matchRating,int requestCode) : base( fn,  ln,  em, ph1, ph2, gen, pas,  abm, pr, dateofBirth,img1, rate)
        {
            this.MatchRating = matchRating;
            this.RequestCode = requestCode;
        }

    }
}