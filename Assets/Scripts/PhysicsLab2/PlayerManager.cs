using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    protected GameObject player;
    protected Rigidbody playerRb;

    [Header("Motion")]
    [SerializeField] protected bool isJumping = false;
    protected float movementSpeed { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerRb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
