using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform shootPoint;

    public GameObject projectile;

    public float fireRate, projectileSpeed;

    private float fireTimer;

    private InputSystem_Actions inputActions;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.Attack.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer <= 0 && inputActions.Player.Attack.triggered)
        {
            
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    void Shoot()
    {
       GameObject p = Instantiate(projectile, shootPoint.position, Quaternion.identity);
       Rigidbody2D prb = p.GetComponent<Rigidbody2D>();
       prb.linearVelocity = shootPoint.up * projectileSpeed;
    }
}
