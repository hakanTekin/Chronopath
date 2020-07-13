using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Tools
{
   
    class Score:MonoBehaviour
    {
        public Text scoreText;
        private int score;
        public int ScoreValue
        {
            get { return score; }
            set { score = value; }
        }
        public void UpdateScoreUI(int s)
        {
            scoreText.text = String.Format("%04d", s);
        }
    }
}
