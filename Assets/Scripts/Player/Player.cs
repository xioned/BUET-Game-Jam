using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool disablePlayerInput;
    public bool interacting = false;
    public bool groundCollid = false;

    [Header("PlayerMovement")]
    [SerializeField] private float playerWalkSpeed;
    [SerializeField] private float playerRunSpeed;
    [SerializeField] private float playerRotationSpeed;
    Camera mainCamera;
    Vector3 playerMoveDirection;
    Vector3 playerMoveAxis;

    [Header("PlayerJump")]
    public bool playerIsGrounded = true;
    [SerializeField] private float playerJumpForce;
    [SerializeField] private float playerFallVelocity;
    [SerializeField] private float playerLeapingVelocity;
    [SerializeField] private float playerJumpAirTime;
    [SerializeField] private float groundedraycastHightOffset = 0.12f;
    [SerializeField] private LayerMask groundLayerMask;
    Rigidbody playerRigidbody;
    float inAirTimer;
    [SerializeField] PlayerGrounded granoundCollid;


    #region private
    Animator animator;
    #endregion private

    #region AnimationHash
    private int motionHash;
    private int speedHash;
    #endregion AnimationHash

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody>();
        //Animation
        animator = GetComponent<Animator>();
        motionHash = Animator.StringToHash("Motion");
        speedHash = Animator.StringToHash("Speed");
    }

    private void Update()
    {
        PlayerFall();
        if (interacting) { return; }
        PlayerJump();
        playerMoveAxis = new Vector3(Input.GetAxisRaw("Vertical"), 0f, Input.GetAxisRaw("Horizontal"));
    }
    private void LateUpdate()
    {
        interacting = animator.GetBool("Interacting");
    }
    private void PlayerJump()
    {
        //
    }


    private void FixedUpdate()
    {
        if (interacting) { return; }
        PlayerRotate();
        PlayerMoveByRigidBody();
    }
    private void PlayerFall()
    {
        if (granoundCollid.collid)
        {
            animator.SetBool("Fall", false);
            if (!playerIsGrounded && !interacting)
            {
                animator.SetBool("Land", true);
            }
            playerIsGrounded = true;
            inAirTimer = 0;

        }

        if (!granoundCollid.collid)
        {
            playerIsGrounded = false;
        }

        if (!playerIsGrounded)
        {
            playerRigidbody.velocity = Vector3.zero;
            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * playerLeapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * playerFallVelocity * inAirTimer);
            animator.SetBool("Fall", true);
            interacting = true;
        }
    }

    
    

    private void PlayerMoveByRigidBody()
    {
        playerMoveDirection = playerMoveAxis.x * mainCamera.transform.forward;
        playerMoveDirection = playerMoveDirection + mainCamera.transform.right * playerMoveAxis.z;
        playerMoveDirection.Normalize();
        playerMoveDirection.y = 0;
        playerRigidbody.velocity = playerMoveDirection * playerWalkSpeed;

        if (playerMoveAxis.x != 0 || playerMoveAxis.z != 0)
        {
            animator.SetFloat(motionHash, 1);
            animator.SetFloat(speedHash, playerRunSpeed);
        }
        if (playerMoveAxis.x == 0 && playerMoveAxis.z == 0)
        {
            animator.SetFloat(motionHash, 0);
            animator.SetFloat(speedHash, 0);
        }
    }

    private void PlayerRotate()
    {
        Vector3 playerTagertDirection = Vector3.zero;

        playerTagertDirection = playerMoveAxis.x * mainCamera.transform.forward;
        playerTagertDirection = playerMoveDirection + mainCamera.transform.right * playerMoveAxis.z;
        playerTagertDirection.Normalize();
        playerTagertDirection.y = 0;
        if (playerTagertDirection == Vector3.zero)
        {
            playerTagertDirection = transform.forward;
        }
        Quaternion targetRotation = Quaternion.LookRotation(playerTagertDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    private void PlayFootStepShound()
    {

    }
    private void PlayLandShound()
    {

    }
}
