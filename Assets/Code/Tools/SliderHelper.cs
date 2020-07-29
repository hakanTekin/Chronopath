using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Code.Tools
{
    class SliderHelper : MonoBehaviour, IPointerUpHandler
    {
        UIManager UI;
        private void Awake()
        {
            UI = GetComponentInParent<UIManager>();
            gameObject.GetComponent<Slider>().onValueChanged.AddListener(ValueChange);
        }
        /// <summary>
        /// Listener callback method for Slider when it has changed value
        /// </summary>
        /// <param name="x"></param>
        public void ValueChange(float x)
        {

            UI.TimeMachineInput((int)x);
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            gameObject.GetComponent<Slider>().value = 0;
         
        }
    }
}
