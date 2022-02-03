using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public GameObject player;
    public Transform respawnPoint;
    
    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.6f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public bool isDead = false;
    
    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            Respawn();
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Respawn()
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position =respawnPoint.position;
        transform.rotation =respawnPoint.rotation;
        GetComponent<CharacterController>().enabled = true;
        isDead = false;
    }
}
