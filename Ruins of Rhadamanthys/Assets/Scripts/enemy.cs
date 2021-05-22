using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int damage)
    {
        animator.SetTrigger("isHurt");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Enemy killed: " + gameObject.name);

        // play death animation
        animator.SetBool("isDead", true);

        // disable enemy
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
