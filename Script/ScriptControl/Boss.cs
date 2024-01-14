using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Slider slider;
    public List<GameObject> player;
    public bool action = false;
    public bool attack = false;
    public float timecount = 10f;

    public GameObject superBullet; 
    public GameObject pointspawn;
    public GameObject target;

    public enemyInfo info;

    private float turnSmoothtime = 0.2f;
    private Vector3 turnSmoothVelocityV3 = Vector3.zero;

    float tmp = 2f;
    float bulletSpeed = 50f;

    public int maxNumButllet = 10;
    public int curButllet = 0;
    private bool reload = false;
    private float timeReload = 3f;
    private float curTimeReload = 0f;
    private float timeShootBigBullet = 0f;
    private float delayshoot = 0f;
    private bool died = false;
    void Start()
    {
        curButllet = maxNumButllet;
        info=GetComponent<enemyInfo>();
        slider.maxValue = info.maxHP;
        info.hp.OnValueChanged += (v1, v2) =>
        {
            slider.value = v2;
        };
        died = false;
        info.onDie.AddListener(die);
    }
    public void die(characterInfo info)
    {
        died = true;
        GetComponent<dissolve>().RunDisolve();
        Destroy(gameObject, 10);
    }
    // Update is called once per frame
    void Update()
    {
        if (reload)
        {
            ReloadBuleet();
        }
        if (action)
        {
            if (checkHeight() < 6f)
            {
                transform.position += Vector3.up * 2f * Time.deltaTime;
            }
        }
        if(died) { return; }
        if(player.Count > 0)
        {
            attack = true;
            timecount += Time.deltaTime;
            if (timecount>5f)
            {
                target = RandomTarget();
                //transform.LookAt(target.transform.position);
            }
            if(target != null)
            {
                Vector3 dir = target.transform.position + new Vector3(0f,0f,0f) + new Vector3(Random.Range(-2f,2f), Random.Range(0, 2f), Random.Range(-2f, 2f)) - transform.position;
                Vector3 dirForward = transform.forward;
                dirForward = Vector3.SmoothDamp(dirForward, dir, ref turnSmoothVelocityV3, turnSmoothtime * 2f);
                Quaternion lookRotation = Quaternion.LookRotation(dirForward);
                transform.rotation = lookRotation;

                timeShootBigBullet += Time.deltaTime;
                if(timeShootBigBullet >10f)
                {
                    timeShootBigBullet = 0f;
                    delayshoot = 2f;
                    spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId,
              15f, pointspawn.transform.position, lookRotation, info.farAttackDmgType,100, info.critRate, info.critDmg,superBullet.name);
                }
            }
        }
        else
            attack = false;
        if (attack && curButllet > 0 &&delayshoot<0f)
        {
            shoot(transform.rotation);
        }
        delayshoot-=Time.deltaTime;
        if (timeShootBigBullet > 10f)
        {
            //ban nig bullet;
            timeShootBigBullet = 0f;
        }
        else
            timeShootBigBullet += Time.deltaTime;
    }
    private float checkHeight()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position,-1*transform.up,out hit,1000f))
        {
            return hit.distance;
        }
        return 20f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !player.Contains(other.gameObject))
        {
            player.Add(other.gameObject);
            action = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && player.Contains(other.gameObject))
        {
            player.Remove(other.gameObject);
        }
    }
    private GameObject RandomTarget()
    {
        if (player.Count == 0) return null;
        int i = Random.Range(0, player.Count);
        return player[i];
    }
    private void shoot(Quaternion dir)
    {
        if (Time.time - tmp > 0.2f)
        {
            spawnPlayerSystem.Instance.spawnBulletServerRpc(NetworkManager.Singleton.LocalClientId, 
                bulletSpeed, pointspawn.transform.position, dir,info.farAttackDmgType, info.attack, info.critRate, info.critDmg);
            tmp = Time.time;
            curButllet--;
            if (curButllet <= 0)
            {
                reload = true;
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
            return;
        }
        curTimeReload += Time.deltaTime;
    }
}
