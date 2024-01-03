using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class MachineGun : MonoBehaviour
{
    public Transform body;
    public Transform head;
    public Transform pointLeft;
    public Transform pointRight;

    public List<GameObject> TargetList;
    public bool AttackMode = false;
    private float turnSmoothtime = 0.2f;
    private float turnSmoothVelocity = 0.0f;
    private Vector3 turnSmoothVelocityV3 = Vector3.zero;
    // Start is called before the first frame update
    float tmp = 2f;
    bool tmpshoot = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (AttackMode)
        {
            var _ = attack();

        }
    }
    async System.Threading.Tasks.Task attack()
    {
        await System.Threading.Tasks.Task.Run(
() =>
{
    //body
    Vector3 betweenEnemeBody = GetTargetNearest().transform.position - body.transform.position;
    float eulerbody = Mathf.Atan2(betweenEnemeBody.x, betweenEnemeBody.z) * Mathf.Rad2Deg;
    float eulerbodySmooth = Mathf.SmoothDampAngle(body.transform.eulerAngles.y, eulerbody, ref turnSmoothVelocity, turnSmoothtime);
    body.transform.rotation = Quaternion.Euler(0f, eulerbodySmooth, 0f);
    //head
    Vector3 dir = GetTargetNearest().transform.position - head.position;
    Vector3 dirForward = head.transform.forward;
    dirForward = Vector3.SmoothDamp(dirForward, dir, ref turnSmoothVelocityV3, turnSmoothtime * 2f);
    Quaternion lookRotation = Quaternion.LookRotation(dirForward);
    head.transform.rotation = lookRotation;

    float bulletSpeed = 100f;
    if (Time.time - tmp > 0.1f)
    {
        if (tmpshoot)
        {
            spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpeed, pointRight.position, lookRotation);
        }
        else
        {
            spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpeed, pointLeft.position, lookRotation);
        }
        tmpshoot = !tmpshoot;
        tmp = Time.time;
    }
}
        );

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out enemyInfo enemy) && !TargetList.Contains(other.gameObject))
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
        if (TargetList.Count > 0f)
            AttackMode = true;
        else AttackMode = false;
    }
    private GameObject GetTargetNearest()
    {
        if (!(TargetList.Count > 0)) return null;
        float distance = float.PositiveInfinity;
        GameObject goNearest = null;
        foreach (GameObject target in TargetList)
        {
            Vector3 dir = target.transform.position - transform.position;
            float distanceBetween = Vector3.SqrMagnitude(dir);
            if (distanceBetween < distance)
            {
                distance = distanceBetween;
                goNearest = target;
            }
        }
        return goNearest;
    }
}
