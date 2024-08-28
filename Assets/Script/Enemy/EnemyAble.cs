using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class EnemyAble : MonoBehaviour
{
    public bool playerInRange;
    public bool CanAttack;

    public float EmyMaxHealth;
    public float emyHealth;

    private void Start()
    {

        emyHealth = EmyMaxHealth;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void GetHit()
    {
        emyHealth -= 1;



    }


    private void Update()
    {
        

        if (CanAttack)
        {
            GlobalState.Instance.resourceHealth = emyHealth;
        }
    }
}
