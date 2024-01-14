using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timecount : MonoBehaviour
{
    public TextMeshProUGUI TimeCount;
    public TextMeshProUGUI TimeCountend;
    public GameObject showResetGame;
    public bool counting = false;
    float time = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void actionTimeCount()
    {
        counting = true;
        time = 0f;
        TimeCount.gameObject.SetActive(true);
        showResetGame.SetActive(false);
        StartCoroutine("countTimePlay");
    }
    public void EndGame()
    {
        showResetGame.SetActive(true);
        counting = false;
        int timeInt = (int)time;
        int minute = timeInt / 60;
        int second = timeInt % 60;
        TimeCountend.SetText(minute.ToString() + " : " + second.ToString());
    }
    IEnumerator countTimePlay()
    {
        while (counting)
        {
            time += Time.deltaTime;
            int timeInt = (int) time;
            int minute = timeInt / 60;
            int second = timeInt % 60;
            TimeCount.SetText(minute.ToString() + " : " + second.ToString());
            yield return null;
        }
    }

    public async void resetGame()
    {
        TimeCount.gameObject.SetActive(true);
        showResetGame.SetActive(false);
        //telePlayer, spawn enemy
        playerReLifePoint.resetCurToDefault();
        //StartCoroutine(playerReLifePoint.current.ReSpawn());
        playerReLifePoint.current.reSpawnAllPlayerServerRpc();
        var sceneMan= NetworkManager.Singleton.SceneManager;
        Debug.Log("scene count "+spaceGate.sceneAdded.Count);
        foreach(Scene scene in spaceGate.sceneAdded)
        {
            Debug.Log("unload "+scene.name);
            sceneMan.UnloadScene(scene);
            await Task.Delay(2000);
        }
      
        //actionTimeCount();
    }
}
