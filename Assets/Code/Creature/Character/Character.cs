using Assets.Code.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Creature.Character
{
    class Character:LivingCreature
    {
        protected Timer Timer;
        protected Score Score;
        protected TimeMachine TimeMachine;
        protected My2DCharacterController Controller;
        public Character()
        {

        }
    }
}
