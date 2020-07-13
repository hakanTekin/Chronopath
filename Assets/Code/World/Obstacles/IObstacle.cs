using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacle
{

    interface IObstacle
    {
        string GetType();
        void UpdateObstacle();
        GameObject GetGameObject();
    }
}
