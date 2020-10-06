using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    // For Player Variables

    public float currentMoveSpeed;
    public float moveSpeed;
    public Vector3 velocity;
    public float currentWalkAnimationSpeed;
    public float walkAnimationSpeed;
    public float rotationSpeed;
    public float jumpForce;
    public float runSpeed;
    public float runAnimationSpeed;

    // For Player Component

    private Rigidbody rb_Player;
    private Animator an_Player;

    // For Other GameObjects

    public GameObject cameraObj;
    public GameObject playerMesh;


    // Awake is called before the first frame update
    void Awake()
    {

        rb_Player = GetComponent<Rigidbody>();
        an_Player = GetComponent<Animator>();

    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {


        // Start Mobile Input

        // For Move

        MovePlayer(CrossPlatformInputManager.GetAxis("Vertical"), CrossPlatformInputManager.GetAxis("Horizontal"));

        // For Attack

        if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            Attack();
        }

        // End Mobile Input


        // check if player in the ground and if player in the ground will enable landing to move from fall animation to land animation

        // draw line from player transform to ground transform
        // use line cast to check if player in the ground .. the line cast will return true if player in the ground

        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -0.01f, 0), Color.blue);

        if (Physics.Linecast(transform.position, transform.position + new Vector3(0, -0.01f, 0)))
        {
            // trigger landing
            an_Player.SetTrigger("Landing");
        }



        // check if top arrow and bottom arrow or left arrow and right arrow are pressed .. if pressed by keyboard move Player

        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {

            // Move Player
            MovePlayer(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        }

        // Click Space to Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // Left Click in the mouse to Attack

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        // Press Left Shift to Run

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            RunDown();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunUp();
        }
        
    }


    // Move Function For Move Player

    public void MovePlayer(float forward, float right)
    {

        // forward parameter mean virtical axis from keyboard , right parameter mean horizontal axis from keyboard
        // Get Vertical axis and horizontal axis and multiple to camera vertical axis and horizontal axis

        Vector3 translation;

        translation = forward * cameraObj.transform.forward;
        translation += right * cameraObj.transform.right;
        translation.y = 0;


        // check if vertical and horizontal pressed

        if (translation.magnitude > 0.2f)
        {
            // set velocity to equal to translation
            velocity = translation;
        } else
        {
            // set velocity to zero
            velocity = Vector3.zero;
        }


        // Move Player By Rigidbody Velocity

        rb_Player.velocity = new Vector3(velocity.normalized.x * moveSpeed, rb_Player.velocity.y, velocity.normalized.z * moveSpeed);

        // Rotate Player

        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.Lerp(playerMesh.transform.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);
        }

        // Move Animation

        an_Player.SetFloat("Velocity", velocity.magnitude * walkAnimationSpeed);

    }


    // Start Jump Function

    public void Jump()
    {
        // jump by rigidbody addforce
        rb_Player.AddForce(Vector3.up * jumpForce);

        // Play Animation
        an_Player.SetTrigger("Jump");

    }

    // End Jump Funcion

    // Start Attack Function

    public void Attack()
    {

        float randomNumber = Random.Range(0, 15);

        if (randomNumber >= 0 && randomNumber <= 5)
        {

            an_Player.SetTrigger("Attack1");

        } else if (randomNumber > 5 && randomNumber <= 10)
        {

            an_Player.SetTrigger("Attack2");

        } else if (randomNumber > 10 && randomNumber <= 15)
        {

            an_Player.SetTrigger("Attack3");

        }

        
    }

    // End Attack Function

    // Start Run Function

    public void RunDown()
    {
        moveSpeed = runSpeed;
        walkAnimationSpeed = runAnimationSpeed;
    }

    public void RunUp()
    {
        moveSpeed = currentMoveSpeed;
        walkAnimationSpeed = currentWalkAnimationSpeed;
    }

    // End Run Function


    // Start Death Function

    public void Death()
    {
        an_Player.SetTrigger("Death");
    }

    // End Death Function



}
