using Assets.Code.World.Chunks;
using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World
{

    
    static class WorldGenerator
    {
        private static GameObject ChunkFactory(Vector2 leftMost, float size)
        {
            GameObject nc = new GameObject("chunk" + (chunkID));
            Chunk c = nc.AddComponent<Chunk>();
            c.Id = chunkID++;
            c.LeftMostPosition = leftMost;
            c.Size = size;
            leftMost.x += size;
            nc.transform.position = leftMost;
            return nc;
            throw new NotImplementedException();
        }
        private static GameObject ObstacleFactory(IObstacle obstacle, Vector2 pos)
        {
            GameObject gameObject = new GameObject();
            throw new NotImplementedException();
        }


        private static int chunkID = 0;
        [SerializeField]private const float defaultChunkWidth = 60;

        public static GameObject GenerateChunk(Vector2 leftMost)
        {
            return GenerateChunk(defaultChunkWidth, leftMost);
        }

        public static GameObject GenerateChunk(float size, Vector2 leftMost)
        {
            GameObject newChunk = ChunkFactory(leftMost, size);
            GenerateObstacles(newChunk.GetComponent<Chunk>());
            return newChunk;
            throw new NotImplementedException();
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
                }
            }
            return 0;
        }
    }
}
