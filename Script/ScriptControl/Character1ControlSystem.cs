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


    // Animator Paremeter
    [SerializeField]
    private Animator animator;
    //

    [SerializeField]
    private bool isRun = false;
    [SerializeField] int currentAttack = 1;

    //Gia tri
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float targetAngle = 0f;
    [SerializeField] float lastClicked = 0f;
    [SerializeField] float lastComboTime = 0f;
    [SerializeField] float maxTimeResetCombo = 3f;
    [SerializeField] float delayAni = 0.75f;
    [SerializeField] float delayCombo = 2f;

    private void Awake()
    {
        controllReceivingSystem = transform.parent.GetComponentInParent<ControllReceivingSystem>();
        animator = transform.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckAtk();
        CheckDashSwap();
        if (isRun && CanRun())
        {
            startMoveMent(targetAngle);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            tmpShift();
        }
        if (Time.time - lastClicked > maxTimeResetCombo)
        {
            ResetCombo();
        }
    }

    ////// ke thua tu cai CharracterControllSystem///////
    public override void UseMovement(InputAction.CallbackContext ctx) 
    {
        base.UseMovement(ctx);
        isRun = true;
        Vector2 JoyMoveValue = ctx.ReadValue<Vector2>();
        Vector3 direction = new Vector3(JoyMoveValue.x, 0f, JoyMoveValue.y); // tao huong di chuyen tu joystick
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg ; //tao goc di chuyen theo truc Y
    }
    public override void cancleMovement()
    {
        base.cancleMovement();
        isRun = false;
        animator.SetBool("isRun", false);
    }

    public override void UseJump(InputAction.CallbackContext ctx) { }
    public override void UseAttack(InputAction.CallbackContext ctx) 
    {
        base.UseAttack(ctx);
        if (CanAtk())
            Atk();
    }
    public override void UseNormalSkill(InputAction.CallbackContext ctx) { }
    public override void UseBurstSkill(InputAction.CallbackContext ctx) { }

    //////////// Ham cua chinh no/////
    private bool CanRun()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8f)  return false;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("dash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f) return false;
        return true;
    }
    public void startMoveMent(float targetAngle)
    {
        if(controllReceivingSystem.IsLockControl()) { return; }
        animator.SetBool("isRun", true);
        controllReceivingSystem.RotatePlayer(targetAngle + Camera.main.transform.eulerAngles.y);
        controllReceivingSystem.MovePlayer(targetAngle + Camera.main.transform.eulerAngles.y, moveSpeed);
    }

    private void tmpShift()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("dash")) { return; }
        animator.Play("dash");
    }
    private bool CanAtk()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime< delayAni) return false;
        if (Time.time - lastComboTime < delayCombo) return false;
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("dash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f) return false;
        return true;
    }
    private void Atk()
    {
        animator.Play("Atk" + currentAttack);
        animator.SetBool("isRun", false);
        if (currentAttack >= 3)
        {
            EndCombo();
        }
        else currentAttack++;
        lastClicked = Time.time;
    }
    private void ResetCombo()
    {
        currentAttack = 1;
        lastClicked = Time.time;
    }
    private void EndCombo()
    {
        currentAttack = 1;
        lastClicked = Time.time;
        lastComboTime = Time.time;
    }

    private void CheckDashSwap()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("dash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6)
        {
            animator.SetBool("dashSwap",true);
        }
        else
        {
            animator.SetBool("dashSwap", false);
        }
    }
    private void CheckAtk()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.8)
        {
            animator.SetBool("isAtk", true);
        }
        else
        {
            animator.SetBool("isAtk", false);
        }
    }

    private void OnAnimatorMove()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("dash")) 
        {
            transform.parent.parent.position += animator.deltaPosition * 2f;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK"))
        {
            transform.parent.parent.position += animator.deltaPosition;
        }
    }
}
