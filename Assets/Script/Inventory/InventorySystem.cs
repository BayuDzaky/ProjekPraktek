using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
    
    public static InventorySystem Instance { get; set; }

    public GameObject inventoryScreenUI;
    public GameObject craftingScreenUI;
    public List<GameObject> SlotList = new List<GameObject>();
    public List<string> ItemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public GameObject InventoryUI;
    public GameObject CraftingUI;
    public GameObject ToolScreen;
    public GameObject InfoItemUi;
    

    public bool isOpen;
    //public bool isFull;

    //PopUp Pick

    public GameObject PickupAlert;
    public Text pickupName;
    public Image pickUpImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    void Start()
    {
        isOpen = false;
       
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                SlotList.Add(child.gameObject);
            }
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {

            Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            Cursor.visible = true;

            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;

            InventoryUI.SetActive(true);
            CraftingUI.SetActive(true);

        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            InventoryUI.SetActive(false);
            CraftingUI.SetActive(false);
            ToolScreen.SetActive(false);

            if (!CraftingSystem.instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.Instance.EnableSelection();
                SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
            }
            
            isOpen = false;
            
        }
    }

    public void OnInventoryTabClicked()
    {
        craftingScreenUI.SetActive(false);
        inventoryScreenUI.SetActive(true);
        ToolScreen.SetActive(false);
    }

    public void OnCraftingTabclicked()
    {
        craftingScreenUI.SetActive(true );
        inventoryScreenUI.SetActive(false);
        ToolScreen.SetActive(false );
    }

    public void ToolScreenUiOpen()
    {
        ToolScreen.SetActive(true);
    }


    public void AddToInventory(string ItemName)
    {
        
            whatSlotToEquip = FindNextEmptySlot();
            itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
            itemToAdd.transform.SetParent(whatSlotToEquip.transform);

            ItemList.Add(ItemName);

        
        TriggerPickupPopUp(ItemName, itemToAdd.GetComponent<Image>().sprite);

        ReCalculateList();
        CraftingSystem.instance.RefreshNeededItem();
    }



    void TriggerPickupPopUp(string ItemName, Sprite itemSprite)
    {
        PickupAlert.SetActive(true);

        pickupName.text = ItemName;
        pickUpImage.sprite = itemSprite;



        StartCoroutine(HidePickupAlertAfterDelay(1f));
    }

    private IEnumerator HidePickupAlertAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        PickupAlert.SetActive(false);
    }




    private GameObject FindNextEmptySlot()
    {
        foreach(GameObject slot in SlotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;

            }
        }
        
        return new GameObject();



    }

    public bool CheckifFull()
    {
        int counter = 0;


        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }

            
        }
        if (counter == 16)
        {
            return true;
        }
        else
        {
            return false;
        }


    }

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;

        for (var i = SlotList.Count - 1; i >= 0; i--)
        {
            if (SlotList[i].transform.childCount > 0)
            {
                if (SlotList[i].transform.GetChild(0).name == nameToRemove + "(Clone)" && counter > 0)
                {
                    Destroy(SlotList[i].transform.GetChild(0).gameObject);
                    counter--; // Mengurangi counter setiap kali item dihapus
                }
            }
        }

        ReCalculateList();
        CraftingSystem.instance.RefreshNeededItem();
    }
    public void ReCalculateList()
    {
        ItemList.Clear();


        foreach(GameObject slot in SlotList)
        {
            if(slot.transform.childCount > 0)
            {

                string name = slot.transform.GetChild(0).name;
               
                string str2 = "(Clone)";

                string result = name.Replace(str2,"");



                ItemList.Add(result);
            }


        }


    }

}