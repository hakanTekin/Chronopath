using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Code.World.Obstacle.Decorator
{
    class ObstacleDecorator : IObstacle
    {
        protected IObstacle wrappee;
        protected int score = 0;

        public ObstacleDecorator(IObstacle w)
        {
            
            this.wrappee = w;
            Score += w.Score;
        }
        
        public virtual int Score { get => score; set => score = value; }

        public GameObject GetGameObject()
        {
            return wrappee.GetGameObject();
        }

        public virtual void UpdateObstacle()
        {
            wrappee.UpdateObstacle();
            Debug.Log("Obstacle Decorator Updated");
        }

        string IObstacle.GetType()
        {
            throw new NotImplementedException();
        }
    }
}
