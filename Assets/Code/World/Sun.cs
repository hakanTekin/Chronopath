using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Sun : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private float maxDegree = -10, minDegree = 180;
    [SerializeField] private Vector3 pos0;
    [SerializeField] private Vector3 pos1;
    [SerializeField] private Vector3 pos2;

    [SerializeField] private Light directionalLight;
    void Start()
    {
        directionalLight = gameObject.GetComponentInChildren<Light>();
        
        if (directionalLight != null && directionalLight.type == UnityEngine.LightType.Directional)
        {

        }
        else directionalLight = null;

        Vector3 pos = Bezier.CalculatePoint(0, pos0, pos1, pos2);
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLight(float worldTime, float worldLimit)
    {
        float t = (worldTime / worldLimit);
        Vector3 pos = Bezier.CalculatePoint(t, pos0, pos1, pos2);
        gameObject.GetComponent<RectTransform>().anchoredPosition = pos;
        bool isAngleIncreasing = minDegree - maxDegree > minDegree ? false : true;//TODO: find a way to change rotation
        directionalLight.GetComponent<RectTransform>().rotation = Quaternion.Euler(minDegree + t*(maxDegree - minDegree), 0, 0);
    }
}
