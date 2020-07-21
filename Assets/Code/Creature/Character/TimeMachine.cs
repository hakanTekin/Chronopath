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
        private int timeAffectionDelta;
        int fuel;

        public TimeMachine(int f = 100, int tad = 1)
        {
            this.timeAffectionDelta = tad;
            this.fuel = f;

        }
        public bool ChangeTime(World.World world, int delta = int.MinValue)
        {
            if (world is null)
            {
                throw new ArgumentNullException(nameof(world));
                return false;
            }

            if (delta == float.MinValue)
                delta = timeAffectionDelta;
            //Ensure an always negative fuelDelta. Since delta can either be negative or positive. But fuelDelta is always supposed to be negative.
            updateFuel(-1*Math.Abs(delta));
            world.ChangeWorldTime(delta);
            
            return true;
        }
        public int updateFuel(int fuelDelta)
        {
            this.fuel += fuelDelta;
            return this.fuel;
        }

    }
}
