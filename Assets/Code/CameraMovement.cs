using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject characterToFollow;
    private Camera camera;
    /// <summary>
    ///<br>Maximum percentage of screen space size compared to player's position for allowing player to change position without moving the camera.</br>
    ///<br></br>
    ///<example>If viewport width is 10, in position (5,0). And if the player is in (3,0) percentage value is 30</example>
    /// </summary>
    public float maxFollowAfterPercentage = 50;

    /// <summary>
    /// Z axis position of the camera. In orthographic mode, this is not important as long as it is not zero
    /// </summary>
    public float cameraDepthPosition = -1;
    /// <summary>
    ///<br>Maximum percentage of screen space size compared to player's position for allowing player to change position without moving the camera.</br> 
    ///<br>Same principle as <see cref="maxFollowAfterPercentage"/></br>
    /// </summary>
    public float minFollowAfterPercentage = 2;

    private float cameraWidth;

    public bool isInMaxLimit, isInMinLimit;
    // Start is called before the first frame update
    void Start()
    {

        gameObject.transform.position = new Vector3(characterToFollow.transform.position.x, characterToFollow.transform.position.y, cameraDepthPosition);
        camera = GetComponent<Camera>();

        if (camera == null)
            camera = gameObject.AddComponent<Camera>();

        if (camera.orthographic)
            cameraWidth = camera.orthographicSize * camera.aspect; //Camera.ortographicSize is the height value. Width is automatically set in unity's code. So width is equal to aspect * orth.Size
        Camera.SetupCurrent(camera);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        adjustCamera();

    }
    private void adjustCamera()
    {
        //It seems to be calculating from the entire map when percentage is smaller. not relative positions
        //X axis
        Vector2 adjustAmount = new Vector2(0, 0);
        float percentage = calculateCharacterPositionToScreenPercentage();
        if (percentage > maxFollowAfterPercentage) //Camera needs to be moved right, character is too far right in screen.
            adjustAmount.x = characterToFollow.transform.position.x - gameObject.transform.position.x;
        else if (percentage < minFollowAfterPercentage)
        {
            isInMinLimit = true;
        }
        //Y axis
        adjustAmount.y = characterToFollow.transform.position.y - gameObject.transform.position.y + 2.5f;

        //Translate the transform
        gameObject.transform.Translate(adjustAmount);
    }
    public bool IsInMaxLimit() {
        isInMaxLimit = maxFollowAfterPercentage <= calculateCharacterPositionToScreenPercentage();
        return isInMaxLimit;

    }

    public bool IsInMinLimit()
    {
        isInMinLimit = minFollowAfterPercentage >= calculateCharacterPositionToScreenPercentage();
        return isInMinLimit;
    }
    private float calculateCharacterPositionToScreenPercentage(GameObject character = null)
    {
        if(character == null)
        {
            if (characterToFollow == null)
            {
                Debug.LogError("No character to follow");
                return float.NegativeInfinity;
            }
            else
                character = characterToFollow;
        }
        float percentage = (character.transform.position.x - (camera.transform.position.x-(cameraWidth / 2f)))
            / 
            cameraWidth
            * 100f;

        return percentage;
    }
}
