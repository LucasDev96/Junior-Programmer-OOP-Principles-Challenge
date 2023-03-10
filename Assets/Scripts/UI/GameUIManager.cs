using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : UIManager
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _gamePauseScreen;

    [Header("Variables")]
    [SerializeField] private int _startingLives;
    [SerializeField] private string _scoreString;
    [SerializeField] public string _livesString;

    private int _currentLives;
    private int _score = 0;
    public bool isGameActive { get; private set; }
    public bool isGamePaused { get; private set; }
    public static GameUIManager Instance { get; private set; }
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetStartingVariables();
        SetScoreText(_score);
        SetAudioSource();
    }

    private void Update()
    {
        TogglePause();
        GameOver();
    }

    // Set the score text, adding how many points have been gained
    public void SetScoreText(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = _scoreString + _score;
    }

    // Set the lives text, removing one life when it gets called
    public void SetLivesText()
    {
        _currentLives--;
        _livesText.text = _livesString + _currentLives;
    }

    // Set a few variables on game start to ensure proper values
    private void SetStartingVariables()
    {
        Time.timeScale = 1;
        _currentLives = _startingLives;
        isGameActive = true;
        isGamePaused = false;
        _gameOverScreen.SetActive(false);
        _gamePauseScreen.SetActive(false);
    }

    // Pause the game if it's not already paused
    private void PauseGame()
    {
        Debug.Log("game should be paused");
        isGamePaused = true;
        _gamePauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    // Unpause the game if it's paused
    public void UnpauseGame()
    {
        Debug.Log("game should be unpaused");
        isGamePaused = false;
        _gamePauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    // Toggle pause when the escape key is pressed
    private void TogglePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused && isGameActive)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isGamePaused)
        {
            UnpauseGame();
        }
    }

    // Triggers the game to stop when a game over occurs
    public void GameOver()
    {
        if (_currentLives <= 0 && isGameActive)
        {
            Time.timeScale = 0;
            _gameOverScreen.SetActive(true);
            isGameActive = false;
            PlayGameOverSound();
        }
    }

    // Sends the player back to the main menu screen
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(MAINMENU);
    }

    // Reload the sceen once the restart button is pressed
    public void RestartGame()
    {
        SceneManager.LoadScene(MAINGAME);
    }

    void SetAudioSource() { _audioSource = gameObject.GetComponent<AudioSource>(); }

    // Play game over sound effect
    void PlayGameOverSound() { _audioSource.Play(); }
}
