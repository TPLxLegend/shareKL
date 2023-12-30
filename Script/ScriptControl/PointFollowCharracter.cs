using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PointFollowCharracter : Singleton<PointFollowCharracter>
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private ControllReceivingSystem controllReceivingSystem;
    [SerializeField]
    private bool Control = true;
    private float sensitivity_y = -0.1f;
    private float sensitivity_x = 0.035f;
    private Vector3 rotate;
    public float eulerAngX;
    private bool showCursor = false;
    //Zoom camera
    [SerializeField]
    CinemachineVirtualCamera vcam;

    [SerializeField]
    private float speedScroll = 2f;
    [SerializeField]
    private float maxzoom = 50f;
    [SerializeField]
    private float minzoom = 30f;
    [SerializeField]
    private float nomalzoom = 40f;
    [SerializeField]
    private float turnsmoothTime = 0.15f;

    //Rotate camera when behind the wall
    private bool isBehind = false;
    private float dirLookTarget;
    //UI aim
    public aimPoint aim;

    private float turnsmoothVelocity = 0.0f;
    private bool returnnomal = false;
    public bool canZoom = true;

    void Start()
    {
        /*
        Target = GameObject.Find("Players(Clone)/TargetLook").transform;
        controllReceivingSystem = GameObject.Find("Players(Clone)").GetComponent<ControllReceivingSystem>();
        PlayerController.Instance.input.Player.look.performed += ctx => { RotateCamera(ctx); };
        PlayerController.Instance.input.Player.look.canceled += ctx => { cancleRotateCamera(); };
        Debug.Log("character:" + PlayerController.Instance.input);
        */
        trackPlayer(PlayerController.Instance.player.transform);
        aim = GameObject.Find("AimCanvas/aimPoint").GetComponent<aimPoint>();
    }

    public void trackPlayer(Transform target)
    {
        Target = target;
        this.controllReceivingSystem = Target.GetComponent<ControllReceivingSystem>();//  controllReceivingSystem;
        //controllReceivingSystem = PlayerController.Instance.player.GetComponent<ControllReceivingSystem>();
        controllReceivingSystem.onBehindTheWallCalled.AddListener(
            LookAtBehindTheWall
            );
        controllReceivingSystem.cancleBehindTheWall.AddListener(
            endBehindTheWall
            );
        PlayerController.Instance.input.Player.look.performed += ctx => { RotateCamera(ctx); };
        PlayerController.Instance.input.Player.look.canceled += ctx => { cancleRotateCamera(); };
        Debug.Log("character:" + PlayerController.Instance.input);
        ShowHideCursor(false);
    }

    void Update()
    {
        FollowCharacter();
        //MouseRotateCamera();
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            showCursor = !showCursor;
            ShowHideCursor(showCursor);
        }

        if (isBetween(vcam.m_Lens.FieldOfView, nomalzoom, 0.1f))
        {
            canZoom = true;
            returnnomal = false;
            vcam.m_Lens.FieldOfView = nomalzoom;
        }
        if ((Input.mouseScrollDelta.y != 0) && canZoom)
            zoomCam();
        if (Input.GetMouseButtonDown(2))
            returnnomal = true;
        if (returnnomal)
            zoomCamNomal();
        if (controllReceivingSystem.isShoot)
        {
            ChangeShoulderOffset(0.4f);
        }
        else
        {
            ChangeShoulderOffset(0f);
        }
        if (isBehind)
        {
            float tmp = transform.rotation.eulerAngles.y;
            tmp = Mathf.Lerp(tmp, dirLookTarget, 0.1f);
            transform.rotation = Quaternion.Euler(0f, tmp, 0f);
            if (isBetween(transform.rotation.eulerAngles.y, dirLookTarget, 0.1f))
            {
                isBehind = false;
            }

        }
    }

    private void MouseRotateCamera()
    {
        if (controllReceivingSystem.IsLockControl()) { return; }
        if (controllReceivingSystem.isBehindTheWall) 
        {
            return; 
        }
        Vector3 currotation = transform.rotation.eulerAngles;
        currotation.z = 0f;
        transform.rotation = Quaternion.Euler(currotation);
        float mouseX = 0, mouseY = 0;
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        rotate = new Vector3(mouseY * sensitivity_x, mouseX * sensitivity_y, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
        eulerAngX = transform.localEulerAngles.x;

        //Debug.Log(rotate);
        if ((eulerAngX > 70f) && (eulerAngX < 140f))
        {
            Vector3 temp = transform.eulerAngles;
            temp.x = 70.0f;
            transform.rotation = Quaternion.Euler(temp);
        }
        else if ((eulerAngX < 290f) && (eulerAngX > 160f))
        {
            Vector3 temp = transform.rotation.eulerAngles;
            temp.x = 290f;
            transform.rotation = Quaternion.Euler(temp);
        }
    }

    private void FollowCharacter()
    {
        if (Target == null) { return; }
        transform.position = Target.position;
    }
    public void ShowHideCursor(bool showcursor)
    {
        if (showcursor)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            controllReceivingSystem.LockControl(true);
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            controllReceivingSystem.LockControl(false);
        }
    }
    private void RotateCamera(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        if (controllReceivingSystem.IsLockControl()) { return; }
        if (controllReceivingSystem.isBehindTheWall) 
        {
            aim.SetPosAimPoint(dir.x, dir.y);
            return; 
        }
        Vector3 currotation = transform.rotation.eulerAngles;
        currotation.z = 0f;
        transform.rotation = Quaternion.Euler(currotation);
        //
        float mouseX = 0, mouseY = 0;
        mouseX = dir.x;
        mouseY = dir.y;

        //Debug.Log(dir);

        rotate = new Vector3(mouseY * sensitivity_x, mouseX * sensitivity_y, 0);
        transform.eulerAngles = transform.eulerAngles - rotate;
        eulerAngX = transform.localEulerAngles.x;

        //Debug.Log(rotate);
        if ((eulerAngX > 70f) && (eulerAngX < 140f))
        {
            Vector3 temp = transform.eulerAngles;
            temp.x = 70.0f;
            transform.rotation = Quaternion.Euler(temp);
        }
        else if ((eulerAngX < 290f) && (eulerAngX > 160f))
        {
            Vector3 temp = transform.rotation.eulerAngles;
            temp.x = 290f;
            transform.rotation = Quaternion.Euler(temp);
        }
    }

    private void cancleRotateCamera()
    {
    }

    public void zoomCam()
    {
        vcam.m_Lens.FieldOfView -= Input.mouseScrollDelta.y * speedScroll;
        if (vcam.m_Lens.FieldOfView > maxzoom)
            vcam.m_Lens.FieldOfView = maxzoom;
        else if (vcam.m_Lens.FieldOfView < minzoom)
            vcam.m_Lens.FieldOfView = minzoom;
    }
    public void zoomCamNomal()
    {
        vcam.m_Lens.FieldOfView = Mathf.SmoothDamp(vcam.m_Lens.FieldOfView, nomalzoom, ref turnsmoothVelocity, turnsmoothTime);
        canZoom = false;
    }
    public void LookAtBehindTheWall(float dir)
    {
        isBehind = true;
        dirLookTarget = dir;
    }
    public void endBehindTheWall()
    {
        aim.ResetAimPos();
    }

    private bool isBetween(float x, float target, float diference)
    {
        return ((x > target - diference) && (x < target + diference));
    }
    private void ChangeShoulderOffset(float target)
    {
        if (isBetween(vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x, target, 0.005f))
        {
            vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x = target;
        }
        float tmp = vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x;
        tmp = Mathf.Lerp(tmp, target, Time.deltaTime * 9f);
        vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>().ShoulderOffset.x = tmp;

    }
}
