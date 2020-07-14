using Assets.Code.World.Chunks;
using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using Assets.Code.World.WorldGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Code.World
{

    
    static class WorldGenerator
    {
        [SerializeField]private const float defaultChunkWidth = 60;

        [SerializeField] private const int maxPositionIterations = 1000;

        public static Chunk GenerateChunk(Vector2 leftMost)
        {
            return GenerateChunk(defaultChunkWidth, leftMost);
        }

        public static Chunk GenerateChunk(float size, Vector2 leftMost)
        {
            GameObject newChunk = ObjectFactory.ChunkFactory(leftMost, size);
            GenerateObstacles(newChunk.GetComponent<Chunk>());
            return newChunk.GetComponent<Chunk>();
        }

        private static int GenerateEnemies(Chunk chunk)
        {
            return 0;
        }
        private static int GenerateObstacles(Chunk chunk)
        {
            int curScore = 0;
            for (int i = 0; i < chunk.Obstacles.Length; i++)
            {
                if(chunk.MaxObjectScore > curScore)
                {
                    //Instantiate a gameobject and decorate its obstacle component
                    IObstacle io = ObjectFactory.ObstacleFactory(chunk.transform.position, chunk);
                    curScore += io.Score;
                    chunk.AddObstacle(io);
                }
            }
            return 0;
        }
        private static Vector2 FindSuitablePosition(Chunk c, IObstacle newObstacle)
        {
            IObstacle[] obstacles = c.Obstacles;
            Vector2 candidate = Vector2.zero;
            int iteration = 0;

            while (iteration < 1000)
            {
                iteration++;

                candidate = new Vector2(UnityEngine.Random.Range(0, 0), UnityEngine.Random.Range(0, 0));
                foreach (IObstacle obstacle in obstacles)
                {

                }
                break;
            }
            return new Vector2(0, 0);
        }            
    }
}
