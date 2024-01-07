using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ActionUltimateCutScene : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;
    public List<Canvas> canvass = new List<Canvas>();
    [SerializeField] Material screen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public  void ActionUltimate(Vector3 position, Quaternion quater)
    {
        transform.position = position;
        transform.rotation = quater;
        playableDirector.Play();
        Debug.Log("duration"+playableDirector.duration);
        //screen.SetInt("_useEffect", 1);
        //await Task.Delay((int)(playableDirector.duration * 1000));
       // screen.SetInt("_useEffect", 0);
        Debug.Log("state:"+screen.GetInt("_useEffect")+"after turn off : " + Time.time);
    }
    public void disableUI()
    {
        foreach (Canvas c in canvass)
        {
            c.GetComponent<Canvas>().enabled = false;
        }
    }
    public void enableUI()
    {
        foreach (Canvas c in canvass)
        {
            c.GetComponent<Canvas>().enabled = true;
        }
    }
}
