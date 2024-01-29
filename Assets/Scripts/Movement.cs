using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed;
    float speedX, speedY;
    Rigidbody2D rb;


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    private void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedY = Input.GetAxisRaw("Vertical") * movementSpeed;
        
        Vector2 movement = new Vector2 (speedX, speedY);
        movement.Normalize();

        rb.velocity = movement * movementSpeed * Time.deltaTime;

        if(movement != Vector2.zero);
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 50, angle);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                movementSpeed = 1000;
            }   

            if(Input.GetKeyUp(KeyCode.Mouse1))
            {
                movementSpeed = 300;
            }             
    }

private void OnTriggerEnter2D(Collider2D collision){
    if (collision.gameObject.CompareTag("Oil")){
    transform.Rotate(Vector3.forward * Random.Range(54, 5000) * Time.deltaTime);
    }
  }
  }

    

