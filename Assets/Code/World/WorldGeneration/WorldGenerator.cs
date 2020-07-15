using Assets.Code.World.Chunks;
using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using Assets.Code.World.WorldGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace Assets.Code.World
{

    
    static class WorldGenerator
    {
        [SerializeField]private const float defaultChunkWidth = 60;
        [SerializeField] private const float defaultChunkHeight = 10;
        [SerializeField] private const int maxPositionIterations = 1000;

        public static Chunk GenerateChunk(Vector2 leftMost)
        {
            return GenerateChunk(new Vector2(defaultChunkWidth, defaultChunkHeight), leftMost);
        }

        public static Chunk GenerateChunk(Vector2 size, Vector2 leftMost)
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
                    IObstacle io;
                    KeyValuePair<Vector2, Vector2> valuePair = FindSuitablePosition(chunk);
                    if (!valuePair.Key.Equals(Vector2.negativeInfinity))
                    {
                        io = ObjectFactory.ObstacleFactory(chunk.transform.position, chunk);
                        io.GetGameObject().transform.position = valuePair.Key;
                        io.GetGameObject().GetComponent<BoxCollider2D>().size = valuePair.Value; //TODO: Convert to a variable
                        curScore += io.Score;

                        //If AddObstacle returns false, then it has failed to add it to chunk buffer. 
                        //It cannot be accessed therefore needs to be destroyes
                        if (!chunk.AddObstacle(io)) 
                            GameObject.Destroy(io.GetGameObject()); 
                    }
                    else
                    {
                        Debug.Log("KVP is negInf");
                    }
                }
            }
            return 0;
        }
        private static KeyValuePair<Vector2 , Vector2>FindSuitablePosition(Chunk c)
        {
            IObstacle[] obstacles = c.Obstacles;
            Vector2 candidatePos = Vector2.zero;
            Vector2 candidateSize = Vector2.zero;
            int iteration = 0;
            float xmax = c.gameObject.transform.position.x + c.Size.x/2;
            float xmin = c.gameObject.transform.position.x - c.Size.x / 2;

            while (iteration < 1000)
            {
                iteration++;
                candidateSize = new Vector2(UnityEngine.Random.Range(1, 3), UnityEngine.Random.Range(1, 3));//TODO: Attach candidateSize to a variable.
                candidatePos = new Vector2(
                    UnityEngine.Random.Range(xmin + candidateSize.x/2, xmax - candidateSize.x/2), //x
                    UnityEngine.Random.Range(0 + candidateSize.y/2, c.Size.y - candidateSize.y/2) //y
                    );
                foreach (IObstacle obstacle in obstacles) //TODO: First obstacle should be seperely created since all will be null otherwise
                {
                    if ( c.ObstalcesCreated == 0 || (obstacle != null && 
                        !IsOverlapping(candidateSize, candidatePos, Vector2.zero, obstacle.GetGameObject().transform.position)) )
                    { //TODO: change Vector 0 with the actual size of this object
                        return new KeyValuePair<Vector2, Vector2>(candidatePos, candidateSize);
                    }
                    
                }
            }
            return new KeyValuePair<Vector2, Vector2>(Vector2.negativeInfinity, Vector2.negativeInfinity);
        }       
        
        /// <summary>
        /// Check if two rectangular objects of size (size1 and size2) and position (pos1 and pos2) are overlapping with each other in 2D space <br></br>
        /// Assumes no rotation is applied to these objects<br></br>
        /// Tangents are considered overlaps
        /// </summary>
        private static bool IsOverlapping(Vector2 size1, Vector2 pos1, Vector2 size2, Vector2 pos2)
        {
            float minX1, maxX1, minY1, maxY1;
            float minX2, maxX2, minY2, maxY2;

            minX1 = pos1.x - size1.x / 2;
            maxX1 = pos1.x + size1.x / 2;

            minX2 = pos2.x - size2.x / 2;
            maxX2 = pos2.x + size2.x / 2;

            minY1 = pos1.y - size1.y / 2;
            maxY1 = pos1.y + size1.y / 2;

            minY2 = pos2.y - size2.y / 2;
            maxY2 = pos2.y + size2.y / 2;

            //case 1: one is left of the other
            if (maxX2 < minX1 || maxX1 < minX2) return false;
            //case 2: one is on top of other
            if(maxY1 < minY2 || maxY2 < minY1) return false;
            //case 3: one is below other
            if (maxY2 < minY1 || maxY1 < minY2) return false;
            //case 4: one is right of other
            if (maxX1 < minX2 || maxX2 < minX1) return false;
             
            return true;
        }
    }
}
