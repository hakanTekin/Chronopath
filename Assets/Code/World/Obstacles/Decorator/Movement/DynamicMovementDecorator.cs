using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacle.Decorator
{
    /// <summary>
    /// A movement type for obstacles. Makes the object move.
    /// </summary>
    class DynamicMovementDecorator:MovementDecorator
    {
        /// <summary>
        /// <br>A moving obstacle.</br>
        /// <br><paramref name="path"/> should contain the direction and lengt of this object's movement</br>
        /// </summary>
        /// <param name="w"></param>
        /// <param name="path"></param>
        
        
        [SerializeField] private static int score = 50;
        public DynamicMovementDecorator(IObstacle w, Vector2 path) : base(w)
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
            Debug.Log("Dyanmic Updated");
        }
    }
}
