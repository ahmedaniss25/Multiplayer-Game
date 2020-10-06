using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PhotonPlayerController : MonoBehaviour
{

    // For Player Variables

    // For Player Component

    private PlayerController pController;

    // For Other Game Objects 



    // Awake is called before the first frame update
    void Awake()
    {
        pController = GetComponent<PlayerController>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {

        // Start Mobile Input

        // For Move

        pController.MovePlayer(CrossPlatformInputManager.GetAxis("Vertical"), CrossPlatformInputManager.GetAxis("Horizontal"));

        // For Attack

        if (CrossPlatformInputManager.GetButtonDown("Attack"))
        {
            pController.Attack();
        }

        // End Mobile Input


        // Start Keyboard Input

        // check if player in the ground and if player in the ground will enable landing to move from fall animation to land animation

        // draw line from player transform to ground transform
        // use line cast to check if player in the ground .. the line cast will return true if player in the ground

        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -0.01f, 0), Color.blue);

        if (Physics.Linecast(transform.position, transform.position + new Vector3(0, -0.01f, 0)))
        {
            // trigger landing
            pController.an_Player.SetTrigger("Landing");
        }



        // check if top arrow and bottom arrow or left arrow and right arrow are pressed .. if pressed by keyboard move Player

        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {

            // Move Player
            pController.MovePlayer(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));

        }

        // Click Space to Jump

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pController.Jump();
        }

        // Left Click in the mouse to Attack

        if (Input.GetMouseButtonDown(0))
        {
            pController.Attack();
        }

        // Press Left Shift to Run

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            pController.RunDown();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            pController.RunUp();
        }

        // End Keyboard Input

    }






}
