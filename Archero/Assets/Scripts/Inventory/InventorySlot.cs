using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("Inventory information")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;

    public InventoryItem item;
    public InventoryManager manager;


    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        item = newItem;
        manager = newManager;
        if (item)
        {
            itemImage.sprite = item.itemSprite;
            itemNumberText.text = "" + item.amount;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickedOn()
    {
        if (item)
        {
            manager.SetupDescriptionAndButton(item.itemDescription, item);
        }
    }
}
