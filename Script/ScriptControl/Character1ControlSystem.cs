using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Cinemachine.AxisState;

public class Character1ControlSystem : CharacterControlSystem
{
    [SerializeField]
    ControllReceivingSystem controllReceivingSystem;
    // Start is called before the first frame update

    [SerializeField]
    private bool isRun = false;

    //Gia tri
    private float moveSpeed = 5f;
    private float targetAngle = 0f;
    private void Awake()
    {
        controllReceivingSystem = transform.parent.GetComponentInParent<ControllReceivingSystem>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isRun)
        {
            startMoveMent(targetAngle);
        }
    }

    ////// ke thua tu cai CharracterControllSystem///////
    public override void UseMovement(InputAction.CallbackContext ctx) 
    {
        base.UseMovement(ctx);
        isRun = true;
        Vector2 JoyMoveValue = ctx.ReadValue<Vector2>();
        Vector3 direction = new Vector3(JoyMoveValue.x, 0f, JoyMoveValue.y); // tao huong di chuyen tu joystick
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y; //tao goc di chuyen theo truc Y
    }
    public override void cancleMovement()
    {
        base.cancleMovement();
        isRun = false;
    }

    public override void UseJump(InputAction.CallbackContext ctx) { }
    public override void UseAttack(InputAction.CallbackContext ctx) { }
    public override void UseNormalSkill(InputAction.CallbackContext ctx) { }
    public override void UseBurstSkill(InputAction.CallbackContext ctx) { }

    //////////// Ham cua chinh no/////
    public void startMoveMent(float targetAngle)
    {

        controllReceivingSystem.RotatePlayer(targetAngle);
        controllReceivingSystem.MovePlayer(targetAngle, moveSpeed);
    }
}
