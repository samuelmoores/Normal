using UnityEngine;

public class BreadWinnerDamageBox : MonoBehaviour
{
    public BreadWinnder bw;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && bw.IsDealingDamage())
        {
            Debug.Log("Damage box trigger entered player while dealing damage");
            PlayerMovement.Instance.TakeDamage(1.0f);
            bw.DealDamage(0);
        }
    }
}
