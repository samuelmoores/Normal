using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BreadWinnder : MonoBehaviour
{
    public GameObject Mesh;
    public ParticleSystem ps;
    GameObject player;
    Animator animator;
    NavMeshAgent agent;
    bool dealDamage = false;
    int damage = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = PlayerMovement.Instance.gameObject;

        gameObject.SetActive(true);
        agent.isStopped = true;
        ps.Play();
        Mesh.gameObject.SetActive(false);
        GetComponent<SphereCollider>().enabled = false;
        StartCoroutine(Show(6.0f));
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 2.0f && !agent.isStopped)
        {
            animator.SetBool("run", true);
            agent.destination = player.transform.position;
        }
        else
            animator.SetBool("run", false);
    }

    public void ReturnToIdleOrRun()
    {
        agent.isStopped = false;
    }

    private void Attack()
    {
        animator.SetTrigger("swipe");
        agent.isStopped = true;
    }

    public void Damage()
    {
        agent.isStopped = true;

        if (damage++ < 5)
        {
           animator.SetTrigger("damage");

        }
        else
        {
            GameManager.Instance.KillZombie();
            animator.SetBool("dead", true);
        }
    }

    public void DealDamage(int isDealingDamage)
    {
        if (isDealingDamage == 0)
            dealDamage = false;
        else
        {
            dealDamage = true;
            agent.isStopped = true;
        }
    }

    public bool IsDealingDamage()
    {
        Debug.Log("is dealing damage is: " + dealDamage);
        return dealDamage;
    }

    IEnumerator Show(float duration)
    {
        float current = 0.0f;

        while(current < duration)
        {
            current += Time.deltaTime;
            yield return null;
        }

        Mesh.gameObject.SetActive(true);
        GetComponent<SphereCollider>().enabled = true;
        agent.isStopped = true;
        ps.Stop();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Attack();
        }
    }
}
