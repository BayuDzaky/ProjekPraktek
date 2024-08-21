using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();

    //
    Button toolsBTN;

    //Tombak
    Button CraftPedangBTN;
    //Obor
    Button CraftOborBTN;

    //Tombak
    Text PedangReq1, PedangReq2;
    //Obor
    Text OborReq1, OborReq2;

    public bool isOpen;

    //Tombak
    public Blueprint PedangBLP = new Blueprint("Pedang", 2, "Batu", 3, "Ranting", 3);

    public Blueprint OborBLP = new Blueprint("Obor", 2, "Getah", 2, "Ranting", 1);


    //Obor


    public static CraftingSystem instance { get; set; }



    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        //pedang
        PedangReq1 = toolsScreenUI.transform.Find("Pedang").transform.Find("Req1").GetComponent<Text>();
        PedangReq2 = toolsScreenUI.transform.Find("Pedang").transform.Find("Req2").GetComponent<Text>();

        CraftPedangBTN = toolsScreenUI.transform.Find("Pedang").transform.Find("Button").GetComponent<Button>();

        CraftPedangBTN.onClick.AddListener(delegate { CraftAnyItem(PedangBLP); });


        //obor
        OborReq1 = toolsScreenUI.transform.Find("Obor").transform.Find("Req1Obor").GetComponent<Text>();
        OborReq2 = toolsScreenUI.transform.Find("Obor").transform.Find("Req2Obor").GetComponent<Text>();

        CraftOborBTN = toolsScreenUI.transform.Find("Obor").transform.Find("ButtonObor").GetComponent<Button>();

        CraftOborBTN.onClick.AddListener(delegate { CraftAnyItem(OborBLP); });
    }


    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }

    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        // add item ke inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);
        
        if(blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            

        }
        else if(blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }
        //Obor


        StartCoroutine(calculate());

        //RefreshNeededItem();
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
        RefreshNeededItem();

    }



    // Update is called once per frame
    void Update()
    {
        //RefreshNeededItem();

        //if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        //{
        //
        //    Debug.Log("i is pressed");
        //    craftingScreenUI.SetActive(true);
         //   Cursor.lockState = CursorLockMode.None;
        //    isOpen = true;
        //    Cursor.visible = true;

        //}
        //else if (Input.GetKeyDown(KeyCode.C) && isOpen)
       // {
          //  craftingScreenUI.SetActive(false);
          //  toolsScreenUI.SetActive(false );

          //  if (!InventorySystem.Instance.isOpen)
           // {
           //     Cursor.lockState = CursorLockMode.Locked;
           //     Cursor.visible = false;
           // }
            
          //  isOpen = false;
            
        //}
    }

    

    public void RefreshNeededItem()
    {
        int stone_count = 0;
        

        int Getah_count = 0;
        int Ranting_count = 0;


        inventoryItemList = InventorySystem.Instance.ItemList;

        foreach(string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Batu":
                    stone_count += 1;
                    break;

                

                case "Getah":
                    Getah_count += 1;
                    break;

                case "Ranting":
                    Ranting_count += 1;
                    break;

            }
        }


        //Pedang

        PedangReq1.text = "3 Batu[" + stone_count + "]";
        PedangReq2.text = "3 Ranting[" + Ranting_count + "]";

        if(stone_count >= 3 && Ranting_count >= 3)
        {
            CraftPedangBTN.gameObject.SetActive(true);
        }
        else
        {
            CraftPedangBTN.gameObject.SetActive(false);
        }


        //obor
        OborReq1.text = "2 Getah[" + Getah_count + "]";
        OborReq2.text = "1 Ranting[" + Ranting_count + "]";

        if (Getah_count >= 2 && Ranting_count >= 1)
        {
            CraftOborBTN.gameObject.SetActive(true);
        }
        else
        {
            CraftOborBTN.gameObject.SetActive(false);
        }
    }
}
