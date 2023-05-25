using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1;
    public AudioClip enemySFX;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!LevelManager.isGameOver)
        {
            transform.LookAt(player);
            if(gameObject.name == "EyeBall")
            {
                Quaternion q = Quaternion.AngleAxis(270, Vector3.up);
                transform.rotation *= q;
            }
            transform.position = Vector3.MoveTowards
                (transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        if(collision.gameObject.CompareTag("Enemy"))
        {
            // enemy kill each other
            AudioSource.PlayClipAtPoint(enemySFX, Camera.main.transform.position);
            gameObject.GetComponent<Animator>().SetTrigger("enemyDestroyed");
            Destroy(gameObject, 1);
        }
    }
}
