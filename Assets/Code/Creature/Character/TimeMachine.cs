﻿using Assets.Code.Tools;
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
        public int fuel;

        public TimeMachine(int f = int.MaxValue, int tad = 2)
        {
            this.timeAffectionDelta = tad;
            this.fuel = f;
        }
        public bool ChangeTime(World.World world, bool isIncrease , int delta = 1)
        {

            if (world is null)
            {
                throw new ArgumentNullException(nameof(world));
            }
            if (fuel > 0)
            {
                delta *= timeAffectionDelta;
                delta *= isIncrease ? 1 : -1;

                world.ChangeWorldTime(delta, true);
                updateFuel(-1 * Math.Abs(delta));//Ensures an always negative fuelDelta. Since delta can either be negative or positive. But fuelDelta is always supposed to be negative.}
                return true;
            }
            else return false; //Could not change time
        }
        public int updateFuel(int fuelDelta)
        {
            this.fuel += fuelDelta;
            return this.fuel;
        }

    }
}
