using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;


    [SerializeField] float jumpForce = 500f;
    [SerializeField] int jumpLength = 10;
    [SerializeField] int jumpWait = 10;

    float constJumpForce;
    int constJumpLength;
    int constJumpWait;

    void Start()
    {
        constJumpForce = jumpForce;
        constJumpLength = jumpLength;
        constJumpWait = jumpWait;

    }

    // Update is called once per frame

    bool jumpB = false;
    Rigidbody2D rb;

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpB = true;
            //Debug.Log("a");
        }

        rb = GetComponent<Rigidbody2D>();
    }



    bool jump = false;

    private void FixedUpdate()
    {
        float currentY = transform.position.y;
        float currentX = transform.position.x;
        transform.position = new Vector2(currentX - Input.GetAxis("Horizontal") * Time.deltaTime * speed * -1, currentY);


        

        if (jump && jumpWait <= 0)
        {
            jumpB = false;
            
            if (jumpLength > 0)
            {
                rb.velocity = new Vector2(0.0f, 0.0f);
                Physics2D.gravity = new Vector2(0, 0.0f);
                rb.AddForce(new Vector2(0f, jumpForce));
                
                jumpLength--;
            } else
            {
                jumpLength = constJumpLength;
                jumpWait = constJumpWait;
                jump = false;
                jumpB = false;
            }

        } else
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
            jumpWait--;
        }

    }

    void OnCollisionStay2D(Collision2D col)
    {

        if (col.gameObject.tag == "Ground")
        {

            if (jumpB)
            {
                jump = true;
            }

        }

    }
}
