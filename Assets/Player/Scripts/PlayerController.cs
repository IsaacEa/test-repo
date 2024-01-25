using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float baseAcceleration = 10 / 3f; //m/s*s  
    [SerializeField] float maxHorizontalVelocity = 10f; //m/s 
    [SerializeField] float minHorizontalVelocity = 3f; //m/s 
    [SerializeField] float horizontalVelocity = 0f; //m/s 
    [SerializeField] float verticalVelocity = 0f; // m/s
    [SerializeField] float punchVelocity = 1f; // m/s
    [SerializeField] float gravity = 9.81f;            //m/s*s
    [SerializeField] float jumpSpeed = 10f;

    [SerializeField] InputActionReference jump;
    [SerializeField] InputActionReference punch;
    [SerializeField] InputActionReference smash;
    [SerializeField] InputActionReference uppercut;


    CharacterController characterController;

    [SerializeField] GameObject punchObject;
    [SerializeField] GameObject uppercutObject;
    [SerializeField] GameObject smashObject;



    private void Start()
    {
        
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        jump.action.Enable();
        punch.action.Enable();
        smash.action.Enable();
        uppercut.action.Enable();
    }



    private void Update()
    {
        //Horizontal Acceleration

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

        //Jump
        if(characterController.isGrounded && 
            jump.action.WasPerformedThisFrame())
        {
            Debug.Log("prueba");
            verticalVelocity = jumpSpeed;
        }

        //Movement
        characterController.Move(
            Vector3.forward * horizontalVelocity * Time.deltaTime +
            Vector3.up * verticalVelocity * Time.deltaTime);

        if(characterController.isGrounded)
        {
            verticalVelocity = 0f;
        }

        if(punch.action.WasPerformedThisFrame())
        {
            StartCoroutine(attackTime(punchObject));
            horizontalVelocity = horizontalVelocity * 1.2f;
            Debug.Log("puñetazo");
        }
        if(smash.action.WasPerformedThisFrame() && characterController.isGrounded == false)
        {
            StartCoroutine(attackTime(smashObject));
            decreaseVelocity(smashObject);
            Debug.Log("smash");
        }
        if(uppercut.action.WasPerformedThisFrame() && characterController.isGrounded)
        {
            StartCoroutine(attackTime(uppercutObject));
            decreaseVelocity(uppercutObject);
            Debug.Log("uppercut");
        }

        IEnumerator attackTime(GameObject attackObject)
        {
            attackObject.SetActive(true);
            if(attackObject == uppercutObject || attackObject == smashObject) { horizontalVelocity = horizontalVelocity / 2; }
            yield return new WaitForSeconds(0.5f);
            attackObject.SetActive(false);
        }
        

        
    }
    void decreaseVelocity(GameObject velObject)
    {
       if(velObject == uppercutObject) 
       {
           Debug.Log("Salto con Uppercut");
           verticalVelocity = jumpSpeed;
       }
       else if(velObject == smashObject)
       {
            Debug.Log("Abajo con Smash");
            verticalVelocity = -jumpSpeed;
        }
        
        
        
    }

}
