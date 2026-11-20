using UnityEngine;
using UnityEngine.SceneManagement;

public class SlimeEnemy : Entity
{
    public float detectionRange;
    public Vector2 jumpForce;
    public Animator anim;
    Vector2 playerPos;
    public float jumpTimer;
    float startJumpTimer;
    Rigidbody2D rb;
    
    bool isGrounded;
    public float checkRadius;
    public Transform feet;
    public LayerMask ground;
    private InputSystem_Actions actions;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startJumpTimer = jumpTimer;
        rb = GetComponent<Rigidbody2D>();
        actions = new InputSystem_Actions();

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feet.position, checkRadius, ground);
        playerPos = PlayerMovement.Instance.transform.position;

        if(Vector2.Distance(transform.position, playerPos) < detectionRange)
        {
            if (startJumpTimer <= 0 && isGrounded)
            {
                Jump();
            }
            else if(isGrounded)
            {
                startJumpTimer -= Time.deltaTime;
            }

            
        }

        SetSprite();


    }
    int DirectionToPlayer()
    {
        int dir = 0;
        return dir = playerPos.x > transform.position.x ? -1 : 1;
    }
    public void SetSprite()
    {
        if (!isGrounded)
        {
            anim.CrossFade("Jump", 0f);

        }
        else
        {
            anim.CrossFade("Idle", 0f);
        }
    }

    void Jump()
    {
        startJumpTimer = jumpTimer;
        rb.linearVelocity = new Vector2(-DirectionToPlayer() * jumpForce.x, jumpForce.y);
        


    }

    public override void Die()
    {
        EnemyManager.Instance.enemyCount--;
        base.Die();
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(feet.position, checkRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            actions.Disable();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
