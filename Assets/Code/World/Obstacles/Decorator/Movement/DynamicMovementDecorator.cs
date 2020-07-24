using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private float currendDistanceTraveled = 0;
        private float pathLength;
        /// <summary>
        /// Direction and length of this objects path
        /// </summary>
        private Vector2 path;
        private Vector2 pathDirection;
        /// <summary>
        /// This obstacles movement speed
        /// </summary>
        private float speed;
        [SerializeField] private static int score = 50;


        /// <summary>
        /// <br>A moving obstacle.</br>
        /// <br><paramref name="path"/> should contain the direction and lengt of this object's movement</br>
        /// </summary>
        /// <param name="w"></param>
        /// <param name="path"></param>
        public DynamicMovementDecorator(IObstacle w, Vector2 path, float speed) : base(w)
        {
            Score = score + base.Score;
            this.path = path;
            this.pathDirection = path.normalized;
            pathLength = path.magnitude;
            this.speed = speed;
            w.GetGameObject().SendMessageUpwards("AssignCoroutine", MoveObstacle());
            this.GetGameObject().GetComponent<SpriteRenderer>().color = Color.blue;
        }


        public string GetType()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObstacle()
        {
            wrappee.UpdateObstacle();
  
        }

        IEnumerator MoveObstacle() {
            while (true) {
                yield return new WaitForFixedUpdate();
                Vector2 x = pathDirection * speed * Time.deltaTime;
                this.GetGameObject().transform.Translate(x);
                currendDistanceTraveled += speed * Time.deltaTime;

                if (currendDistanceTraveled > pathLength)
                {
                    pathDirection *= -1;
                    currendDistanceTraveled = 0;
                }
            }
            
        }
    }
}
