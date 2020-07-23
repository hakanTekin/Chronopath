using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Tools
{
    class Timer:MonoBehaviour
    {
        Coroutine TimerCoroutine;
        private int seconds, minutes;

        UIManager UI = null;

        public int Seconds { get => seconds; set => seconds = value; }
        public int Minutes { get => minutes; set => minutes = value; }

        private void Start()
        {
            Seconds = 0;
            Minutes = 0;

            //If there is a UIManager in the game
            UI = GameObject.FindObjectOfType<UIManager>();
            StartCoroutine(CountTimer());
        }

        public IEnumerator CountTimer()
        {
            while (true)
            {
                
                yield return new WaitForSeconds(1);
                Seconds++;
                if(Seconds >= 60)
                {
                    Minutes += Seconds / 60;
                    Seconds %= 60;
                }
                UpdateTimeUI();
            }
        }

        private void UpdateTimeUI()
        {
            if(UI != null)
            {
                UI.UpdateTime(ToString());
            }
        }

        public override string ToString()
        {
            return String.Format("{0:00} : {1:00}", Minutes, Seconds);
        }
    }
}
