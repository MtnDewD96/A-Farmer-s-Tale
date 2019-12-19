using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item",menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemSprite;
    public int amount;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;

    public void Craft()
    {

        thisEvent.Invoke();
    }

    public void IncreaseAmount(int amountToIncrease)
    {
        amount += amountToIncrease;
    }
}
