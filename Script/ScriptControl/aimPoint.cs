using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class aimPoint : MonoBehaviour
{
    public TextMeshProUGUI bulletNum;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    private float indlePos = 25f;
    private float indleSubPos = -25f;
    private float distance = 300f;
    public int x = 1;
    // gtri di chuyen tam
    public float curDistance = 0f;

    private int tmpnum = 110;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveAim();
    }
    private void moveAim()
    {
        //up
        float tmp = up.transform.localPosition.y;
        tmp += curDistance * Time.deltaTime;
        if(tmp < indlePos) { tmp = indlePos; }
        if (tmp > indlePos + 20f) { tmp = indlePos + 20f; }
        up.transform.localPosition = new Vector3(0f, tmp, 0f);
        //down
        tmp = down.transform.localPosition.y;
        tmp -= curDistance * Time.deltaTime;
        if (tmp > indleSubPos) { tmp = indleSubPos; }
        if (tmp < indleSubPos - 20f) { tmp = indleSubPos - 20f; }
        down.transform.localPosition = new Vector3(0f, tmp, 0f);
        //left
        tmp = left.transform.localPosition.x;
        tmp -= curDistance * Time.deltaTime;
        if (tmp > indleSubPos) { tmp = indleSubPos; }
        if (tmp < indleSubPos - 20f) { tmp = indleSubPos - 20f; }
        left.transform.localPosition = new Vector3(tmp, 0f, 0f);
        //right
        tmp = right.transform.localPosition.x;
        tmp += curDistance * Time.deltaTime;
        if (tmp < indlePos) { tmp = indlePos; }
        if (tmp > indlePos + 20f) { tmp = indlePos + 20f; }
        right.transform.localPosition = new Vector3(tmp, 0f, 0f);

        //////
        curDistance -= x * (Mathf.Sqrt(x)) + 10f;
        if (curDistance < -300f)  
        {
            x = 1;
            curDistance = -300f;
        }
        else
            x++;
    }
    public void shoot(float num)
    {
        curDistance = distance;
        bulletNum.SetText(num.ToString());
    }
    public void setBullet(float num)
    {
        bulletNum.SetText(num.ToString());
    }
    public void SetPosAimPoint(float x,float y)
    {
        float tmpx = transform.localPosition.x;
        float tmpy = transform.localPosition.y;
        tmpx += x * 2f;
        tmpy += y * 2f;
        float xpos, ypos;
        if (isBetween(tmpx, -(Screen.width/2 ), (Screen.width/2 )))
            xpos = tmpx;
        else
        {
            if (tmpx < -(Screen.width/2)) { xpos = -(Screen.width / 2); }
            else { xpos = (Screen.width / 2); }
        }
        if (isBetween(tmpy, -(Screen.height/2), (Screen.height / 2)))
            ypos = tmpy;
        else
        {
            if (tmpy < -(Screen.height / 2)) { ypos = -(Screen.height / 2); }
            else { ypos = (Screen.height / 2); }
        }
        transform.localPosition = new Vector3(xpos, ypos, 0f);

    }
    public void ResetAimPos()
    {
        transform.localPosition = Vector3.zero;
    }
    public Vector2 GetLocalPos()
    {
        return new Vector2(transform.localPosition.x,transform.localPosition.y);
    }
    private bool isBetween(float x, float min, float max)
    {
        if(x>=min && x<=max)    { return true; }
        return false;
    }
}
