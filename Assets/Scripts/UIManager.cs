using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Sprite[] _livesDisplay;
    [SerializeField] Image _livesImg;
    [SerializeField] Text _scoreText;
    [SerializeField] GameObject _gameOverText;
    [SerializeField] GameObject _restartText;
    bool _gameOver = false;

    void Start()
    {
        NewGameUI();
    }



    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        if (currentLives >= 0)
        {
            _livesImg.sprite = _livesDisplay[currentLives];
        }
        else if (currentLives < 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameOver = true;
        _gameOverText.SetActive(true);
        _restartText.SetActive(true);
        StartCoroutine(GameOverBlink());
    }

    IEnumerator GameOverBlink()
    {
        while (_gameOver == true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.SetActive(!_gameOverText.activeInHierarchy);
        }
    }

    public void NewGameUI()
    {
        _gameOver = false;
        _scoreText.text = "Score: " + 0;
        _gameOverText.SetActive(false);
        _restartText.SetActive(false);
    }
}
