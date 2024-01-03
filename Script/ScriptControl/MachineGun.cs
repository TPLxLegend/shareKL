using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;
using Unity.UI;
using UnityEngine.UI;

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
    float bulletSpeed = 100f;

    public int maxNumButllet = 100;
    public int curButllet = 0;

    private bool reload = false;
    private float timeReload = 4f;
    private float curTimeReload = 0f;

    public TextMeshProUGUI textMeshProUGUI;
    public Slider slider;
    void Start()
    {
        curButllet = maxNumButllet;
    }

    // Update is called once per frame
    void Update()
    {
        if (reload)
        {
            ReloadBuleet();
        }
        if(AttackMode)
        {
            //body
            Vector3 betweenEnemeBody = GetTargetNearest().transform.position - body.transform.position;
            float eulerbody = Mathf.Atan2(betweenEnemeBody.x, betweenEnemeBody.z) * Mathf.Rad2Deg;
            float eulerbodySmooth = Mathf.SmoothDampAngle(body.transform.eulerAngles.y, eulerbody, ref turnSmoothVelocity, turnSmoothtime);
            body.transform.rotation = Quaternion.Euler(0f, eulerbodySmooth, 0f);
            //head
            Vector3 dir = GetTargetNearest().transform.position - head.position;
            Vector3 dirForward = head.transform.forward;
            dirForward = Vector3.SmoothDamp(dirForward, dir, ref turnSmoothVelocityV3, turnSmoothtime*2f);
            Quaternion lookRotation = Quaternion.LookRotation(dirForward);
            head.transform.rotation = lookRotation;

            if(curButllet > 0) 
            {
                shoot(lookRotation);
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy" && !TargetList.Contains(other.gameObject))
        {
            TargetList.Add(other.gameObject);
            ChangeModeMachineGun();
            var info =other.gameObject.GetComponent<enemyInfo>();
            info.onDie.AddListener((i) =>
            {
                TargetList.Remove(i.gameObject);
            });
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
        float distance = float.PositiveInfinity;
        GameObject goNearest = null;
        foreach(GameObject target in TargetList)
        {
            Vector3 dir = target.transform.position - transform.position;
            float distanceBetween = Vector3.SqrMagnitude(dir);
            if(distanceBetween < distance)
            {
                distance = distanceBetween;
                goNearest = target;
            }
        }
        return goNearest;
    }

    private void shoot(Quaternion dir)
    {
        if (Time.time - tmp > 0.1f)
        {
            if (tmpshoot)
            {
                spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpeed, pointRight.position, dir);
            }
            else
            {
                spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, bulletSpeed, pointLeft.position, dir);
            }
            tmpshoot = !tmpshoot;
            tmp = Time.time;
            curButllet--;
            if(curButllet <= 0)
            {
                reload = true;
                showHideUI(true);
            }
        }
    }
    private void ReloadBuleet()
    {
        if (curTimeReload > timeReload)
        {
            curButllet = maxNumButllet;
            curTimeReload = 0f;
            reload = false;
            showHideUI(false);
            return;
        }
        curTimeReload += Time.deltaTime;
        slider.value = curTimeReload;
    }

    private void showHideUI(bool tmp)
    {
        slider.gameObject.SetActive(tmp);
        textMeshProUGUI.gameObject.SetActive(tmp);
    }
}
