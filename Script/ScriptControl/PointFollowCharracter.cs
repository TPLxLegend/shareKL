using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PointFollowCharracter : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private ControllReceivingSystem controllReceivingSystem;
    [SerializeField]
    private bool Control = true;
    private float sensitivity_y = -0.1f;
    private float sensitivity_x = 0.01f;
    private Vector3 rotate;
    public float eulerAngX;
    private bool showCursor = false;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        Target = GameObject.Find("Players/TargetLook").transform;
        controllReceivingSystem = GameObject.Find("Players").GetComponent<ControllReceivingSystem>();
        PlayerController.Instance.input.Player.look.performed += ctx => { RotateCamera(ctx); };
        PlayerController.Instance.input.Player.look.canceled += ctx => { cancleRotateCamera(); };
        Debug.Log("character:" + PlayerController.Instance.input);

        ShowHideCursor(false);
    }

    // Update is called once per frame
    void Update()
    {
        FollowCharacter();
        //MouseRotateCamera();
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            showCursor = !showCursor;
            ShowHideCursor(showCursor);
        }
    }

    private void MouseRotateCamera()
    {
        if (controllReceivingSystem.IsLockControl()) { return; }
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
        if(showcursor)
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
        if (controllReceivingSystem.IsLockControl()) { return; }
        Vector2 dir = ctx.ReadValue<Vector2>();
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
}
