using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    bool hasWeapon = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && hasWeapon)
        {
            animator.SetTrigger("attack");
        }
    }

    public void PickupWeapon()
    {
        hasWeapon = true;
    }

    public void DropWeapon()
    {
        hasWeapon = false;
    }
}
