using Assets.Code.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    // Start is called before the first frame update
    World world;
    Gradient bgGradient;
    SpriteRenderer renderer;
    void Start()
    {
        world = FindObjectOfType<World>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        bgGradient = new Gradient();
        bgGradient.mode = GradientMode.Blend;
        GradientColorKey[] gradientColorKeys = new GradientColorKey[5];
        gradientColorKeys[0].color = new Color32(3,194,252,1);
        gradientColorKeys[0].time = 0f;
        
        gradientColorKeys[1].color = new Color32(3, 227, 252,1);
        gradientColorKeys[1].time = 0.3f;

        gradientColorKeys[2].color = new Color32(252, 165, 3,1);
        gradientColorKeys[2].time = .7f;

        gradientColorKeys[3].color = new Color32(0,106,181,1);
        gradientColorKeys[3].time = .85f;

        gradientColorKeys[4].color = new Color32(0, 49, 84,1);
        gradientColorKeys[4].time = .95f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[5];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[2].alpha = 1.0f;
        alphaKeys[3].alpha = 1.0f;
        alphaKeys[4].alpha = 1.0f;
        bgGradient.SetKeys(gradientColorKeys, alphaKeys);
    }
    Color colorEvaluated;
    // Update is called once per frame
    private void FixedUpdate()
    {
        var v = (float)world.worldTime / (float)world.WorldEndTime % 1f;
        colorEvaluated = bgGradient.Evaluate(v);
        renderer.color = colorEvaluated;
    }
}
