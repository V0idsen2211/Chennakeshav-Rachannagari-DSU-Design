using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 size;

    public LayerMask collidable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] cols = Physics2D.OverlapBoxAll(transform.position, size, 0, collidable);
        if (cols.Length > 0)
        {
            foreach (Collider2D c in cols)
            {
                if (c.GetComponent<Entity>() != null)
                {
                    c.GetComponent<Entity>().TakeDamage(1);
                }
            }
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size/2);
    }
}
