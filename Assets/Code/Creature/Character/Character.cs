using Assets.Code.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Code.Creature.Character
{
    class Character : LivingCreature
    {
        protected Timer Timer;
        protected Score Score;
        protected TimeMachine TimeMachine;
        protected My2DCharacterController Controller;
        public Character()
        {

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
