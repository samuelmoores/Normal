using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cam;
    public float runSpeed;
    public float turnSpeed;
    Animator animator;
    CharacterController controller;
    PlayerAttack playerAttack;
    bool freeze = false;

    public static PlayerMovement Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // This gets called every time a new scene loads
        Debug.Log("scene loaded");
        transform.position = Vector3.zero;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();    
        controller = GetComponent<CharacterController>();
        playerAttack = GetComponent<PlayerAttack>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller.Move(Vector3.down * runSpeed * Time.deltaTime);

        Debug.Log("local: " + transform.localPosition);
        Debug.Log("global: " + transform.position);

        transform.position = Vector3.zero;

        UnFreeze();
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
        Debug.Log("-------------------------UNFREEZE----------------------");
        freeze = false;
        playerAttack.CanAttack();
    }

    public void TakeDamage(float damageAmount)
    {
        if(!freeze)
        {
            Freeze();
            Debug.Log("play damage animation");
            animator.SetTrigger("damage");
        }
    }
}
