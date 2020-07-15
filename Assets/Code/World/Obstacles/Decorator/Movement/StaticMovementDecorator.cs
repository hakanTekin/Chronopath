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
        [SerializeField] private static int score = 50;
        public StaticMovementDecorator(IObstacle w) : base(w)
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
            Debug.Log("StaticMovementUpdate");
        }
    }
}
