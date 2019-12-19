using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tree : MonoBehaviour
{
    public bool playerInRange;
    public Inventory playerInventory;
    public Item ressource;
    public bool isCutDown;
    public GameObject dialogBox;
    public Text dialogText;
    public string dialog;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("interact") && playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                if (!isCutDown)
                {
                    HarvestTree();
                }
            }
        }
    }

    private void HarvestTree()
    {
        dialogBox.SetActive(true);
        dialogText.text = "You found 10 logs!";
        //ressource = GetComponent<Item>();
        //playerInventory.AddItem(ressource);
        //playerInventory.currentItem = ressource;
        isCutDown = true;
        StartCoroutine(GrowingCoroutine());

    }

    private IEnumerator GrowingCoroutine()
    {
        animator.SetBool("isGrowing", true);
        yield return null;
        yield return new WaitForSeconds(25f);
        animator.SetBool("isGrowing", false);
        isCutDown = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerTag"))
        {
            playerInRange = true;
            Debug.Log("player in range");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerTag"))
        {
            playerInRange = false;
            Debug.Log("player exit");
        }

    }
}
