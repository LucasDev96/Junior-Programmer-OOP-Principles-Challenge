using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _livesText;

    [Header("Variables")]
    [SerializeField] private int _startingLives;
    private int _currentLives;
    private int _score = 0;
    [SerializeField] private string _scoreString;
    [SerializeField] public string _livesString;

    // Start is called before the first frame update
    void Start()
    {
        _currentLives = _startingLives;
        SetScoreText(_score);
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
}
