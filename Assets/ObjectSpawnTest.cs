using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnTest : MonoBehaviour
{

    GameObject g = new GameObject();
    // Start is called before the first frame update
    void Start()
    {
        BoxCollider2D c = g.AddComponent<BoxCollider2D>();
        c.size = new Vector2(1, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
