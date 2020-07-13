﻿using Assets.Code.Creature.Enemy;
using Assets.Code.World.Obstacle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Code.World.Chunks
{
    class Chunk : MonoBehaviour                                        

    {
        public static int maxNumberOfEnemies = 5;
        public static int maxNumberOfObstacles = 20;
        private Enemy[] enemies;
        private IObstacle[] obstacles;
        private Vector2 leftMostPosition;
        private float size;
        private int id;

        /// <summary>
        /// The maximum allowed number of points chunk can contain.<br></br>Each enemy and obstacle has its objectScore and they are summed in the chunk they are in
        /// </summary>
        private int maxObjectScore;
        public int MaxObjectScore { get => maxObjectScore; }

        public float Size { get => size; set => size = value; }
       
        /*
         * Chunks are gameObjects that contain multiple other gameObjects
         * The children are of Type Enemy and IObstacle.
         * Each gameObject should be instantiated, therefore when destroying them Destroy() method should be used for each item
         */
        public Enemy[] Enemies { get => enemies; set => enemies = value; }
        public IObstacle[] Obstacles { get => obstacles; set => obstacles = value; }
        public int Id { get => id; set => id = value; }
        public Vector2 LeftMostPosition { get => leftMostPosition; set => leftMostPosition = value; }

        private void Awake()
        {
            Enemies = new Enemy[maxNumberOfEnemies];
            obstacles = new IObstacle[maxNumberOfObstacles];
            maxObjectScore = 1000;
        }
        public bool MoveChunk(float delta)
        {
            gameObject.transform.Translate(Vector3.right * delta);
            return true;
        }

        public bool RemoveChunkContent()
        {
            for(int i = 0; i<Enemies.Length; i++)
            {
                Destroy(Enemies[i]);
            }
            for (int i = 0; i < obstacles.Length; i++)
            {
                Destroy(obstacles[i].GetGameObject());
            }

            Destroy(this.gameObject);
            return false;
        }
        public bool UpdateBlippers(float worldTime)
        {
            return false;
        }            


    }
}
