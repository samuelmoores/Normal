using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public string Name;
    public Transform demonPit;
    public Transform startingPos;
    public Spawner spawner;
    Animator animator;
    NavMeshAgent agent;
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
                spellCasted = false;
                goingHome = false;
            }
        }

        if(agent.velocity.magnitude > 0.0f && !spellCasted && !moving && !goingHome)
        {
            if (agent.remainingDistance < agent.stoppingDistance)
            {
                Debug.Log("spellCasted set to true");
                spellCasted = true;
                animator.SetTrigger("spell");
                animator.SetBool("walk", false);
                spawner.Spawn();
            }
        }
        else if(agent.velocity.magnitude > 0.0f && agent.remainingDistance < agent.stoppingDistance)
        {
            animator.SetBool("walk", false);
            moving = false;
        }

        if(agent.isStopped && goingHome)
        {
                if(Input.GetKeyDown(KeyCode.E) && found)
                {
                    NPC_UI.Instance.Hide();
                    PlayerMovement.Instance.UnFreeze();
                    agent.destination = demonPit.position;
                    animator.SetBool("walk", true);
                    moving = true;
                    goingHome = false; //pathfinder (D&D inspired game like bauldors gate
            }
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
        if(other.CompareTag("Player") && (!moving))// || goingHome))
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
        Debug.Log("Go Home");
        agent.destination = startingPos.position;
        animator.SetBool("walk", true);
        goingHome = true;
        spellCasted = false;
    }


}
