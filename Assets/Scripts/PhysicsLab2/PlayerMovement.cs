using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerManager
{
    [SerializeField] private float speedMultiplier, initialSpeed = 5, jumpForce;
    private bool onAir = false;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.A)) speedMultiplier = 2;
        else if (Input.GetKey(KeyCode.LeftControl)) speedMultiplier = 0.5f;
        else speedMultiplier = 1;

        movementSpeed = initialSpeed * speedMultiplier;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }

        if (isJumping)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
            Debug.Log("isJumping...");
        }
    }
    private void FixedUpdate()
    {
        if (!onAir)
        {
            playerRb.velocity = (new Vector3(Input.GetAxis("Horizontal") * movementSpeed, playerRb.velocity.y, Input.GetAxis("Vertical") * movementSpeed));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            onAir = false;
        }
        else onAir = true;
    }
    private void OnCollisionStay(Collision collision)
    {

    }
}
