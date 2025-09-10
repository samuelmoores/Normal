using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cam;
    public float runSpeed;
    public float turnSpeed;
    Animator animator;
    CharacterController controller;
    PlayerAttack playerAttack;
    bool freeze = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();    
        controller = GetComponent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller.Move(Vector3.down * runSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical);
        
        if(moveDirection != Vector3.zero && CanMove())
        {
            moveDirection = Quaternion.AngleAxis(cam.transform.rotation.eulerAngles.y, Vector3.up) * moveDirection;
            Quaternion toRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0.0f, moveDirection.z));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnSpeed * 100.0f * Time.deltaTime);
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        moveDirection.y = -9.8f;

        moveDirection.Normalize();

        if(CanMove())
            controller.Move(moveDirection.normalized * runSpeed * Time.deltaTime);
    }

    bool CanMove()
    {
        return controller.isGrounded && !freeze;
    }

    public void Freeze()
    {
        freeze = true;
    }

    public void UnFreeze()
    {
        freeze = false;
        playerAttack.CanAttack();
    }
}
