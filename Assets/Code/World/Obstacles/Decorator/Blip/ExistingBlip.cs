using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacles.Decorator.Blip
{

    /// <summary>
    /// A special blip type. Obstacle starts its existance in a specific world time and ends it in another.
    /// </summary>
    class ExistingBlip : BlipDecorator
    {
        /// <summary>
        /// World time for this object to appear.
        /// </summary>
        protected float existanceStart;
        /// <summary>
        /// World time for this object to cease its existance
        /// </summary>
        protected float existanceEnd;
        public ExistingBlip(IObstacle w) : base(w)
        {
            ConfigureBlip();
        }
        protected override void ConfigureBlip()
        {
            float wet = 1000; //wet : World End Time, set to 1000 by default if there is no world found, this will be used
            if (world != null)
                wet = world.WorldEndTime;
            base.ConfigureBlip();
            existanceStart = UnityEngine.Random.Range(0, (int)(wet * 0.5)); //All will be created before half-worldMaxTime
            existanceEnd = UnityEngine.Random.Range(existanceStart + (int)(wet * 0.2), (int)(wet)); //Ends after existanceStart + 20% of worldMaxTime in minimum life span.

        }
        public override void UpdateObstacle()
        {
            float wet = 1000; //wet : World End Time, set to 1000 by default if there is no world found, this will be used
            if (world != null)
                wet = world.WorldEndTime;

            Debug.Log("ExistingBlip");
            if(existanceStart > wet) //Object does not yet exists
            {
                this.GetGameObject().SetActive(false);
            }
            else if(existanceEnd <= wet) //Object no longer exists
            {
                this.GetGameObject().SetActive(false);
            }else
            {
                this.GetGameObject().SetActive(true);
                wrappee.UpdateObstacle();
            }
            
        }
    }
}
