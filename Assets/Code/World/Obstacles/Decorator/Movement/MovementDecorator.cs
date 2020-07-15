using System;
using UnityEngine;

namespace Assets.Code.World.Obstacle.Decorator
{
    class MovementDecorator : ObstacleDecorator
    {

        public MovementDecorator(IObstacle w) : base(w)
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
            Debug.Log("MovementDecoratorUpdate");
        }
    }
}
