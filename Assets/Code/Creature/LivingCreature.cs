using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code
{
    abstract class LivingCreature : MonoBehaviour
    {
        protected float Health;
        protected float Damage;
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

        protected virtual bool Death()
        {
            Debug.Log("DED");
            return false;
        }
    }
}
