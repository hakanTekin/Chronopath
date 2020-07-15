using Assets.Code.World.Chunks;
using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.World.WorldGeneration
{
    static class ObjectFactory
    {
        private static int chunkID = 0;
        public static GameObject ChunkFactory(Vector2 leftMost, Vector2 size)
        {
            GameObject nc = new GameObject("chunk" + (chunkID));
            Chunk c = nc.AddComponent<Chunk>();
            c.Id = chunkID++;
            c.LeftMostPosition = leftMost;
            c.Size = size;
            leftMost.x += size.x; //TODO: Check if correct ? leftmost should be -= size.x
            nc.transform.position = leftMost;
            return nc;
        }

        public static IObstacle ObstacleFactory(Vector2 pos, Chunk parent)
        {
            GameObject no = new GameObject();
            IObstacle io = no.AddComponent<Obstacle.Obstacle>(); //Concrete Obstacle Added
            io = CreateDecorationForObject(ref io);
            no.transform.SetParent(parent.gameObject.transform);
            io.UpdateObstacle();
            return io;
        }

        private static IObstacle CreateDecorationForObject(ref IObstacle concreteObstacle)
        {
            int movement = UnityEngine.Random.Range(0, 2);
            int blip = UnityEngine.Random.Range(0, 2);


            /*TODO:
             * Create better generation algorithm.
             * - Static objects should be more common
             * - Dynamic/Static ratio should increase while player continues playing.
             * - Blippers should be very common (atlesat a ratio of 3 blipper/non-blipper)
             */
            IObstacle decorated = concreteObstacle;
            
            switch (blip)
            {
                case 0:
                    break;
                case 1:
                    decorated = new BlipDecorator(decorated);
                    break;
                default:
                    Debug.LogError("Impossbile Blip Decoration Randomized");
                    break;
            }

            switch (movement)
            {
                case 0: //Static
                    decorated = new StaticMovementDecorator(decorated);
                    break;
                case 1: //Dynamic
                    decorated = new DynamicMovementDecorator(decorated, Vector2.zero); //TODO: Change vector.zero to a meaningful path;
                    break;
                default:
                    Debug.LogError("Impossbile Movement Decoration Randomized");
                    break;
            }
            return decorated;
        }
    }
}