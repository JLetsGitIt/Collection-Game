using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBlocks : MonoBehaviour
{
    public Color startColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.Sin(Time.time * 3);
        t += 1; // range between 0 and 2
        t /= 2; // clamp between 0 and 1
        GetComponent<Renderer>().material.color = Color.Lerp(startColor, endColor, t);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Vector3 direction = collision.gameObject.transform.position;
            direction.y = 0;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * 3, ForceMode.Impulse);
        }
    }
}
