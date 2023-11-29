using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Cinemachine.AxisState;

public class ControllReceivingSystem : MonoBehaviour
{
    [SerializeField]
    public CharacterControlSystem curCharacterControl; 
    // Start is called before the first frame update
    public CharacterController characterController;

    public UnityEvent<CharacterControlSystem> onCurCharacterChange;
    

    //Gia tri
    [SerializeField]
    private float turnSmoothtime = 0.1f;
    [SerializeField]
    private float turnSmoothVelocity = 0.0f;
    [SerializeField]
    private bool lockControl = false;
    [SerializeField]
    private float gravity = 9.81f;
    [SerializeField]
    private float _directionY = 0.0f;
    [SerializeField]
    private float jumpSpeed = 5f;
    [SerializeField]
    private bool forwardWhenJump = false;
    [SerializeField]
    private float forceForwardWhenJump = 0f;
    private float dirForwardJump = 0f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        ReLoadCurCharacter();
    }

    private void OnEnable()
    {
       

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gravity
        Vector3 jumpdirection = new Vector3(0f, 0f, 0f);
        if (_directionY > -gravity)
        {
            _directionY -= gravity * Time.deltaTime;
        }
        jumpdirection.y = _directionY;
        characterController.Move(jumpdirection * Time.deltaTime);
        if(forwardWhenJump)
        {
            Vector3 moveDir = Quaternion.Euler(0f, dirForwardJump, 0f) * Vector3.forward;
            characterController.Move(moveDir * forceForwardWhenJump * Time.deltaTime);
            forceForwardWhenJump-=Time.deltaTime*0.5f;
        }
        if(characterController.isGrounded)
        {
            forwardWhenJump = false;
            forceForwardWhenJump = 0f;
        }
    }
    //Cac Method support cho chinh no///////////////////////////////////////////////////////////////////////////
    private void ReLoadCurCharacter()
    {
        Transform team;
        team = transform.GetChild(1).transform;
        foreach(Transform obj in team.transform)
        {
            if (obj.gameObject.activeSelf == true)
            {
                curCharacterControl=obj.gameObject.GetComponent<CharacterControlSystem>();
                onCurCharacterChange.Invoke(curCharacterControl);
                break;
            }
        }
    }
    public void LockControl(bool lockc)
    {
        lockControl = lockc;
    }
    public bool IsLockControl()
    {
        return lockControl;
    }


    //Cac method PlayerControler goi////////////////////////////////////////////////////////////////////////////
    public void MoveMent(InputAction.CallbackContext context)
    {
        curCharacterControl.UseMovement(context);
        
    }
    public void cancleMovement()
    {
        curCharacterControl.cancleMovement();
    }
    public void Atk(InputAction.CallbackContext context)
    {
        curCharacterControl.UseAttack(context);

    }
    public void cancleAtk()
    {
        
    }
    public void Jump(InputAction.CallbackContext context)
    {
        curCharacterControl.UseJump(context);
    }
    public void cancleJump()
    {

    }


    // Cac Method Child call//////////////////////////////////////////////////////////////////////////////////////
    public void RotatePlayer(float targetAngle) //goc xoay theo truc Y
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);       
    }
    public void MovePlayer(float targetAngle, float speedMove) //goc di chuyen theo truc Y
    {
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        characterController.Move(moveDir * speedMove * Time.deltaTime);
    }
    public void apllyRootMotion(Vector3 dir)
    {
        Vector3 dirFix = dir;
        dirFix.y = 0f;
        characterController.Move(dirFix * Time.deltaTime);
    }
    public void Jump(float dir)
    {
        _directionY = jumpSpeed;
        if(dir == 0f) { return; }
        forwardWhenJump = true;
        forceForwardWhenJump = 5f;
        dirForwardJump = dir;
    }
    public void teleport(Vector3 point)
    {
        characterController.enabled = false;
        transform.position = point;
        characterController.enabled = true;
    }
}
