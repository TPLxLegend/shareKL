using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endmission : MonoBehaviour
{
    public BoxCollider boxcheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkEnemy())
        {
            boxcheck.enabled = false;
        }
        else
        {
            boxcheck.enabled = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Timecount timecount = GameObject.Find("AimCanvas/sysTimeCount").GetComponent<Timecount>();
            if (timecount == null) return;
            timecount.EndGame();
        }
    }

    public bool checkEnemy()
    {
        if(transform.childCount == 0) return false;
        return true;
    }
}
