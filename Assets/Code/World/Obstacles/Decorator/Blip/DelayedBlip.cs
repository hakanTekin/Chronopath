using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Code.World.Obstacles.Decorator.Blip
{
    /// <summary>
    /// <br>A special blip type.</br>
    /// <br>Object starts blipping in a specific world time and ends blipping in another</br>
    /// <br>Behaves like a standart blipper when blipping is active.</br>
    /// <br>When blipping is deactive, </br>
    /// </summary>
    class DelayedBlip : BlipDecorator
    {
        /// <summary>
        /// World time for this object to start blipping (if in existance period)
        /// </summary>
        protected float blipStart;

        /// <summary>
        /// World time for this object to stop blipping (if in existance period)
        /// </summary>
        protected float blipEnd;
        public DelayedBlip(IObstacle w):base(w){

        }

        public override void UpdateObstacle()
        {
            wrappee.UpdateObstacle();
        }
    }
}
