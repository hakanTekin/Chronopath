﻿using System;
using UnityEngine;

namespace Assets.Code.World.Obstacle.Decorator
{
    enum MovementType
    {
        None = 0,
        Static = 1,
        Dynamic = 2
    }
    class MovementDecorator : ObstacleDecorator
    {

        public MovementDecorator(IObstacle w) : base(w)
        {
            Score = score + base.Score;
            this.GetGameObject().GetComponent<SpriteRenderer>().color = Color.white;

        }

        public string GetType()
        {
            throw new NotImplementedException();
        }

        public override void UpdateObstacle()
        {
            wrappee.UpdateObstacle();

        }
    }
}
