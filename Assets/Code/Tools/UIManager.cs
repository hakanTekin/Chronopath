using Assets.Code.Creature.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Code.Tools
{
    class UIManager : MonoBehaviour
    {
        [SerializeField]private GameObject TimerGO, ScoreGO, SliderGO;
        private Text TimerText;
        private Text ScoreText;

        public GameObject pauseMenu;
        public GameObject pauseButton;

        public Button attackButton;


        private Character character;

        private bool isMenuOpen;
        private static Font TextFont;

        private Joystick joystick;
        private void Start()
        {
            isMenuOpen = false;
            character = GetComponentInParent<Character>();
            //Text Elements Setup

            TimerText = TimerGO.GetComponent<Text>();
            ScoreText = ScoreGO.GetComponent<Text>();
            if(attackButton == null)
                attackButton = gameObject.GetComponentInChildren<Button>();

            attackButton.onClick.AddListener(character.Attack);

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
        public void Menu(bool isForced = false)
        {
            if (isMenuOpen)
            {
                pauseMenu.SetActive(false);
                isMenuOpen = false;
                pauseButton.SetActive(true);
                Time.timeScale = 1;
                //Close menu
            }
            else
            {
                pauseMenu.SetActive(true);
                isMenuOpen = true;
                if (!isForced) {
                    Button[] buttons = pauseMenu.GetComponents<Button>();
                    foreach (Button item in buttons)
                    {
                        if (item.name == "ResumeButton")
                            item.gameObject.SetActive(false);
                    }
                }
                pauseButton.SetActive(false);
                //Open menu
                Time.timeScale = 0;
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(1);
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
