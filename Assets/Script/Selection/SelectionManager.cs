using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set;}

    public bool onTarger;

    public GameObject selectedObjeck;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject interaction_Info_UI;
    Text interaction_text;

    public Image centerDotImage;
    public Image handIcon;

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
        onTarger = false;
    }


    

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;

            InteractableObject SemuaInteract = selectionTransform.GetComponent<InteractableObject>();

            if (SemuaInteract && SemuaInteract.PlayerInRange)
            {
                onTarger = true;
                selectedObjeck = SemuaInteract.gameObject;
                interaction_text.text = SemuaInteract.GetItemName();
                interaction_Info_UI.SetActive(true);


                if (SemuaInteract.CompareTag("pickable"))
                {
                    centerDotImage.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                }
                else
                {
                    handIcon.gameObject.SetActive(false);
                    centerDotImage.gameObject.SetActive(true) ;
                }
            }
            else
            {
                onTarger = false;
                interaction_Info_UI.SetActive(false);
                handIcon.gameObject.SetActive(false);
                centerDotImage.gameObject.SetActive(true);
            }

        }
        else
        {

            onTarger = false ;
            interaction_Info_UI.SetActive(false);
            handIcon.gameObject.SetActive(false);
            centerDotImage.gameObject.SetActive(true);
        }
    }

    public void DisableSelection()
    {
        handIcon.enabled = false;
        centerDotImage.enabled = false;
        interaction_Info_UI.SetActive(false) ;

        selectedObjeck = null;
    }

    public void EnableSelection()
    {
        handIcon.enabled = true;
        centerDotImage.enabled = true;
        interaction_Info_UI.SetActive(true ) ;
    }
}
