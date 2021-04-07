using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;
    public float attStr;
    public int itemID;

    void Start()
    {
        Debug.Log("Item Name: " + itemName);
        Debug.Log("Description: " + itemDesc);
        Debug.Log("Attack Strength: " + attStr);
        Debug.Log("Item ID: " + itemID);
    }

}