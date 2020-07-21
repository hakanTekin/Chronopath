﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Assets.Code.World.Obstacle
{
    class Obstacle : MonoBehaviour, IObstacle
    {

        SpriteShape shape;
        
        Vector2 size;
        BoxCollider2D collider;
        private int score = 0;

        private SpriteShapeController shapeController;
        private SpriteShapeRenderer shapeRenderer;
        
        public int Score { get => score; set => score = value; }

        private void Awake()
        {
            collider = gameObject.AddComponent<BoxCollider2D>();
            collider.size = new Vector2(2,2); //TODO: Why?
            collider.offset = Vector2.zero;
            shapeRenderer = gameObject.AddComponent<SpriteShapeRenderer>();
            shapeController = gameObject.AddComponent<SpriteShapeController>();
            shape = Resources.Load<SpriteShape>("ObstacleShapeProfile");
            shapeController.spriteShape = shape;
        }

        public Spline CreateSpline()
        {
            Spline box = shapeController.spline;
            
            Vector3 lt, rt, lb, rb; //left-top, right-top, left-bottom, right-bottom;
            lt = new Vector3(
                -1 * (collider.size.x / 2),
                collider.size.y / 2,
                gameObject.transform.position.z
                );

            rt = new Vector3(
                collider.size.x / 2,
                collider.size.y / 2,
                gameObject.transform.position.z
                );

            lb = new Vector3(
                -1 * (collider.size.x / 2),
                -1 * (collider.size.y / 2),
                gameObject.transform.position.z
                );

            rb = new Vector3(
                collider.size.x / 2,
               -1 * (collider.size.y / 2),
                gameObject.transform.position.z
                );

            box.InsertPointAt(0, lt);
            box.InsertPointAt(1, rt);
            box.InsertPointAt(2, rb);
            box.InsertPointAt(3, lb);

            shapeController.splineDetail = 1;
            shapeController.RefreshSpriteShape();
            return box;
        }
        public void UpdateObstacle()
        {
            Debug.Log("Obstacle Updated");
        }

        GameObject IObstacle.GetGameObject()
        {
            return this.gameObject;
        }
        public World GetWorld()
        {
            return FindObjectOfType<World>();
        }

        string IObstacle.GetType()
        {
            throw new NotImplementedException();
        }
    }
}
