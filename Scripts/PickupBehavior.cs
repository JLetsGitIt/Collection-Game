using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupBehavior : MonoBehaviour
{
    public static int pickupCount = 0;
    public int scoreValue = 1;
    public AudioClip pickupSFX;
    public Text scoreText;
    public static int score = 0;
    float firstHalf;
    float timer;
    //public GameObject shockwave;

    // Start is called before the first frame update
    void Start()
    {
        pickupCount++;
        Debug.Log("Pickup count: " + pickupCount);
        score = 0;
        timer = 0;
        firstHalf = LevelManager.duration / 2;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        scoreText.text = score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //gameObject.GetComponent<Animator>().SetTrigger("pickupDestroyed");
            AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
            //Instantiate(shockwave, transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f); //2
        }
    }

    private void OnDestroy()
    {
        if(!LevelManager.isGameOver)
        {
            score += scoreValue;
            pickupCount--;
            if(timer <= firstHalf)
            {
                score += scoreValue;
            }
            //Debug.Log("score: " + score + " time " + timer);
            if (pickupCount <= 0)
            {
                //Debug.Log("You Win!");
                scoreText.text = PickupBehavior.score.ToString();
                LevelManager lm = FindObjectOfType<LevelManager>();
                lm.LevelBeat();
            }
        }
        
    }
}
