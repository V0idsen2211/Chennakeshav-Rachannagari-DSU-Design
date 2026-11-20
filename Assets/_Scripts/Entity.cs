using UnityEngine;

public class Entity : MonoBehaviour, IDamageable
{
    public int health;

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
    
    
}
