using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currentY = transform.position.y;
        float currentX = transform.position.x;
        transform.position = new Vector2(currentX - Input.GetAxis("Horizontal") * Time.deltaTime * speed * -1, currentY);
    }
}
