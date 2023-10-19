using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour
{
    //public movement variables 
    public float MaxMoveSpeed = 5;
    public float MoveAcceleration = 10;
    public float JumpSpeed = 5;
    public float JumpMaxTime = 0.5f;
    public bool canControl = true; 

    //public camera object 
    public Camera PlayerCamera;

    //public block animations/objects/variables 
    //add block anim here
    public Slider BlockSlider;
    public Image BlockFill; 
    public float BlockStamina, MaxBlockStamina;

    //private attack animations/variables 
    [SerializeField] private SwordAnimation SwordAnimation;
    [SerializeField] private bool Attack1;
    [SerializeField] private bool Attack2;
    [SerializeField] private bool Attack3;
    private float currentAttackCombo = 0;
    private float maxAttackCombo = 3;

    //private objects/variables 
    private float JumpTimer = 0; 
    private CharacterController characterController;
    private bool jumpInputPressed = false;
    private bool isJumping = false;
    private bool blockPressed; 
    private Vector2 moveInput = Vector2.zero;
    private Vector2 currentHorizontalVelocity = Vector2.zero; 
    private float currentVerticalVelocity = 0;
    private bool recharging = false;
    private Coroutine recharge; 

    private void Awake()
    {
        //calls character controller object on object awake to recieve player inputs
        characterController = GetComponent<CharacterController>();

        //locks cursor on obj awake 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (canControl)
        {
            //camera setup in class 
            Vector3 cameraSpaceMovement = new Vector3(moveInput.x, 0, moveInput.y);
            cameraSpaceMovement = PlayerCamera.transform.TransformDirection(cameraSpaceMovement);
            Vector2 cameraHorizontalMovmement = new Vector2(cameraSpaceMovement.x, cameraSpaceMovement.z);
            currentHorizontalVelocity = Vector2.Lerp(currentHorizontalVelocity, cameraHorizontalMovmement * MaxMoveSpeed, Time.deltaTime * MoveAcceleration);

            //jumping setup in class 
            if (isJumping == false)
            {
                currentVerticalVelocity += Physics.gravity.y * Time.deltaTime;

                if (characterController.isGrounded && currentVerticalVelocity < 0)
                {
                    currentVerticalVelocity = Physics.gravity.y * Time.deltaTime;
                }
            }
            else
            {
                JumpTimer += Time.deltaTime;

                if (JumpTimer >= JumpMaxTime)
                {
                    isJumping = false;
                }
            }

            //Setup look rotation for player based on camera direction compared to player input in class
            Vector3 currentVelocity = new Vector3(currentHorizontalVelocity.x, currentVerticalVelocity, currentHorizontalVelocity.y);

            Vector3 horizontalDirection = Vector3.Scale(currentVelocity, new Vector3(1, 0, 1));
            
            if (horizontalDirection.magnitude > 0.0001)
            {
                Quaternion newDirection = Quaternion.LookRotation(horizontalDirection.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, newDirection, Time.deltaTime * MoveAcceleration);
            }

            characterController.Move(currentVelocity * Time.deltaTime);

            //if player presses block button, decrease block stamina every frame time scaled by time.deltatime, if less than 0, set to 0
            if (blockPressed)
            {
                BlockStamina -= Time.deltaTime;

               if (BlockStamina < 0)
               {
                 BlockStamina = 0;
               }
               //sets mana bar/slider value equal to the current block stamina value 
                BlockSlider.value = BlockStamina;
            }

            //increase block stamina every frame time scaled by time.deltatime 
            if (recharging)
            {
                //once the recharging bool is set to true within the coroutine, block stamina bar increases time scaled by time.delta time every frame 
                BlockStamina += Time.deltaTime;

                //if the current block stamina is greater than the max at any point, set them equal and stop recharging 
                if (BlockStamina > MaxBlockStamina)
                {
                    BlockStamina = MaxBlockStamina;
                    recharging = false;
                }

                //sets the slider/mana bar value equal to current stamina 
                BlockSlider.value = BlockStamina;
            }
        }
        
    }

    //recieves action map move input values 
    public void OnMove(InputValue value)
    {
        //if moving, sets sword animation to walking 
        SwordAnimation.WalkAnim(true); 
        
        //sets the move input value  
        moveInput = value.Get<Vector2>();

        if (moveInput == new Vector2(0, 0) ) 
        {
            //sets aniomation back to idle if not moving 
            SwordAnimation.WalkAnim(false); 
        }
    }

    //recieves actuion map jump input values
    public void OnJump(InputValue value)
    {
        //sets the jump input value 
        jumpInputPressed = value.Get<float>() > 0; 

        //if the player is grounded and jumps, they are jumping, but the player cannot jump while jumping/in mid air
        if(jumpInputPressed)
        {
            if (characterController.isGrounded)
            {
                isJumping = true;
                JumpTimer = 0; 
                currentVerticalVelocity = JumpSpeed;
            }
        }
        else
        {
            if (isJumping)
            {
                isJumping = false;
            }
        }
    }

    //gets action map input for attacking, calls the sword animation attack trigger function
    public void OnAttack(InputValue value)
    {
        SwordAnimation.TriggerAttackCombo();
    }

    //recieves the action map block input 
    public void OnBlock(InputValue value)
    {
        //sets the bool of is pressed to true  
        blockPressed = value.isPressed; 

        //if the button is released the recharge coroutine begins 
        if (!blockPressed)
        {
            StartCoroutine(RechargeStamina());
        } 
    }

    //coroutine to recharge block mana 
    private IEnumerator RechargeStamina()
    {
        //waits one second before recharging 
        yield return new WaitForSeconds(1f);

        //sets recharging bool to true 
        recharging = true;
    }

}