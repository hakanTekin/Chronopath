using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Creature.Character
{
    class TimeMachine
    {
        private float timeAffectionDelta;
        float fuel;

        TimeMachine(int f = 100, float tad = 1)
        {

            this.timeAffectionDelta = tad;
            this.fuel = f;

        }
        public bool ChangeTime(float delta = float.MinValue)
        {
            if (delta == float.MinValue)
                delta = timeAffectionDelta;
            //TODO: Change World Time
            return false;
        }
    }
}
