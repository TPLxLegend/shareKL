using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
public class FressFScrollView : MonoBehaviour
{
    public static FressFScrollView instance;

    public Scrollbar scrollBar;
    public GameObject HolderSelected;// de su li nhan F
    public int numberSelected;
    public float scroll_pos;
    public float[] posHolderList;
    public float distanceHolder;
    private float speedChange = 0.1f;
    public Canvas parentCanvas;

    //prefab HolderItem
    public GameObject prefabHolder;
    //List data
    public List<ItemAction> listItem = new List<ItemAction>();
    
    // Start is called before the first frame update

    public Sprite tmps;
    void Start()
    {  
        if(instance == null) instance = this;
        ChangedList();
        PlayerController.Instance.input.Player.FressF.performed += PressFItem;
    }

    private void PressFItem(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (listItem.Count == 0) return;
        listItem[numberSelected].UseItem();
    }

    // Update is called once per frame
    void Update()
    {
        ShowList();
    }
    private void ShowList()
    {
        //spamListToView();
        if (listItem.Count == 0)
        {
            scroll_pos = 1f;
            distanceHolder = 1f;
            numberSelected = 0;
            listItem.Clear();
            posHolderList = null;
            HolderSelected = null;
            return;
        }
        if (Input.mouseScrollDelta.y != 0)
        {
            float tmp = Input.mouseScrollDelta.y;
            scrollBar.GetComponent<Scrollbar>().value += tmp * speedChange;
            if(scrollBar.GetComponent<Scrollbar>().value > 1f) 
                scrollBar.GetComponent<Scrollbar>().value = 1f;
            if (scrollBar.GetComponent<Scrollbar>().value < 0f)
                scrollBar.GetComponent<Scrollbar>().value = 0f;
            scroll_pos = scrollBar.GetComponent<Scrollbar>().value;
        }
        
        ReCheckSelected();
    }
    private void ChangedList()
    {
        if (listItem.Count ==0)
        {
            posHolderList = null;
            parentCanvas.GetComponent<Canvas>().enabled = false;
            
            return;
        }
        else
            parentCanvas.GetComponent<Canvas>().enabled = true;

        posHolderList = new float[listItem.Count];
        if (listItem.Count == 1)
        {
            distanceHolder = 3f;
        }
        else
        {  
            distanceHolder = 1f / (posHolderList.Length - 1f);
        }
        speedChange = distanceHolder;
        for (int i = 0; i < posHolderList.Length; i++)
        {
            posHolderList[i] = distanceHolder * i;
        }
    }
    private bool IsBetween(float x, float target)
    {
        if(x<(target+ distanceHolder/2) && x>=(target - distanceHolder/2))return true;
        return false;
    }
    private void ReCheckSelected()
    {
        for (int i = 0; i < posHolderList.Length; i++)
        {
            if (IsBetween(1f - scroll_pos, posHolderList[i]))
            {
                HolderSelected = transform.GetChild(i).gameObject;
                numberSelected = i;
                ChangeColor();
                break;
            }
            HolderSelected = null;
        }
        
    }
    private void ChangeColor()
    {
        for (int i = 0; i < posHolderList.Length; i++)
        {
            if(numberSelected ==i)
            {
                var color = transform.GetChild(i).GetComponent<Button>().colors;
                color.normalColor = Color.yellow;
                transform.GetChild(i).GetComponent<Button>().colors = color;
            }
            else
            {
                var color = transform.GetChild(i).GetComponent<Button>().colors;
                color.normalColor = Color.white;
                transform.GetChild(i).GetComponent<Button>().colors = color;
            }
        }
    }

    public void AddItem(ItemAction item)
    {
        if (listItem.Contains(item)) return;
        listItem.Add(item);
        spamListToView();
    }
    public void RemoveItem(ItemAction item)
    {
        if (!listItem.Contains(item)) return;
        listItem.Remove(item);
        spamListToView();
    }
    private void spamListToView()
    {
        DeleView();
        foreach(ItemAction item in listItem)
        {
            GameObject go = Instantiate(prefabHolder,transform);
            var holderItemScript = go.GetComponent<holderItem>();
            holderItemScript.SetDataHolderItem(item);
           // go.GetComponent<NetworkObject>().TrySetParent(transform,false);
        }
        ChangedList();
    }
    private void DeleView()
    {
        foreach(Transform go in transform)
        {
            Destroy(go.gameObject);
        }
    }
}
