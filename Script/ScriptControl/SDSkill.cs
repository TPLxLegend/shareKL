using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SDSkill : MonoBehaviour
{
    public Image CDE;
    public Image CDQ;
    public TextMeshProUGUI timeEText;
    public TextMeshProUGUI timeQText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCD(float skille, float skillQ,string timeE, string timeQ)
    {
        CDE.fillAmount = skille;
        CDQ.fillAmount = skillQ;
        timeEText.SetText(timeE);
        timeQText.SetText(timeQ);
    }
}
