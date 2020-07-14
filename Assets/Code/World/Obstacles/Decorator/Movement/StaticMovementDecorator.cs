using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Code.World.Obstacle.Decorator
{
    class StaticMovementDecorator:MovementDecorator
    {
        private int score = 50;
        public override int Score { get => score; set => score = wrappee.Score + value; }
        public StaticMovementDecorator(IObstacle w) : base(w)
        {

        }

        public string GetType()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObstacle()
        {
            wrappee.UpdateObstacle();
            Debug.Log("StaticMovementUpdate");
        }
    }
}
