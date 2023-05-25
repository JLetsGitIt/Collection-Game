using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float levelDuration = 10.0f;
    public static float duration;
    public Text timerText;
    public Text gameText;
    //public Text scoreText;
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    public static bool isGameOver = false;
    public string nextLevel;
    float countDown;

    // Start is called before the first frame update
    void Start()
    {
        duration = levelDuration;
        isGameOver = false;
        countDown = levelDuration;
        SetTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            if(countDown > 0)
            {
                countDown -= Time.deltaTime;
            }
            else 
            {
                countDown = 0.0f;
                LevelLost();
            }
            SetTimerText();
        }
    }

    void SetTimerText()
    {
        timerText.text = countDown.ToString("f2");
    }

    public void LevelLost()
    {
        isGameOver = true;
        timerText.text = "0.00";
        gameText.gameObject.SetActive(true);
        gameText.text = "GAME OVER!";
        //Camera.main.GetComponent<AudioSource>().pitch = 1;
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);
        Invoke("LoadCurrentLevel", 2);
    }

    public void LevelBeat()
    {
        isGameOver = true;
        gameText.gameObject.SetActive(true);
        gameText.text = "YOU WIN!";
        //Camera.main.GetComponent<AudioSource>().pitch = 2f;
        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);
        if(!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        } else 
        {
            gameText.text = "VICTORY!";
        }
    }

    void LoadNextLevel()
    {
        PickupBehavior.pickupCount = 0;
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        PickupBehavior.pickupCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
