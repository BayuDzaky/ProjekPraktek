using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceHealthEmy : MonoBehaviour
{
    public Slider slider;

    private float currentHealth, MaxHealth;


    public GameObject GlobalState;

    private void Awake()
    {
        slider = GetComponent<Slider>(); 
    }


    private void Update()
    {
        currentHealth = GlobalState.GetComponent<GlobalState>().resourceHealth;
        MaxHealth = GlobalState.GetComponent<GlobalState>().resourceHealthMax;


        float fillValue = currentHealth / MaxHealth;
        slider.value = fillValue;
    }
}
