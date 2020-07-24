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
        [SerializeField] private const float defaultChunkHeight = 7;
        [SerializeField] private const int maxPositionIterations = 1000;
        
        public static Chunk GenerateChunk(Vector2 leftMost)
        {
            return GenerateChunk(new Vector2(defaultChunkWidth, defaultChunkHeight), leftMost);
        }
        public static Chunk GenerateChunk(Vector2 leftMost, Camera cam)
        {
            float width = defaultChunkWidth;
            if(width < cam.orthographicSize * cam.aspect * 2)
            {              
                width = cam.orthographicSize * cam.aspect * 2 + 5;
            }
            return GenerateChunk(new Vector2(width, defaultChunkHeight), leftMost);
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
                        io = ObjectFactory.ObstacleFactory(chunk, valuePair.Key, valuePair.Value);
                        curScore += io.Score;

                        //If AddObstacle returns false, then it has failed to add it to chunk buffer. 
                        //It cannot be accessed therefore needs to be destroyes
                        if (!chunk.AddObstacle(io))
                            GameObject.Destroy(io.GetGameObject()); 
                    }
                    else
                    {
                        Debug.LogError("KVP is negInf");
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
            float xmax = c.gameObject.transform.position.x + (c.Size.x / 2);
            float xmin = c.gameObject.transform.position.x - (c.Size.x / 2);

            float ymax = c.gameObject.transform.position.y + (c.Size.y / 2);
            float ymin = c.gameObject.transform.position.y - (c.Size.y / 2);

            bool isSuitable;
            while (iteration < 1000)
            {
                isSuitable = true; //Flag needed for continuing or stopping iterations. True when no overlaps exist with the currens candidates.
                iteration++;
                candidateSize = new Vector2(UnityEngine.Random.Range(1f, 3f), UnityEngine.Random.Range(1f, 3f));//TODO: Attach candidateSize to a variable.
                candidatePos = new Vector2(
                    UnityEngine.Random.Range(xmin + candidateSize.x/2, xmax - candidateSize.x/2), //x
                    UnityEngine.Random.Range(ymin - candidateSize.y/2, ymax - candidateSize.y/2) //y
                    );
                foreach (IObstacle obstacle in obstacles)
                {
                    if(obstacle != null)
                    {
                        if (IsOverlapping(candidateSize, candidatePos, obstacle.GetGameObject().GetComponent<BoxCollider2D>().size, obstacle.GetGameObject().transform.position)) //TODO: Dont use boxcollider.size, use a seperate variable)
                        {
                            //Cant return here since it shouldnt return after just checking the first obstacle
                            //If there is an overlap with any existing object, break the loop and try another point and size.
                            isSuitable = false;
                            break;
                        }
                    }
                }
                if(isSuitable) return new KeyValuePair<Vector2, Vector2>(candidatePos, candidateSize);
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

            minX1 = pos1.x - size1.x / 2f;
            maxX1 = pos1.x + size1.x / 2f;

            minX2 = pos2.x - size2.x / 2f;
            maxX2 = pos2.x + size2.x / 2f;

            minY1 = pos1.y - size1.y / 2f;
            maxY1 = pos1.y + size1.y / 2f;

            minY2 = pos2.y - size2.y / 2f;
            maxY2 = pos2.y + size2.y / 2f;

            //case 1: one is left of the other
            if (maxX2 < minX1) return false;
            //case 2: one is on top of other
            if(maxY1 < minY2) return false;
            //case 3: one is below other
            if (maxY2 < minY1) return false;
            //case 4: one is right of other
            if (maxX1 < minX2) return false;

            return true;
        }
    }
}
