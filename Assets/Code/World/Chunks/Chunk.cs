using Assets.Code.Creature.Enemy;
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
        //TODO: Create a density system and adjust the amounts to the chunk sizes.
        public static int maxNumberOfEnemies = 5;
        public static int maxNumberOfObstacles = 20;

        private Enemy[] enemies;
        private IObstacle[] obstacles;

        [SerializeField] private int obstalcesCreated = 0;
        [SerializeField] private int enemiesCreated = 0;

        [SerializeField] private Vector2 leftMostPosition;
        [SerializeField] private Vector2 size;
        private int id;

        /// <summary>
        /// The maximum allowed number of points chunk can contain.<br></br>Each enemy and obstacle has its objectScore and they are summed in the chunk they are in
        /// </summary>
        private int maxObjectScore;
        public int MaxObjectScore { get => maxObjectScore; }
        public Vector2 Size { get => size; set => size = value; }
       
        /*
         * Chunks are gameObjects that contain multiple other gameObjects
         * The children are of Type Enemy and IObstacle.
         * Each gameObject should be instantiated, therefore when destroying them Destroy() method should be used for each item
         */
        public Enemy[] Enemies { get => enemies; }
        public IObstacle[] Obstacles { get => obstacles; }
        public int Id { get => id; set => id = value; }
        public Vector2 LeftMostPosition { get => leftMostPosition; set => leftMostPosition = value; }
        public int EnemiesCreated { get => enemiesCreated; set => enemiesCreated = value; }
        public int ObstalcesCreated { get => obstalcesCreated; set => obstalcesCreated = value; }

        private void Awake()
        {
            enemies = new Enemy[maxNumberOfEnemies];
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
        public bool UpdateBlippers()
        {
            foreach (IObstacle io in obstacles)
                if (io != null)
                    io.UpdateObstacle();
            return false;
        }            

        public bool AddObstacle(IObstacle obstacle)
        {
            for(int i=0; i<obstacles.Length; i++)
            {
                if(obstacles[i] == null)
                {
                    obstacles[i] = obstacle;
                    this.ObstalcesCreated++;
                    return true;
                }
            }

            return false;
        }
    }
}
