using UnityEngine;

public class Sword : MonoBehaviour
{
    bool inflictDamage = false;

    public void Attack()
    {
        inflictDamage = true;
    }

    public void EndAttack()
    {
        inflictDamage = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && inflictDamage)
        {
            inflictDamage = false;
            BreadWinnder bw = other.gameObject.GetComponent<BreadWinnder>();
            bw.Damage();
        }
    }
}
