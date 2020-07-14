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
        public static GameObject ChunkFactory(Vector2 leftMost, float size)
        {
            GameObject nc = new GameObject("chunk" + (chunkID));
            Chunk c = nc.AddComponent<Chunk>();
            c.Id = chunkID++;
            c.LeftMostPosition = leftMost;
            c.Size = size;
            leftMost.x += size;
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
            return new BlipDecorator(new StaticMovementDecorator(concreteObstacle));
        }
    }
}
