using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("movement")]
    [SerializeField] float speed = 5;

    [Header("animations")]
    public Animator animator;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 20;

    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    bool jump = false;
    bool touchGround = false;

    float constJumpForce;
    int constJumpLength;
    int constJumpWait;

    void Start()
    {
        constJumpForce = jumpForce;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame


    Rigidbody2D rb;


    void Update()
    {

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }



        // sword
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }


        // Apsisuko pacanas
        if (Input.GetAxis("Horizontal") >= 0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (Input.GetAxis("Horizontal") <= -0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }





    private void FixedUpdate()
    {
        float currentY = transform.position.y;
        float currentX = transform.position.x;
        transform.position = new Vector2(currentX - Input.GetAxis("Horizontal") * Time.deltaTime * speed * -1, currentY);

        //OnCollisionStay2D();

        if (jump && touchGround)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        jump = false;
        //touchGround = false;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }


    }

    void Attack()
    {
        // play animation
        animator.SetTrigger("Attack");

        // check if there are enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<enemy>().takeDamage(attackDamage);
            Debug.Log("we hit: " + enemy.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    /*void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Debug.Log("zeme");
            touchGround = true;
        }

    }*/

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            touchGround = true;
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            touchGround = false;
        }
    }
}
