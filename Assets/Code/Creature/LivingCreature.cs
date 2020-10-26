using Assets.Code.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    abstract class LivingCreature : MonoBehaviour, World.IDamageable
    {
        protected float Health = 20;
        protected float Damage = 20;
        protected Animator Animator;

        private Vector2 currentVelocity;
        private Quaternion currentRotation;
        private Vector2 deltaSpeed;

        protected virtual void HandleAnimation()
        {
        }

        protected virtual void UpdateHealth(float dmg)
        {

        }

        protected virtual void Attack(float dmg)
        {

        }

        public virtual bool GetDamage(float dmg)
        {
            Debug.Log(this.gameObject.name + " getting " + dmg + " damage");
            this.Health -= dmg;
            if (this.Health <= 0)
            {
                this.Death();
            }
            return true;
        }

        public virtual bool Death()
        {
            Debug.Log("DED");
            if (this.gameObject.tag == "Player") {
                Time.timeScale = 0; //Stop the game ese;
                this.gameObject.BroadcastMessage("Menu", true);
            }else
            {
                Destroy(this.gameObject);
            }
            return true;
        }
    }
}
