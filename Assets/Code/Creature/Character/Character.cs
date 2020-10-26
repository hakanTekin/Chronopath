using Assets.Code.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Assets.Code.Creature.Character
{
    class Character : LivingCreature
    {
        private World.World world;
        protected Timer Timer;
        protected Score Score;
        /// <summary>
        /// Time Machine of the Player
        /// </summary>
        public TimeMachine TimeMachine;        
        /// <summary>
        /// Character physics handler
        /// </summary>
        public My2DCharacterController Controller;
        /// <summary>
        /// Inputs from new input system
        /// </summary>
        private CharacterInputs inputController;
        /// <summary>
        /// Vector3 indicating the weapon hit range From the right edge of character collider (from half height)
        /// </summary>
        [SerializeField]
        private Vector3 weaponVector;

        private bool isAttacking;
        /// <summary>
        /// When slider is held, in what interval should world time change.
        /// </summary>
        [SerializeField] private float timeMachineInputRepeatInterval = 0.3f;
        /// <summary>
        /// Coroutines used when player tries to change world time via slider
        /// </summary>
        private Coroutine increaseTimerCoroutine, decreaseTimerCoroutine;
        
        private int currentTimeMachineInput = 0;

        private void Awake()
        {
            Time.timeScale = 1;
            Controller = gameObject.GetComponent<My2DCharacterController>();
            this.TimeMachine = new TimeMachine();//Creates a timemachine with default values
            this.Score = gameObject.AddComponent<Score>();
            this.Timer = gameObject.AddComponent<Timer>();
            this.world = FindObjectOfType<World.World>();
            inputController = new CharacterInputs();
            this.Animator = gameObject.GetComponent<Animator>();
            inputController.TimeMachine.DecreaseTime.performed += (obj) => StartIncreaseCR(obj);
            inputController.TimeMachine.IncreaseTime.performed += (obj) => StartDecreaseCR(obj);

            inputController.TimeMachine.DecreaseTime.canceled += (obj) => { decreaseCR_Switch = false; world.TimerStartStopState(true); };
            inputController.TimeMachine.IncreaseTime.canceled += (obj) => { increaseCR_Switch = false; world.TimerStartStopState(true); };

            this.isAttacking = false;
        }
        /// <summary>
        /// <br>Starts coroutine for increasing world time.</br>
        /// <br>For timeMachine usage. Automatic world time step has its own coroutine</br>
        /// </summary>
        /// <param name="obj"></param>

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack(Damage);
            }
        }

        public void StartIncreaseCR(InputAction.CallbackContext obj){
            world.TimerStartStopState(false);
            decreaseTimerCoroutine = StartCoroutine(DecreaseTime());
            
        }
        /// <summary>
        /// Starts coroutine for decreasing world time
        /// </summary>
        /// <param name="obj"></param>
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
                yield return new WaitForSeconds(0.2f);
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
                yield return new WaitForSeconds(0.2f);
            }
            decreaseCR_Switch = true;
        }

        /// <summary>
        /// <br>Starts coroutines accordingly when timeMachine slider recieves input</br>
        /// </summary>
        /// <param name="x"> time machine input value</param>
        public void TimeMachineSliderInput(int x)
        {
            if (x == 0)
            {
                if (decreaseTimerCoroutine != null) {
                    StopCoroutine(decreaseTimerCoroutine);
                    decreaseTimerCoroutine = null;
                }

                if (increaseTimerCoroutine != null) {
                    StopCoroutine(increaseTimerCoroutine);
                    increaseTimerCoroutine = null;
                }
               
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
            if(inputController != null)
                inputController.Enable();
        }

        private void OnDisable()
        {
            if(inputController != null)
                inputController.Disable();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public void Attack() {
            Attack(Damage);
            Debug.Log("Attackin");

        }
        protected override void Attack(float dmg)
        {
            isAttacking = true;
            this.Animator.SetTrigger("attack");
            Vector3 pos = Controller.collisionBox.bounds.center;
            pos.x += Controller.collisionBox.bounds.size.x/2 + 0.2f; //Right side of the collision box + some margin
            pos.y -= Controller.collisionBox.bounds.size.y/2 + 0.1f;
            Vector3 to = pos + weaponVector;
            RaycastHit2D[] hits = Physics2D.RaycastAll(pos, to);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit == true)
                {
                    if (hit.collider.gameObject == this.gameObject)
                    {
                        Debug.Log("Character hit itself");
                    }
                    else
                    {
                        hit.collider.gameObject.BroadcastMessage("GetDamage", 20);
                    }
                    base.Attack(dmg);
                }
            }
            Debug.DrawLine(pos, to);
            isAttacking = false;
        }

        protected override void HandleAnimation()
        {
            base.HandleAnimation();
            this.Animator.SetFloat("speed", Math.Abs(Controller.realSpeed.x));
            this.Animator.SetBool("jumping", !Controller.isGrounded);
            this.Animator.SetBool("crouching", Controller.isCrouching);
        }

        protected override void UpdateHealth(float dmg)
        {
            base.UpdateHealth(dmg);

        }

        public void MovementInput(Vector2 delta)
        {
            Controller.MovementInput(delta);
            HandleAnimation();
            
        }
    }
}
