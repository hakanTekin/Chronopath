using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.World.Obstacle.Decorator
{
    class DynamicMovementDecorator:MovementDecorator
    {
        private int score = 100;
        public override int Score { get => score; set => score = wrappee.Score + value; }
        public DynamicMovementDecorator(IObstacle w) : base(w)
        {

        }

        public string GetType()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObstacle()
        {
            throw new NotImplementedException();
        }
    }
}
