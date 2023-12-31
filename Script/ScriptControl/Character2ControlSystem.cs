using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class Character2ControlSystem : CharacterControlSystem
{
    [SerializeField]
    private ControllReceivingSystem controllReceivingSystem;
    
    public aimPoint aimPoint;



    //Paremeter Animator
    public float runStyle = 1f;
    public playerState playerstate;
    public ShootState shootState;
    public Vector2 dirShootRun;


    //Gia tri
    //private bool isRun = false;
    //private bool isFastRun = false;
    public RunState runState;
    public float moveSpeed = 5f;
    private float runShootMoveSpeed = 3f;
    private float runSpeed = 7f;
    private float fastRunSpeed = 15f;
    [SerializeField] private float targetAngle = 0f;
    [SerializeField] private float dirFowardJump = 0f;
    private float changeSpeed = 5f;
    public float targetRunspeed = 5f;
    public float targetRunStyle = 1f;
    public float curBullet = 0f;
    public float maxBullet = 50f;
    public int shootOnSecond = 10;
    private float lastTimeShoot = 0f;
    public LayerMask ignoreShoot;
    private float timeCancleFisrtShoot = 0.1f;

    private bool shootWhenSitDown = false;

    void Start()
    {
        animator = transform.GetComponent<Animator>();

        controllReceivingSystem = transform.parent.GetComponentInParent<ControllReceivingSystem>();
        ResetTele();
        aimPoint = GameObject.Find("AimCanvas/aimPoint").GetComponent<aimPoint>();
    }
    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGround", controllReceivingSystem.characterController.isGrounded);
        animator.SetBool("fall", !controllReceivingSystem.characterController.isGrounded);
        if ((runState == RunState.run || runState == RunState.fastRun) && CanRun())
        {
            startMoveMent(targetAngle);
        }
        ChangeFasrRun();
        if (shootState != ShootState.none && playerstate == playerState.normal)
            RunShoot(shootState);
        if (shootWhenSitDown && playerstate == playerState.BehindTheWall)
        {
            SitDownShoot();
        }
        //luu gia tri cho action Jump
        dirFowardJump = targetAngle + Camera.main.transform.eulerAngles.y;
    }
    public override void UseMovement(InputAction.CallbackContext ctx)
    {
        base.UseMovement(ctx);
        if (runState == RunState.none)
            runState = RunState.run;
        if (shootState == ShootState.fastRunShoots)
        {
            endShoot();
        }
        if (playerstate == playerState.BehindTheWall)
        {
            cancleBehindTheWall();
        }
        Vector2 tmpValue = ctx.ReadValue<Vector2>();
        dirShootRun.x = NomalizeVectorInAnimator(tmpValue.x);
        dirShootRun.y = NomalizeVectorInAnimator(tmpValue.y);
        Vector2 JoyMoveValue = ctx.ReadValue<Vector2>();
        Vector3 direction = new Vector3(JoyMoveValue.x, 0f, JoyMoveValue.y); // tao huong di chuyen tu joystick
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg; //tao goc di chuyen theo truc Y

    }
    public override void cancleMovement()
    {
        base.cancleMovement();
        runState = RunState.none;
        animator.SetBool("isRun", false);
        dirShootRun = new Vector2(0f, 0f);
    }
    public override void UseJump(InputAction.CallbackContext ctx)
    {
        base.UseJump(ctx);
        if (canJump())
            Jump();
    }
    public override void UseAttack(InputAction.CallbackContext ctx)
    {
        base.UseAttack(ctx);
        if (!CanATK()) return;
        if (playerstate == playerState.normal)
        {
            switch (runState)
            {
                case RunState.fastRun:
                    {
                        shootState = checkStateShootRun();
                        CallToCameraMan(true);
                        break;
                    }
                default:
                    {
                        shootState = ShootState.runShoot;
                        CallToCameraMan(true);
                        break;
                    }
            }
        }
        else
        {
            shootWhenSitDown = true;
        }

    }
    public override void cancleAttack(InputAction.CallbackContext ctx)
    {
        base.cancleAttack(ctx);
        endShoot();
    }
    public override void UseNormalSkill(InputAction.CallbackContext ctx) { }
    public override void UseBurstSkill(InputAction.CallbackContext ctx) { }
    public override void UseDash(InputAction.CallbackContext ctx)
    {
        if (!CanDash()) return;
        targetRunspeed = fastRunSpeed;
        targetRunStyle = 2f;
        runState = RunState.fastRun;
    }
    public override void cancleDash(InputAction.CallbackContext ctx)
    {
        targetRunspeed = runSpeed;
        targetRunStyle = 1f;
        if (runState == RunState.fastRun)
            runState = RunState.run;
    }
    public override void ActionC(InputAction.CallbackContext ctx)
    {
        if (CanActionC())
        {
            playAni = "Dash";
            
            controllReceivingSystem.isDash = true;
        }
    }
    public override void cancleC(InputAction.CallbackContext ctx) { }

    public override void ResetTele()
    {
        base.ResetTele();
        targetRunspeed = runSpeed;
        targetRunStyle = runStyle;
        moveSpeed = runSpeed;
        runStyle = 1f;
        playerstate = playerState.normal;
        runState = RunState.none;
        shootState = ShootState.none;
        dirShootRun = new Vector2(0f, 0f);
        curBullet = maxBullet;
        shootWhenSitDown = false;
    }

    //////////// Ham cua chinh no/////
    private bool CanRun()
    {
        if (playerstate != playerState.normal) { return false; }

        if (!controllReceivingSystem.characterController.isGrounded) { return false; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Dash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9f) return false;
        return true;
    }
    public void startMoveMent(float targetAngle)
    {
        if (controllReceivingSystem.IsLockControl()) { return; }
        animator.SetBool("isRun", true);
        if (shootState == ShootState.runShoot)
        {
            controllReceivingSystem.MovePlayer(targetAngle + Camera.main.transform.eulerAngles.y, runShootMoveSpeed);
            return;
        }
        //di chuyen binh thuong
        controllReceivingSystem.RotatePlayer(targetAngle + Camera.main.transform.eulerAngles.y);
        controllReceivingSystem.MovePlayer(targetAngle + Camera.main.transform.eulerAngles.y, moveSpeed);

    }

    public void ChangeFasrRun()
    {
        animator.SetFloat("runStyle", runStyle);
        if (valueBetween(runStyle, targetRunStyle - 0.15f, targetRunStyle + 0.15f) && valueBetween(moveSpeed, targetRunspeed - 0.5f, targetRunspeed + 0.5f))
        {
            runStyle = targetRunStyle;
            moveSpeed = targetRunspeed;
            return;
        }
        moveSpeed = Mathf.Lerp(moveSpeed, targetRunspeed, Time.deltaTime * changeSpeed);
        runStyle = Mathf.Lerp(runStyle, targetRunStyle, Time.deltaTime * changeSpeed);
    }
    public bool CanDash()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunStyleBlend")) return true;
        return false;
    }
    public bool CanActionC()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunStyleBlend") && runState == RunState.fastRun) return true;
        return false;
    }
    private bool canJump()
    {
        if (!controllReceivingSystem.CheckGrounded()) { return false; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9) { return false; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("ATK")) { return false; }
        if (playerstate != playerState.normal) { return false; }
        return true;
    }
    private bool CanATK()
    {
        if (!controllReceivingSystem.CheckGrounded()) { return false; }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.9) { return false; }
        if (curBullet < 1) { return false; }
        return true;
    }
    private void Jump()
    {
        playAni=("Jump");
        if (runState == RunState.run || runState == RunState.fastRun)
        {
            controllReceivingSystem.Jump(dirFowardJump, moveSpeed + 1f);
            controllReceivingSystem.setRotatePlayer(dirFowardJump);
            return;
        }
        controllReceivingSystem.Jump(0f, 5f);
    }
    private void RunShoot(ShootState state)
    {
        animator.SetBool("isAtk", true);
        if (state == ShootState.runShoot)
            animator.Play("RunShoot");
        animator.SetLayerWeight(animator.GetLayerIndex("LayerHand"), 1f);
        if (state == ShootState.runShoot)
            animator.Play("LayerHand.RunShootUpper");
        else if (state == ShootState.fastRunShoots)
            animator.Play("LayerHand.FastRunShoot");
        // set goc ban -->
        float eulerAngX = Camera.main.transform.localEulerAngles.x;
        float eulerAngy = Camera.main.transform.localEulerAngles.y;
        float xAngle = 0f;
        if (eulerAngy > 180f)
        {
            if (eulerAngX > 256f)
                xAngle = (eulerAngX * (-1f)) + 360f;
            else
                xAngle = -eulerAngX;
        }
        else
        {
            if (eulerAngX > 256f)
                xAngle = 180f - (eulerAngX - 180f);
            else
                xAngle = -(180f + (((eulerAngX * (-1f)) + 180f) * (-1f)));
        }
        SetHoriVertiAnimatorRunShoot(dirShootRun.x, dirShootRun.y, false);
        animator.SetFloat("runShootEular", xAngle);
        controllReceivingSystem.RotatePlayer(Camera.main.transform.eulerAngles.y);
        Shoot();
    }

    private void SitDownShoot()
    {
        animator.SetBool("isAtk", true);
        Vector2 aimPos = aimPoint.GetLocalPos();
        animator.SetFloat("mouseHori", aimPos.x);
        animator.SetFloat("mouseVerti", aimPos.y);
        Shoot();
    }


    [SerializeField] Transform bulletTransform;
    float bulletSpeed = 100f;
    public void Shoot()
    {
        if (playerstate == playerState.BehindTheWall)
        {
            cancleReload();
        }
        if (curBullet < 1)
        {
            endShoot();
            return;
        }
        if (timeCancleFisrtShoot > 0)
        {
            timeCancleFisrtShoot -= Time.deltaTime;
            return;
        }
        if (Time.time - lastTimeShoot > (1f / shootOnSecond))
        {
            lastTimeShoot = Time.time;
            curBullet -= 1;
            aimPoint.shoot(curBullet);
            // What is Cai dong nay?? :)))
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(aimPoint.GetLocalPos().x + 960f, aimPoint.GetLocalPos().y + 540f, 0f));
            RaycastHit hit;
            float tl = 0.02f;
            if (Physics.Raycast(ray, out hit, 1000f, ignoreShoot))
            {
                var length = ((hit.point - bulletTransform.position).magnitude);    //hit.distance;
                Vector3 range = new Vector3(Random.Range(-tl * length, tl * length), Random.Range(-tl * length, tl * length), Random.Range(-tl * length, tl * length));
                Debug.LogWarning("Ke qua Rangdom:  " + range);
                range += hit.point;
                Quaternion rot = Quaternion.LookRotation(range - bulletTransform.position);
                spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpeed, bulletTransform.position, rot);
                Debug.Log("shooting hit point: " + hit.point + " name:" + hit.transform.name);
            }
            else
            {
                Vector3 endPoint = ray.origin + ray.direction * 1000f;
                var length = ((endPoint - bulletTransform.position).magnitude);    //hit.distance;
                Vector3 range = new Vector3(Random.Range(-tl * length, tl * length), Random.Range(-tl * length, tl * length), Random.Range(-tl * length, tl * length));
                Debug.LogWarning("Ke qua Rangdom Maxxx:  " + range);
                range += endPoint;
                Quaternion rot = Quaternion.LookRotation(range - bulletTransform.position);
                spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpeed, bulletTransform.position, rot);
                Debug.Log("shooting raycast not hit every thing so we use the endpoint");
            }

        }
    }

    public void ReLoadBullet()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("LayerHand"), 1f);
        animator.Play("LayerHand.Reload");
    }
    private void cancleReload()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("LayerHand"), 0f);
    }
    public void ReLoadBulletEnd()
    {
        animator.SetLayerWeight(animator.GetLayerIndex("LayerHand"), 0f);
        curBullet = maxBullet;
    }
    public void DashEnd()
    {
        controllReceivingSystem.isDash = false;
    }
    private void endShoot()
    {
        shootState = ShootState.none;
        CallToCameraMan(false);
        //animator.SetLayerWeight(animator.GetLayerIndex("LayerHand"), 0f);
        if (curBullet != maxBullet)
            ReLoadBullet();
        animator.SetBool("isAtk", false);
        timeCancleFisrtShoot = 0.1f;
        shootWhenSitDown = false;
    }

    private void CallToCameraMan(bool isShoot)
    {
        controllReceivingSystem.ChangeRunShoot(isShoot);
    }

    public bool CameraManCheckIsShoot()
    {
        if (shootState == ShootState.runShoot || shootState == ShootState.fastRunShoots)
            return true;
        else return false;
    }
    public ShootState checkStateShootRun()
    {
        if (dirShootRun == new Vector2(0f, 1f))
            return ShootState.fastRunShoots;
        else return ShootState.runShoot;
    }
    public void SetHoriVertiAnimatorRunShoot(float x, float y, bool tmpbool)
    {
        if (tmpbool)
        {
            animator.SetFloat("horizontal", x);
            animator.SetFloat("vertical", y);
            return;
        }
        float tmpx = animator.GetFloat("horizontal");
        float tmpy = animator.GetFloat("vertical");
        animator.SetFloat("horizontal", Mathf.Lerp(tmpx, x, Time.deltaTime * 3f));
        animator.SetFloat("vertical", Mathf.Lerp(tmpy, y, Time.deltaTime * 3f));
    }

    public override void BehindTheWall(Vector3 SitPosition, float dirLookAt)
    {
        playerstate = playerState.BehindTheWall;
        //test
        animator.Play("SitDown");
    }
    public override void cancleBehindTheWall()
    {
        playerstate = playerState.normal;
        controllReceivingSystem.cancleBehindTheWallChild();
    }
    public bool valueBetween(float value, float minValue, float maxValue)
    {
        if (value > Mathf.Min(minValue, maxValue) && value < Mathf.Max(minValue, maxValue)) return true;
        return false;
    }
    private float NomalizeVectorInAnimator(float x)
    {
        if (x > 0.5f) return 1f;
        if (x < -0.5f) return -1f;
        return 0f;
    }
    private void OnAnimatorMove()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            controllReceivingSystem.apllyRootMotion(animator.deltaPosition * 600f);
        }
    }


}
//PlayerState chia la 2 co che chinh: ban tu do va nap sau tuong
public enum playerState
{
    normal,
    BehindTheWall
};
public enum RunState
{
    none,
    run,
    fastRun
};

public enum ShootState
{
    none,
    runShoot,
    fastRunShoots
};
