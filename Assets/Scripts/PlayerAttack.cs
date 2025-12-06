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

        //selecting weapons
        if(weapons.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || GameManager.Instance.HasWon())
            {
                hasWeapon = false;
                HoldWeapon(-1); //1 is no weapon
                return;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                hasWeapon = true;
                HoldWeapon(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                hasWeapon = true;
                HoldWeapon(1);
            }
        }

    }

    public void HoldWeapon(int weaponsIndex)
    {
        if (weaponsIndex >= weapons.Count) // pressing a key with no weapon there
            return;

        inventory.SetActiveItem(weaponsIndex + 1); //first image in inventory is no weapon
        
        if(weaponsIndex == -1) //put away all weapons
        {
            SetWeaponAnim(0);
            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].SetActive(false);
            }
            return;
        }

        Pickup weapon = weapons[weaponsIndex].GetComponent<Pickup>();
        weapons[weaponsIndex].SetActive(true);
        SetWeaponAnim(weapon.GetWeaponNumber()); //each weapon has a number that corresponds to it's first animation in a list

        for (int i = 0; i < weapons.Count; i++)
        {
            if (i != weaponsIndex) 
                weapons[i].SetActive(false); // hide the prev weapon
        }
    }
        
    public void PickupWeapon(GameObject newWeapon, int weaponNumber)
    {
        hasWeapon = true;
        weapons.Add(newWeapon);
        HoldWeapon(weapons.Count - 1);
    }

    private void SetWeaponAnim(int weaponNumber)
    {
        int animIndex = 0;

        if(weaponNumber != 0)
            animIndex = ((weaponNumber - 1) * 3) + 2;

        overideController = new AnimatorOverrideController(animator.runtimeAnimatorController);

        foreach (var anim in overideController.animationClips)
        {
            if (anim.name.Contains("Idle"))
            {
                overideController[anim] = weaponAnimationClips[animIndex];
            }

            if (anim.name.Contains("Run"))
            {
                overideController[anim] = weaponAnimationClips[animIndex + 1];
            }

            if (anim.name.Contains("Attack") && animIndex != 0)
            {
                overideController[anim] = weaponAnimationClips[animIndex + 2];
            }
        }

        animator.runtimeAnimatorController = overideController;
    }

    public void DropWeapon()
    {
        hasWeapon = false;
    }

    public void CanAttack()
    {
        attacking = false;
    }

    public void Attack()
    {
        Sword sword = weapons[0].GetComponent<Sword>();

        if (sword)
        {
            sword.Attack();
        }
    }

    public void EndAttack()
    {
        Sword sword = weapons[0].GetComponent<Sword>();

        if (sword)
        {
            sword.EndAttack();
        }
    }
}
