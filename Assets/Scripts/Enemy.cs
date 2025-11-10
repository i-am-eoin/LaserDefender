using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100;

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        DamageDealer damageDealer = otherObject.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
    }
    

}
