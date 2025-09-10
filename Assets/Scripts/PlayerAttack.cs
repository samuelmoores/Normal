using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInventory inventory;
    public List<AnimationClip> weaponAnimationClips;

    List<GameObject> weapons;
    Animator animator;
    AnimatorOverrideController overideController;
    bool hasWeapon = false;
    bool attacking = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        weapons = new List<GameObject>();

        overideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
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
            animator.SetBool("hasWeapon", false);
            weapons[0].SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 0)
        {
            inventory.SetActiveItem(1);
            animator.SetBool("hasWeapon", true);
            weapons[0].SetActive(true);
        }
    }

    public void PickupWeapon(GameObject newWeapon, int weaponNumber)
    {
        hasWeapon = true;
        weaponNumber *= 3;
        
        foreach (var anim in overideController.animationClips)
        {

            if(anim.name.Contains("WeaponIdle"))
            {
                Debug.Log("replace animation clip number: " + weaponNumber);
                overideController[anim] = weaponAnimationClips[weaponNumber];
            }

            if (anim.name.Contains("WeaponRun"))
            {
                overideController[anim] = weaponAnimationClips[weaponNumber + 1];
            }

            if (anim.name.Contains("WeaponAttack"))
            {
                overideController[anim] = weaponAnimationClips[weaponNumber + 2];
            }

            animator.runtimeAnimatorController = overideController;
        }
        

        animator.SetBool("hasWeapon", true);
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
