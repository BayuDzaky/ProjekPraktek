using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    // -- UI -- //
    public GameObject quickSlotsPanel;

    public List<GameObject> quickSlotsList = new List<GameObject>();

    public GameObject numbersHolder;

    public int selectedNumber = -1;
    public GameObject selectedItem;

    public GameObject SelectedItemModel;

    public GameObject toolHolder;   

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            SelectionQuickSlot(1);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            SelectionQuickSlot(2);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectionQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectionQuickSlot(4);
        }


    }

    void SelectionQuickSlot(int number)
    {
        if (checkIfSlotIsFull(number) == true)   
        {

            if (selectedNumber != number)
            {
                selectedNumber = number;

                // Unselect item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;

                }

                selectedItem = getSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                SetEquippedModel(selectedItem);
                


                // ubah warna
                foreach (Transform child in numbersHolder.transform)
                {

                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;

                }


                Text toBeChanged = numbersHolder.transform.Find("number" + number).transform.Find("Text").GetComponent<Text>();
                toBeChanged.color = Color.white;
            }
            else
            {

                selectedNumber = -1;

                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }

                if (SelectedItemModel != null)
                {
                    DestroyImmediate(SelectedItemModel.gameObject);
                    SelectedItemModel = null;
                }

                // ubah warna
                foreach (Transform child in numbersHolder.transform)
                {

                    child.transform.Find("Text").GetComponent<Text>().color = Color.gray;

                }
            }
            
        }

        
    }


    private void SetEquippedModel(GameObject selectedItem)
    {
        if (SelectedItemModel != null)
        {
            DestroyImmediate(SelectedItemModel.gameObject);
            SelectedItemModel = null;
        }

        string selectedItemName = selectedItem.name.Replace("(Clone)", "");
        SelectedItemModel = Instantiate(Resources.Load<GameObject>(selectedItemName + "_Model"),
            new Vector3(-1.737f, -0.454f, 1.224f), Quaternion.Euler(34.078f, -18.911f, -10.168f));
        SelectedItemModel.transform.SetParent(toolHolder.transform, false);
    }





    GameObject getSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber -1 ].transform.GetChild(0).gameObject;
    }

    bool checkIfSlotIsFull (int slotNumber)
    {

        if (quickSlotsList[slotNumber -1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private void Start()
    {
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }

    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);


        InventorySystem.Instance.ReCalculateList();

    }


    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }

    public bool CheckIfFull()
    {

        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }

        if (counter == 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}