
using Assets.Code.World;
using Assets.Code.World.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class My2DCharacterController : MonoBehaviour
{
    public BoxCollider2D collisionBox;
    public float gravity;

    /// <summary>
    /// Real speed is the raw combination of all forces applied to this object. 
    /// </summary>
    public Vector2 realSpeed;
    /// <summary>
    /// Processed version of realSpeed. If the surrounding object are to move instead of character, this is necessary
    /// </summary>
    private Vector2 worldSpeed;

    public Rigidbody2D rigid;

    private bool isMovingWorld;

    private Dictionary<Collider2D, Vector2> collisions = new Dictionary<Collider2D, Vector2>();


    public bool isGrounded;
    public float characterSpeed = 10;
    public float jumpForce = 30;


    private Vector2 groundDetectionCheckPosition;
    private Vector2 groundCheckBoxSize;

    public CameraMovement CameraMovement;
    private World World;

    void Start()
    {
        isGrounded = false;
        collisionBox.enabled = true;        
        rigid.bodyType = RigidbodyType2D.Kinematic;
        groundCheckBoxSize = new Vector2(collisionBox.bounds.extents.x, 0.02f);
        World = FindObjectOfType<World>();
        if(CameraMovement == null)
        {
            CameraMovement = FindObjectOfType<Camera>().GetComponent<CameraMovement>();
        }
    }

    void Update()
    {
        //Handling input
        realSpeed.x = Input.GetAxis("Horizontal") * characterSpeed;
        worldSpeed.x = realSpeed.x;

        isMovingWorld = realSpeed.x > 0f && (CameraMovement != null) && CameraMovement.isInMaxLimit();
       
        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump(jumpForce);
    }

    void FixedUpdate()
    {
        if(!IsGrounded())
            ApplyGravity(); //Checks for and applies gravity

        //If character is touching ground, then there is no point having a velocity in negative y direction
        if (isGrounded && realSpeed.y < 0)
        {
            realSpeed.y = 0;
            
        }
            

        //Change where the character is looking according to its speed
        ApplyReactionSpeedToCollisions();

        worldSpeed = realSpeed;
        if (isMovingWorld) //If player tries to move forward but is in the camera follow limit, then move the entire world to back      
            worldSpeed.x = 0;

        if (isMovingWorld)
        {
            World.MoveChunks(realSpeed.x * -1 * Time.deltaTime);
        }

        MoveTo(worldSpeed);

    }


    /// <summary>
    ///Iterates through all the active collisions and disables movement in that direction by applying a counter speed
    ///This is similar to a 'reaction force' in concept
    /// </summary>
    void ApplyReactionSpeedToCollisions()
    {
        foreach (KeyValuePair<Collider2D, Vector2> kvp in collisions)
        {
            Vector2 temp = kvp.Value;
            float dotValue = Vector2.Dot(realSpeed, temp);
            if (dotValue > 0 && kvp.Key.IsTouching(collisionBox))
            {
                Vector2 speedInRotationToBeDisabled = temp * dotValue * -1;
                realSpeed.x += speedInRotationToBeDisabled.x;
                realSpeed.y += speedInRotationToBeDisabled.y;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D c)
    {
        Vector2 reactionForceRotation = CalculateCollisionDirection(c);    
        
        if (collisions.ContainsKey(c.collider)) //If the same key somehow exists, remove the old one
            collisions.Remove(c.collider);

        collisions.Add(c.collider, reactionForceRotation);
    }

    /// <summary>
    /// Gets a vector of magnitude 1, in the direction of a surface normal 
    /// </summary>
    /// <param name="collision"></param>
    /// <returns>Vector of magnitude 1 in the direction of surface normal</returns>
    private Vector2 CalculateCollisionDirection(Collision2D collision)
    {
        Vector2 v = collision.GetContact(0).normal;
        return v * -1;
    }

    /// <summary>
    /// Method for handling a Jump action
    /// </summary>
    /// <param name="jumpForce"></param>
    internal void Jump(float jumpForce)
    {
        isGrounded = false;
        realSpeed += new Vector2(0, jumpForce);
        worldSpeed += new Vector2(0, jumpForce);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (Collider2D c in collisions.Keys.ToList())
        {
            if (c == collision.collider)
            {
                collisions.Remove(c);
                IsGrounded();
                return;
            }
            
        }
        IsGrounded();
        return;
    }

    /// <summary>
    /// <para>
    /// Checks for a ground by using raycasts.
    /// </para>
    /// <br>A raycast does not ignore the collider if it is fired inside one. So to detect a ground,</br>
    /// <br>method checks the amount of hits there are and decides wether the character is touching ground or not.</br>
    /// </summary>
    /// <returns></returns>
    /// 
    
    private bool IsGrounded()
    {
        groundDetectionCheckPosition = collisionBox.bounds.center;
        groundDetectionCheckPosition.y -= collisionBox.bounds.extents.y;
        //RaycastHit2D[] c = new RaycastHit2D[20];
        Collider2D[] collider2 = new Collider2D[20];
        //int hitCount = Physics2D.RaycastNonAlloc(collisionBox.bounds.center, Vector2.down, c, collisionBox.bounds.extents.y+0.03f);

        int hitCount = Physics2D.OverlapBoxNonAlloc(groundDetectionCheckPosition, groundCheckBoxSize, 0f, collider2);
       
        for(int i = 0; i<hitCount; i++)
           

        if (hitCount >= 2) 
            isGrounded = true; //If there are two, its either player + one more object or 2 objects. Either case means there is ground
        else if (hitCount == 1)
            isGrounded = !collider2[0].transform.Equals(gameObject.transform); //If the collided transform does not belong to the player, then we are touching a ground
        else
            isGrounded = false;

        return isGrounded;
    }

    /// <summary>
    /// Checks for ground, and applies a value of <c>gravity</c> if the character is not grounded
    /// </summary>
    private void ApplyGravity()
    {
        realSpeed.y += gravity * Time.deltaTime;
        worldSpeed.y += gravity * Time.deltaTime;
    }

    /// <summary>
    /// Simple MoveTo method
    /// Changes the position of character by the given speed.
    /// </summary>
    /// <example>
    /// If character is in (5,2) and the speed is (2,0), character will be moved to position (7,2) 
    /// </example>
    /// <param name="speed"></param>
    /// <returns></returns>
    public bool MoveTo(Vector3 speed) {
        gameObject.transform.Translate(speed * Time.deltaTime);
        return true;
    }
}