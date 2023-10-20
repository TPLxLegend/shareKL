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
    private bool Control = true;
    private float sensitivity_y = -200f;
    private float sensitivity_x = 100f;
    private Vector3 rotate;
    public float eulerAngX;

    [SerializeField]
    private Vector2 passValueCameraRotate = Vector2.zero;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        Target = GameObject.Find("Players/TargetLook").transform;

        //StartCoroutine(CreateEx());
        PlayerController.Instance.input.Player.look.performed += ctx => { Debug.Log("look"); RotateCamera(ctx.ReadValue<Vector2>()); };
        PlayerController.Instance.input.Player.look.canceled += ctx => { cancleRotateCamera(); };
        Debug.Log("character:" + PlayerController.Instance.input);

    }

    // Update is called once per frame
    void Update()
    {
        FollowCharacter();
        // RotateCamera();
        
    }

    private void FollowCharacter()
    {
        if (Target == null) { return; }
        transform.position = Target.position;
    }
    private void RotateCamera(Vector2 dir)
    {
        if (!Control) { return; }
        //reset Z_rotate
        Vector3 currotation = transform.rotation.eulerAngles;
        currotation.z = 0f;
        transform.rotation = Quaternion.Euler(currotation);
        //
        float mouseX = 0, mouseY = 0;
        //mouseX = dir.x; mouseY = dir.y;
        mouseX = dir.x - passValueCameraRotate.x;
        mouseY = dir.y - passValueCameraRotate.y;

        Debug.Log(dir + " --- " + passValueCameraRotate);

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
        passValueCameraRotate = dir;
    }

    private void cancleRotateCamera() 
    {
        passValueCameraRotate = Vector2.zero;
    }
    IEnumerator CreateEx()
    {
        yield return new WaitForSeconds(1);
        
    }
}
