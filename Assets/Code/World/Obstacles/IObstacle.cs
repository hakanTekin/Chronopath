using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.Obstacle
{

    interface IObstacle
    {
        int Score { get; set; }
        string GetType();
        void UpdateObstacle();
        GameObject GetGameObject();
        World GetWorld();
    }
}
