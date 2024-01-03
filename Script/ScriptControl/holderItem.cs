using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class holderItem :MonoBehaviour,IItemAction //ItemAction
{
    //date item
    public ItemAction linkItem;
    //View show in Canvas
    public Image iconShow;
    public TextMeshProUGUI showDeception;

    void Start()
    {
        LoadDataHolderItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadDataHolderItem()
    {
        if(linkItem.baseIcon != null)
            iconShow.sprite = linkItem.baseIcon;
        showDeception.SetText(linkItem.baseDeception);
    }
    public void SetDataHolderItem(ItemAction linkitem)
    {
        this.linkItem = linkitem;
        //this.baseIcon = icon;
        //this.baseDeception = Deception;
        LoadDataHolderItem();
    }
    public void UseItem()
    {
        linkItem.UseItem();
    }
}
