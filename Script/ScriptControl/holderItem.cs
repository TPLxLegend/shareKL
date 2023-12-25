using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class holderItem : ItemAction
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
        if(baseIcon != null)
            iconShow.sprite = baseIcon;
        showDeception.SetText(baseDeception);
    }
    public void SetDataHolderItem(ItemAction linkitem, Sprite icon, string Deception)
    {
        this.linkItem = linkitem;
        this.baseIcon = icon;
        this.baseDeception = Deception;
        LoadDataHolderItem();
    }
    public override void UseItem()
    {
        base.UseItem();
        linkItem.UseItem();
    }
}
