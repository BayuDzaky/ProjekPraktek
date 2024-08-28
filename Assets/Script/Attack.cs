using UnityEngine;


[RequireComponent (typeof(Animator))]
public class Attack : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && InventorySystem.Instance.isOpen == false && CraftingSystem.instance.isOpen == false)
        {
            animator.SetBool("isAttacking", true);

            GameObject selectedEmy = SelectionManager.Instance.selectedEnemy;

            if (selectedEmy != null)
            {
                selectedEmy.GetComponent<EnemyAble>().GetHit();
            }


        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
}
