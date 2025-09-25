using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public string Name;
    public Transform demonPit;
    public Transform startingPos;
    Animator animator;
    NavMeshAgent agent;
    public BreadWinnder breadWinnnder;
    bool found = false;
    bool dialogue = false;
    bool spellCasted = false;
    bool moving = false;
    bool goingHome = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && found && !moving)
        {
            if (!dialogue)
                StartDialogue();
            else
            {
                NPC_UI.Instance.Hide();
                animator.SetBool("dialogue", false);
                PlayerMovement.Instance.UnFreeze();
                agent.destination = demonPit.position;
                animator.SetBool("walk", true);
                moving = true;
            }
        }

        if(agent.velocity.magnitude > 0.0f && !spellCasted)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                spellCasted = true;
                breadWinnnder.InitTheBreadWinder();
                animator.SetTrigger("spell");
                animator.SetBool("walk", false);
            }
        }
        else if(agent.velocity.magnitude > 0.0f && agent.remainingDistance < agent.stoppingDistance)
        {
            animator.SetBool("walk", false);
        }

        if(goingHome)
        {
            NPC_UI.Instance.SetNPCText("Return to the mountatins?");

            if(Input.GetKeyDown(KeyCode.E) && found)
                SceneManager.LoadScene(0);

        }
    }

    private void StartDialogue()
    {
        dialogue = true;
        NPC_UI.Instance.SetNPCText("Would you like me to show you something?");
        animator.SetBool("dialogue", true);
        PlayerMovement.Instance.Freeze();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && (!moving || goingHome))
        {
            NPC_UI.Instance.Show();
            NPC_UI.Instance.SetNPCText(Name);
            found = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NPC_UI.Instance.Hide();
            found = false;
        }
    }

    public void ReturnHome()
    {
        agent.destination = startingPos.position;
        animator.SetBool("walk", true);
        goingHome = true;
    }


}
