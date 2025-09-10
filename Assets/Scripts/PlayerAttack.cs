using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInventory inventory;
    List<GameObject> weapons;
    Animator animator;
    bool hasWeapon = false;
    bool attacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        weapons = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && hasWeapon && !attacking)
        {
            animator.SetTrigger("attack");
            attacking = true;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && weapons.Count > 0)
        {
            inventory.SetActiveItem(0);
            animator.SetBool("hasSword", false);
            weapons[0].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 0)
        {
            inventory.SetActiveItem(1);
            animator.SetBool("hasSword", true);
            weapons[0].SetActive(true);
        }
    }

    public void PickupWeapon(GameObject newWeapon)
    {
        hasWeapon = true;
        weapons.Add(newWeapon);
    }

    public void DropWeapon()
    {
        hasWeapon = false;
    }

    public void CanAttack()
    {
        attacking = false;
    }
}
