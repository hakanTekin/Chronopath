using Assets.Code.World.Chunks;
using Assets.Code.World.Obstacle;
using Assets.Code.World.Obstacle.Decorator;
using Assets.Code.World.Obstacles.Decorator.Blip;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTest : MonoBehaviour
{
    Chunk c;
    // Start is called before the first frame update
    void Start()
    {
        IObstacle io = gameObject.GetComponentInChildren<Obstacle>();
        c = gameObject.GetComponent<Chunk>();
        c.AddObstacle(new DynamicMovementDecorator(new ExistingBlip(io), Vector2.zero, 7f));
    }
    private void FixedUpdate()
    {
        c.UpdateBlippers();
    }
    // Update is called once per frame
}
