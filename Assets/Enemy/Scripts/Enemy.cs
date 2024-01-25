using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float baseAcceleration = 10f / 3f;     // m/s*s
    [SerializeField] float maxHorizontalVelocity = 4f;      // m/s
    [SerializeField] float gravity = -9.81f;                // m/s*s
    [SerializeField] float jumpSpeed = 5f;                  // m/s

    [Header("Attack")]
    [SerializeField] string playerTag;
    [SerializeField] HitCollider attackHitCollider;
    bool isTouchingPlayer;

    [Header("Death")]
    [SerializeField] HitCollider deathHitCollider;

    CharacterController characterController;
    float horizontalVelocity = 0f;      // m/s
    float verticalVelocity = 0f;        // m/s
    float minHorizontalVelocity = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateState();
        //UpdateMovement();
    }

    void UpdateMovement()
    {
        if (horizontalVelocity < minHorizontalVelocity)
        {
            horizontalVelocity +=
            baseAcceleration *  // m/s*s
            Time.deltaTime;     // s
        }

        //Vertical Acceleration
        verticalVelocity +=
            gravity *
            Time.deltaTime;

        /*Jump
        if (characterController.isGrounded &&
            jump.action.WasPerformedThisFrame())
        {
            Debug.Log("prueba");
            verticalVelocity = jumpSpeed;
        }*/

        //Movement
        characterController.Move(
            Vector3.forward * horizontalVelocity * Time.deltaTime +
            Vector3.up * verticalVelocity * Time.deltaTime);

        if (characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }
    }
}
