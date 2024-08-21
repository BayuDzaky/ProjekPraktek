using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public bool PlayerInRange;

    public string GetItemName()
    {
        return ItemName;
    }




    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && PlayerInRange && SelectionManager.Instance.onTarger && SelectionManager.Instance.selectedObjeck == gameObject)
        {
            if (!InventorySystem.Instance.CheckifFull())
            {
                
                InventorySystem.Instance.AddToInventory(ItemName);

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Fulll anjeng");
            }
            
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}
