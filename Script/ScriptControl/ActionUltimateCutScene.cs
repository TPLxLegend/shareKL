using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Unity.UI;

public class ActionUltimateCutScene : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;
    public List<Canvas> canvass = new List<Canvas>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActionUltimate(Vector3 position, Quaternion quater)
    {
        transform.position = position;
        transform.rotation = quater;
        playableDirector.Play();
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
