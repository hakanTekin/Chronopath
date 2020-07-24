using Assets.Code.World.Chunks;
using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using Assets.Code.World.Obstacles.Decorator.Blip;
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
            Vector2 pos = new Vector2(leftMost.x + size.x / 2, size.y/2);
            c.gameObject.transform.position = pos;
            c.MaxObjectScore = c.Id/2 * 100 + c.MaxObjectScore; //MaxObject Score is set to base amount in chunk awake() method. Then for each 2 chunks, adittional 100 points is allocated for new chunks.
            return nc;
        }

        public static IObstacle ObstacleFactory(Chunk parent, Vector3 pos, Vector2 size)
        {
            GameObject no = new GameObject();
            Obstacle.Obstacle o = no.AddComponent<Obstacle.Obstacle>(); //Concrete Obstacle Added
            o.gameObject.transform.position = pos;
            o.gameObject.GetComponent<BoxCollider2D>().size = size; //TODO: Convert to a variable
            o.CreateSpline();

            IObstacle io = o;

            io = CreateDecorationForObject(ref io);
            no.transform.SetParent(parent.gameObject.transform);                        
            return io;
        }

        private static IObstacle CreateDecorationForObject(ref IObstacle concreteObstacle)
        {
            MovementType movement = (MovementType)UnityEngine.Random.Range(0, (int)MovementType.Dynamic + 1);
            BlipType blip = (BlipType)UnityEngine.Random.Range(0, (int)BlipType.Delayed + 1);


            /*TODO:
             * Create better generation algorithm.
             * - Static objects should be more common
             * - Dynamic/Static ratio should increase while player continues playing.
             * - Blippers should be very common (atlesat a ratio of 3 blipper/non-blipper)
             */
            IObstacle decorated = concreteObstacle;
            
            switch (blip)
            {
                case BlipType.Standart:
                    decorated = new BlipDecorator(decorated);
                    break;

                case BlipType.Existing:
                    decorated = new ExistingBlip(decorated);

                    break;

                case BlipType.Delayed:
                    decorated = new DelayedBlip(decorated);

                    break;

                case BlipType.None:
                    break;
                default:
                    Debug.LogError("Impossbile Blip Decoration Randomized");
                    break;
            }

            switch (movement)
            {
                case MovementType.None:

                    decorated = new MovementDecorator(decorated);
                    break;

                case MovementType.Static: //Static //TODO: Disabled for now. Find a use 
                    //decorated = new StaticMovementDecorator(decorated);

                    decorated = new MovementDecorator(decorated);
                    break;

                case MovementType.Dynamic: //Dynamic

                    decorated = new DynamicMovementDecorator(decorated, new Vector2(4,4), 7f); //TODO: Change vector.zero to a meaningful path;
                    break;

                default:
                    Debug.LogError("Impossbile Movement Decoration Randomized");
                    break;
            }
            return decorated;
        }
    }
}