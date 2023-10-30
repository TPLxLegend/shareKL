using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.AxisState;

public class ControllReceivingSystem : MonoBehaviour
{
    [SerializeField]
    private CharacterControlSystem curCharacterControl; 
    // Start is called before the first frame update
    [SerializeField]
    private CharacterController characterController;

    

    //Gia tri
    [SerializeField]
    private float turnSmoothtime = 0.1f;
    [SerializeField]
    private float turnSmoothVelocity = 0.0f;
    [SerializeField]
    private bool lockControl = false;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        ReLoadCurCharacter();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
