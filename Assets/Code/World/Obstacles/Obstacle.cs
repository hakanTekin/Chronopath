using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacle
{
    class Obstacle : MonoBehaviour, IObstacle
    {
        public void UpdateObstacle()
        {
            throw new NotImplementedException();
        }

        GameObject IObstacle.GetGameObject()
        {
            return this.gameObject;
        }

        string IObstacle.GetType()
        {
            throw new NotImplementedException();
        }
    }
}
