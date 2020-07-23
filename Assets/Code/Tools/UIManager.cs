using Assets.Code.Creature.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

namespace Assets.Code.Tools
{
    class UIManager : MonoBehaviour
    {
        private GameObject TimerGO, ScoreGO, SliderGO;
        public Text TimerText;
        public Text ScoreText;


        private Character character;

        private bool isMenuOpen;
        private static Font TextFont;

        private Joystick joystick;
        private void Start()
        {
            character = GetComponentInParent<Character>();
            //Text Elements Setup
            TimerGO = new GameObject("TimerGO");
            ScoreGO = new GameObject("ScoreGO");
            
            TextFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");

            TimerGO.transform.SetParent(gameObject.transform);

            TimerText = TimerGO.AddComponent<Text>();
            TimerText.text = "yaraklarr";
            TimerText.font = TextFont;
            TimerText.material = TextFont.material;

            ScoreGO.transform.SetParent(gameObject.transform);

            ScoreText = ScoreGO.AddComponent<Text>();
            ScoreText.text = "basaklarr";
            ScoreText.font = TextFont;
            ScoreText.material = TextFont.material;

            joystick = gameObject.GetComponentInChildren<FixedJoystick>();
        }

        private void Update()
        {
            MovementInput(joystick.Direction);
        }

        

        public void UpdateTime(String newTime)
        {
            TimerText.text = newTime;

        }
        public void UpdateScore(String newScore)
        {
            ScoreText.text = newScore;
        }

        public void MovementInput(Vector2 direction)
        {
            MovementInput(direction.x, direction.y);
        }
        public bool MovementInput(float x, float y)
        {
            character.Controller.MovementInput(new Vector2(x, y));
            return false;
        }

        public bool TimeMachineInput(int x)
        {
            if(!isMenuOpen)
                character.TimeMachineSliderInput(x);
            return false;
        }
        public void Menu()
        {
            if (isMenuOpen)
            {
                isMenuOpen = false;
                Time.timeScale = 1;
                //Close menu
            }
            else
            {
                isMenuOpen = true;
                //Open menu
                Time.timeScale = 0;
            }
        }
    }
}
