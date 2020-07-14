using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacle.Decorator
{
    class MovementDecorator : ObstacleDecorator
    {
        private int score = 0;
        public override int Score { get => score; set => score = wrappee.Score + value; }
        public MovementDecorator(IObstacle w):base(w)
        {
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
