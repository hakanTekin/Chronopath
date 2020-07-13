using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Tools
{
    class UIManager : MonoBehaviour
    {
        private GameObject TimerGO, ScoreGO;
        public Text TimerText;
        public Text ScoreText;

        private bool isMenuOpen;
        private static Font TextFont;
        private void Start()
        {
            TimerGO = new GameObject("TimerGO");
            ScoreGO = new GameObject("ScoreGO");
            TextFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            TimerGO.transform.parent = gameObject.transform;

            TimerText = TimerGO.AddComponent<Text>();
            TimerText.text = "yaraklarr";
            TimerText.font = TextFont;
            TimerText.material = TextFont.material;
            

            ScoreGO.transform.parent = gameObject.transform;

            ScoreText = ScoreGO.AddComponent<Text>();
            ScoreText.text = "basaklarr";
            ScoreText.font = TextFont;
            ScoreText.material = TextFont.material;
            
        }

        public void UpdateTime(String newTime)
        {
            TimerText.text = newTime;

        }
        public void UpdateScore(String newScore)
        {
            ScoreText.text = newScore;
        }

        public bool MovementInput(float x, float y)
        {
            return false;
        }

        public bool TimeMachineInput(float x)
        {
            return false;
        }
        public bool Menu()
        {
            if (isMenuOpen)
            {
                isMenuOpen = false;
                //Close menu
            }
            else
            {
                isMenuOpen = true;
                //Open menu
            }
            return isMenuOpen;
        }
    }
}
