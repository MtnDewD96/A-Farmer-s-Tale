using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject emptyItemSlot;
    [SerializeField] private GameObject inventorPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject craftButton;
    public InventoryItem currentItem;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        craftButton.SetActive(true);
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for(int i = 0; i< playerInventory.myInventory.Count; i++)
            {
                GameObject temp = Instantiate(emptyItemSlot, inventorPanel.transform.position, Quaternion.identity);
                temp .transform.SetParent(inventorPanel.transform);
                InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                if (newSlot)
                {
                    newSlot.Setup(playerInventory.myInventory[i], this);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionText, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionText;
        //craftButton.SetActive(true);
    }

    public void CraftButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Craft();
        }
    }
}
