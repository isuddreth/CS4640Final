using UnityEngine;
using System.Collections;

public class TPCharController : MonoBehaviour
{
    public bool isControllable = true;
 
    public bool canJump = true;
    public float jumpHeight = 1.5f;

    public float walkingSpeed = 6.0f;
    public float speedSmoothing = 10.0f;
    public float sidewayWalkMultiplier = 0.5f;

    public float gravity = 20.0f;
 
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 faceDirection = Vector3.zero;
    private float moveSpeed = 0.0f;
    private float verticalSpeed = 0.0f;
 
    private bool jumping = false;
    private float lastJumpButtonTime = -10.0f;
 
    private float jumpTimeout = 0.15f;
    
    private bool jumpingReachedApex = false;

    private Vector2 axisInputValue;
    private bool jumpInputValue;
    private bool crouchInputValue;
    private bool runInputValue;
   
 
    private CollisionFlags collisionFlags;
 
    private int jumpButtonPressedTwice = 0;
	
	void Start ()
    {
	    moveDirection = transform.TransformDirection(Vector3.forward);

        axisInputValue.x = 0.0f;
        axisInputValue.y = 0.0f;
        jumpInputValue = false;
        crouchInputValue = false;
        runInputValue = false;
	}
	
	
	void Update ()
    {
        if (isControllable)
        {
            axisInputValue.x = Input.GetAxis("Horizontal");
            axisInputValue.y = Input.GetAxis("Vertical");
            jumpInputValue = Input.GetButtonDown("Jump");
        }
        else
        {
            axisInputValue.x = 0f;
            axisInputValue.y = 0f;
        }
 
        if (jumpInputValue)
        {
            if(jumpButtonPressedTwice==-2)
                jumpButtonPressedTwice=1;
            else
                jumpButtonPressedTwice=-1;
            lastJumpButtonTime = Time.time;
        }
       
        UpdateSmoothedMovementDirection();
 
        ApplyGravity ();
        ApplyJumping();
       
        var movement = moveDirection * moveSpeed + new Vector3(0, verticalSpeed, 0);// + inAirVelocity;
        movement *= Time.deltaTime;
       
        CharacterController controller = GetComponent<CharacterController>();
        collisionFlags = controller.Move(movement);
       
        if (faceDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(faceDirection);
       
        if (IsGrounded())
        {
            if (jumping)
            {
                jumping = false;
                jumpButtonPressedTwice=0;
                SendMessage("DidLand", SendMessageOptions.DontRequireReceiver);
            }
        }
	}


    void UpdateSmoothedMovementDirection()
    {
        Transform cameraTransform;

        if (Camera.main != null)
            cameraTransform = Camera.main.transform;
        else
            cameraTransform = transform;

        bool grounded = IsGrounded();
       
        Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;
 
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        
        float horizontal = axisInputValue.x;
        float vertical = axisInputValue.y;
      
        Vector3 targetDir = forward * vertical + right * horizontal * sidewayWalkMultiplier;

        //temp
        if (targetDir != Vector3.zero)
            faceDirection = Vector3.Slerp(faceDirection, targetDir,Time.deltaTime*8);

        if (grounded)
        {
            if (faceDirection != Vector3.zero)
            {
                moveDirection = targetDir.normalized;
            }
               
            float curSmooth = speedSmoothing * Time.deltaTime;
            float targetSpeed = Mathf.Min(targetDir.magnitude, 1.0f);

            targetSpeed *= walkingSpeed;
               
            moveSpeed = Mathf.Lerp(moveSpeed, targetSpeed, curSmooth);
        }
    }


    void ApplyGravity()
    {
        if (isControllable)
        {              
            if (jumping && !jumpingReachedApex && verticalSpeed <= 0.0)
            {
                    jumpingReachedApex = true;
                    SendMessage("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
            }
               
            if (IsGrounded ())
                    verticalSpeed = 0.0f;
            else
                    verticalSpeed -= gravity * Time.deltaTime;
        }
    }


    void ApplyJumping()
    {
        if (IsGrounded())
        {
            if (canJump && Time.time < lastJumpButtonTime + jumpTimeout)
            {
                verticalSpeed = CalculateJumpVerticalSpeed (jumpHeight);
                SendMessage("DidJump", SendMessageOptions.DontRequireReceiver);
            }
        }
    }


    void DidJump()
    {
        jumping = true;
        jumpingReachedApex = false;
        lastJumpButtonTime = -10;
    }


    float CalculateJumpVerticalSpeed(float targetJumpHeight)
    {
        return Mathf.Sqrt(2f * targetJumpHeight * gravity);
    }


    void HidePlayer()
    {
        GameObject.Find("rootJoint").GetComponent<SkinnedMeshRenderer>().enabled = false;
        isControllable = false;
    }


    void ShowPlayer()
    {
        GameObject.Find("rootJoint").GetComponent<SkinnedMeshRenderer>().enabled = true;
        isControllable = true;
    }


    public bool IsMoving()
    {
        //return (Mathf.Abs(axisInputValue.x) + Mathf.Abs(axisInputValue.x)) > 0.5;
        return (moveSpeed > 1f);
    }


    bool IsGrounded()
    {
        return (collisionFlags & CollisionFlags.CollidedBelow) != 0;
    }


    public void StopMoving()
    {
        moveSpeed = 0.25f;
    }
}
