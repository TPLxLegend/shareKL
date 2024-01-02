using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MachineGun : MonoBehaviour
{
    public Transform body;
    public Transform head;
    public Transform pointLeft;
    public Transform pointRight;

    public List<GameObject> TargetList;
    public bool AttackMode = false;
    private float turnSmoothtime = 0.4f;
    private float turnSmoothVelocity = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(AttackMode)
        {
            //body
            Vector3 betweenEnemeBody = GetTargetNearest().transform.position - body.transform.position;
            float eulerbody = Mathf.Atan2(betweenEnemeBody.x, betweenEnemeBody.z) * Mathf.Rad2Deg;
            float eulerbodySmooth = Mathf.SmoothDampAngle(body.transform.eulerAngles.y, eulerbody, ref turnSmoothVelocity, turnSmoothtime);
            body.transform.rotation = Quaternion.Euler(0f, eulerbodySmooth, 0f);
            //head
            float y = GetTargetNearest().transform.position.y - head.transform.position.y;
            Vector3 newpos = GetTargetNearest().transform.position;
            newpos.y = head.transform.position.y;
            Vector3 dis = newpos - head.transform.position;
            float distand = Vector3.Magnitude(dis);
            float eulerhead = Mathf.Atan2(distand, y) * Mathf.Rad2Deg;
            float eulerheadSmooth = Mathf.SmoothDampAngle(head.transform.eulerAngles.x, eulerhead, ref turnSmoothVelocity, turnSmoothtime);
            head.transform.rotation = Quaternion.Euler(eulerheadSmooth, 0f, 0f);
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && !TargetList.Contains(other.gameObject))
        {
            TargetList.Add(other.gameObject);
            ChangeModeMachineGun();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && TargetList.Contains(other.gameObject))
        {
            TargetList.Remove(other.gameObject);
            ChangeModeMachineGun();
        }
    }

    private void ChangeModeMachineGun()
    {
        if(TargetList.Count>0f)
            AttackMode = true;
        else AttackMode = false;
    }
    private GameObject GetTargetNearest()
    {
        if(!(TargetList.Count>0)) return null;
        float distance = 0f;
        GameObject goNearest = null;
        foreach(GameObject target in TargetList)
        {
            Vector3 dir = target.transform.position - transform.position;
            float distanceBetween = Vector3.SqrMagnitude(dir);
            if(distanceBetween > distance)
            {
                distance = distanceBetween;
                goNearest = target;
            }
        }
        return goNearest;
    }
}
