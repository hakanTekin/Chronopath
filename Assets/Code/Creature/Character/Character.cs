using Assets.Code.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Code.Creature.Character
{
    class Character : LivingCreature
    {
        private World.World world;
        protected Timer Timer;
        protected Score Score;
        protected TimeMachine TimeMachine;
        protected My2DCharacterController Controller;
        private CharacterInputs inputController;

        private Coroutine increaseTimerCoroutine, decreaseTimerCoroutine;

        private int currentTimeMachineInput = 0;

        private void Awake()
        {
            Controller = gameObject.GetComponent<My2DCharacterController>();
            this.TimeMachine = new TimeMachine();//Creates a timemachine with default values
            this.Score = gameObject.AddComponent<Score>();
            this.Timer = gameObject.AddComponent<Timer>();
            this.world = FindObjectOfType<World.World>();
            inputController = new CharacterInputs();

            inputController.TimeMachine.DecreaseTime.performed += (obj) => StartIncreaseCR(obj);
            inputController.TimeMachine.IncreaseTime.performed += (obj) => StartDecreaseCR(obj);

            inputController.TimeMachine.DecreaseTime.canceled += (obj) => { decreaseCR_Switch = false; world.TimerStartStopState(true); };
            inputController.TimeMachine.IncreaseTime.canceled += (obj) => { increaseCR_Switch = false; world.TimerStartStopState(true); };
        }

        public void StartIncreaseCR(InputAction.CallbackContext obj){
            world.TimerStartStopState(false);
            decreaseTimerCoroutine = StartCoroutine(DecreaseTime());
            
        }
        public void StartDecreaseCR(InputAction.CallbackContext obj) {
            world.TimerStartStopState(false); 
            increaseTimerCoroutine = StartCoroutine(IncreaseTime());
            
        }
        /// <summary>
        ///  switch bool for continously increasing/decreasing time when timeChange inputs are active
        /// </summary>
        private bool increaseCR_Switch = true, decreaseCR_Switch = true;

        /// <summary>
        /// Increase Time
        /// </summary>
        /// <param name="deltaMultiplier">Multiplier for timeAffectionDelta<br></br>Value of 3 will change time by 3*TAD</param>
        /// <returns></returns>
        private IEnumerator IncreaseTime(){
            while (increaseCR_Switch) {
                TimeMachine.ChangeTime(world,true, currentTimeMachineInput);
                yield return new WaitForSeconds(1);
            }
            increaseCR_Switch = true;
        }
        /// <summary>
        /// Decrease Time
        /// </summary>
        /// <param name="deltaMultiplier">Multiplier for timeAffectionDelta<br></br>Value of 3 will change time by 3*TAD</param>
        /// <returns></returns>
        private IEnumerator DecreaseTime()
        {
            while (decreaseCR_Switch) {
                TimeMachine.ChangeTime(world, false, currentTimeMachineInput);
                yield return new WaitForSeconds(1);
            }
            decreaseCR_Switch = true;
        }

        public void TimeMachineSliderInput(int x)
        {
            if (x == 0)
            {
                if(decreaseTimerCoroutine != null)
                    StopCoroutine(decreaseTimerCoroutine);
                if (increaseTimerCoroutine != null)
                    StopCoroutine(increaseTimerCoroutine);
                world.TimerStartStopState(true);
            }
            else if (x > 0)
            {
                world.TimerStartStopState(false);
                currentTimeMachineInput = x;
               if(increaseTimerCoroutine == null)//if there is no current coroutine working. Initialize it.
                    increaseTimerCoroutine = StartCoroutine(IncreaseTime());
            }
            else if (x < 0) {
                world.TimerStartStopState(false);
                currentTimeMachineInput = Math.Abs(x); //Time machine input is always positive. DecreaseTime method always takes its negative
                if(decreaseTimerCoroutine == null) //if there is no current coroutine working. Initialize it.
                    decreaseTimerCoroutine = StartCoroutine(DecreaseTime());
            }
        }

        private void OnEnable()
        {
            inputController.Enable();
        }

        private void OnDisable()
        {
            inputController.Disable();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        
        protected override void Attack(float dmg)
        {
            base.Attack(dmg);
        }

        protected override bool Death()
        {
            return base.Death();
        }

        protected override void HandleAnimation()
        {
            base.HandleAnimation();
        }

        protected override void UpdateHealth(float dmg)
        {
            base.UpdateHealth(dmg);
        }
    }
}
