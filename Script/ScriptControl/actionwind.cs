using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionwind : MonoBehaviour
{
    public GameObject listenemy;
    public GameObject wind;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkEnemy())
        {
            wind.SetActive(false);
        }
        else
        {
            wind.SetActive(true);
        }
    }
    public bool checkEnemy()
    {
        if (listenemy.transform.childCount == 0) return false;
        return true;
    }
}
