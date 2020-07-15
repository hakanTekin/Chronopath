using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacle.Decorator
{
    class BlipDecorator:ObstacleDecorator
    {
        private int score = 50;
        public override int Score { get => score; set => score = value; }
        public BlipDecorator(IObstacle w) : base(w)
        {
            
            Score = score + base.Score;
        }

        public string GetType()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObstacle()
        {
            wrappee.UpdateObstacle();
            Debug.Log("BlipDecoratorUpdate");
        }
    }
}
