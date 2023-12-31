using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class ControllReceivingSystem : MonoBehaviour
{
    [SerializeField]
    public CharacterControlSystem curCharacterControl;
    // Start is called before the first frame update
    public CharacterController characterController;

    public UnityEvent<CharacterControlSystem> onCurCharacterChange;


    //Gia tri
    private float turnSmoothtime = 0.04f;
    [SerializeField]
    private float turnSmoothVelocity = 0.0f;
    [SerializeField]
    private bool lockControl = false;
    private float gravity = 18f;
    [SerializeField]
    private float _directionY = 0.0f;
    private float jumpSpeed = 7f;
    [SerializeField]
    private bool forwardWhenJump = false;
    [SerializeField]
    private float forceForwardWhenJump = 0f;
    private float dirForwardJump = 0f;

    private float DirmoveInputForWindForce;
    private float forceFallFoward = 0f;
    private bool ForwardWindForce = false;

    //Cho CameraCheck
    public bool isShoot = false;
    //SetCamera when BehindTheWall
    public bool isBehindTheWall = false;
    public bool isBehindTheWallRote = false;
    public float dirLookWhenBehindTheWall;
    public Vector3 dirRotateWhenBehindTheWall;
    public bool isDash = false;

    public float distanceCheck;
    public Vector3 boxCheck;
    public LayerMask layerCheck;

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
        if (forwardWhenJump && characterController.isGrounded == false) //co che move khi jump
        {
            Vector3 moveDir = Quaternion.Euler(0f, dirForwardJump, 0f) * Vector3.forward;
            characterController.Move(moveDir * forceForwardWhenJump * Time.deltaTime);
            forceForwardWhenJump -= Time.deltaTime * 0.5f;
        }
        if (forwardWhenJump == false && !CheckGrounded())
        {
            Vector3 moveDir = Quaternion.Euler(0f, DirmoveInputForWindForce, 0f) * Vector3.forward;
            characterController.Move(moveDir * forceFallFoward * Time.deltaTime);
        }
        if (characterController.isGrounded)
        {
            forwardWhenJump = false;
            forceForwardWhenJump = 0f;
            _directionY = -1f;
        }
        if (isBehindTheWallRote)
        {
            float tmp = transform.rotation.eulerAngles.y;
            tmp = Mathf.Lerp(tmp, dirLookWhenBehindTheWall, 0.1f);
            transform.rotation = Quaternion.Euler(0f, tmp, 0f);
            Vector3 tmpV3 = transform.position;
            tmpV3 = Vector3.Lerp(tmpV3, dirRotateWhenBehindTheWall, 0.5f);
            tmpV3.y = transform.position.y;
            transform.position = tmpV3;
            if (isBetween(transform.rotation.y, dirLookWhenBehindTheWall, 0.1f) && isBetweenV3(transform.position, dirRotateWhenBehindTheWall))
            {
                isBehindTheWallRote = false;
            }
        }
    }
    //Cac Method support cho chinh no///////////////////////////////////////////////////////////////////////////
    private void ReLoadCurCharacter()
    {
        Transform team;
        team = transform.GetChild(1).transform;
        foreach (Transform obj in team.transform)
        {
            if (obj.gameObject.activeSelf == true)
            {
                curCharacterControl = obj.gameObject.GetComponent<CharacterControlSystem>();
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
        float targetAngle = Mathf.Atan2(context.ReadValue<Vector2>().x, context.ReadValue<Vector2>().y) * Mathf.Rad2Deg;
        DirmoveInputForWindForce = targetAngle + Camera.main.transform.eulerAngles.y;
        ForwardWindForce = true;
        forceFallFoward = 3f;
    }
    public void cancleMovement()
    {
        curCharacterControl.cancleMovement();
        ForwardWindForce = false;
        forceFallFoward = 0f;
    }
    public void Atk(InputAction.CallbackContext context)
    {
        curCharacterControl.UseAttack(context);

    }
    public void cancleAtk(InputAction.CallbackContext context)
    {
        curCharacterControl.cancleAttack(context);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        curCharacterControl.UseJump(context);
    }
    public void cancleJump()
    {

    }

    public void Dash(InputAction.CallbackContext context)
    {
        curCharacterControl.UseDash(context);
    }
    public void cancleDash(InputAction.CallbackContext context)
    {
        curCharacterControl.cancleDash(context);
    }
    public void ActionC(InputAction.CallbackContext context)
    {
        curCharacterControl.ActionC(context);
    }
    public void cancleC(InputAction.CallbackContext context)
    {
        curCharacterControl.cancleC(context);
    }
    public UnityEvent<float> onBehindTheWallCalled;
    public void BehindTheWall(Vector3 SitPosition, float dirLookAt)
    {
        onBehindTheWallCalled.Invoke(dirLookAt);
        curCharacterControl.BehindTheWall(SitPosition, dirLookAt);
        //characterController.enabled = false;
        //transform.position = SitPosition;
        //transform.rotation = Quaternion.Euler(0f, dirLookAt, 0f);
        //characterController.enabled = true;
        isBehindTheWall = true;
        isBehindTheWallRote = true;
        dirLookWhenBehindTheWall = dirLookAt;
        dirRotateWhenBehindTheWall = SitPosition;
    }


    // Cac Method Child call//////////////////////////////////////////////////////////////////////////////////////
    public void RotatePlayer(float targetAngle) //goc xoay theo truc Y
    {
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
    public void setRotatePlayer(float targetAngle)
    {
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
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
    public void Jump(float dir, float force)
    {
        _directionY = jumpSpeed;
        if (dir == 0f) { return; }
        forwardWhenJump = true;
        forceForwardWhenJump = force;
        dirForwardJump = dir;
    }
    public void AddWindForce(float force)
    {
        _directionY += force;
        forceForwardWhenJump = 3f;
        forwardWhenJump = ForwardWindForce;
        dirForwardJump = DirmoveInputForWindForce;
    }
    public void teleport(Vector3 point)
    {
        characterController.enabled = false;
        transform.position = point;
        characterController.enabled = true;
    }

    public bool CheckGrounded()
    {
        if (Physics.BoxCast(transform.position, boxCheck, -transform.up, transform.rotation, distanceCheck, layerCheck)) return true;
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, boxCheck);
    }
    public void ChangeRunShoot(bool tmp)
    {
        isShoot = tmp;
    }
    public UnityEvent cancleBehindTheWall;
    public void cancleBehindTheWallChild()
    {
        cancleBehindTheWall.Invoke();
        isBehindTheWall = false;
        isBehindTheWallRote = false;
    }

    public void ResetTelePort()
    {
        curCharacterControl.ResetTele();
        forwardWhenJump = false;
    }
    private bool isBetween(float x, float target, float diference)
    {
        return ((x > target - diference) && (x < target + diference));
    }
    private bool isBetweenV3(Vector3 x, Vector3 target)
    {
        if (x.x > target.x + 0.1f || x.x < target.x - 0.1f) return false;
        if (x.z > target.z + 0.1f || x.z < target.z - 0.1f) return false;
        return true;
    }
}
