using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PlayerManager
{
    [SerializeField] private float movementSpeed;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.A)) movementSpeed = 2;
        else if (Input.GetKey(KeyCode.LeftControl)) movementSpeed = 0.5f;
        else movementSpeed = 1;
    }
    private void FixedUpdate()
    {
        player.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * movementSpeed);
    }
}
