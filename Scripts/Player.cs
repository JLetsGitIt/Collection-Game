using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10f;
    public Transform ground;
    Rigidbody rb;
    AudioSource jumpSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jumpSFX = GetComponent<AudioSource>();
    }
    void Update()
    {
        if(transform.position.y < 0)
        {
            FindObjectOfType<LevelManager>().LevelLost();
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if(!LevelManager.isGameOver)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 forceVector = new Vector3(moveHorizontal, 0.0f, moveVertical);
            if(Input.GetKey(KeyCode.LeftShift))
            {
                rb.AddForce(forceVector * speed * 2);
            } else {
                rb.AddForce(forceVector * speed);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if(transform.position.y < 1.1)
                {
                    rb.AddForce(0, 5, 0, ForceMode.Impulse);
                    jumpSFX.Play();
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.LevelLost();
            Destroy(gameObject);
        }
    }

}
