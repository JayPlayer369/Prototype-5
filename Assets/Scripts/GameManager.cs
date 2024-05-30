using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public float spawnRate = 1.0f;
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton; 
    public GameObject titleScreen;


    public Slider volumerSlider;
    public AudioSource music;
    public TextMeshProUGUI livesText;
    private int lives;
    public GameObject pauseScreen;
    private bool paused;

    void Start()
    {
        volumerSlider.onValueChanged.AddListener(delegate {ValueChangeCheck(); });
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        music.volume = volumerSlider.value;
        Debug.Log(volumerSlider.value);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    IEnumerator SpawnTarget()
    {   
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

            
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        isGameActive = true;
       StartCoroutine(SpawnTarget());
       score = 0;
       UpdateScore(0);
       UpdateLives(3);
       spawnRate /= difficulty;

    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
