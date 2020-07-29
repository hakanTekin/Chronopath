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
        [SerializeField]private GameObject TimerGO, ScoreGO, SliderGO;
        private Text TimerText;
        private Text ScoreText;

        public GameObject pauseMenu;


        private Character character;

        private bool isMenuOpen;
        private static Font TextFont;

        private Joystick joystick;
        private void Start()
        {
            character = GetComponentInParent<Character>();
            //Text Elements Setup

            TimerText = TimerGO.GetComponent<Text>();
            ScoreText = ScoreGO.GetComponent<Text>();

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
            character.MovementInput(direction);
        }
        public bool MovementInput(float x, float y)
        {
            MovementInput(new Vector2(x, y));
            return true;
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
                pauseMenu.SetActive(false);
                isMenuOpen = false;
                Time.timeScale = 1;
                //Close menu
            }
            else
            {
                pauseMenu.SetActive(true);
                isMenuOpen = true;
                //Open menu
                Time.timeScale = 0;
            }
        }
    }
}
