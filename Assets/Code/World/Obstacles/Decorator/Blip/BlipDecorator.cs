using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.World.Obstacle.Decorator
{
    class BlipDecorator:ObstacleDecorator
    {
        public BlipDecorator(IObstacle w) : base(w)
        {

        }

        public string GetType()
        {
            throw new NotImplementedException();
        }

        public void UpdateObstacle()
        {
            throw new NotImplementedException();
        }
    }
}
