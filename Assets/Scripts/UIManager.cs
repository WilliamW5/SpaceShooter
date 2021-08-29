using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Handle to Text
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesImg; 
    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartGameText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Assign text component to handle
        _scoreText.text = "Score: " + 0;
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL.");
        }
    }

   public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverDisplay();
        }
    }

   private void GameOverDisplay()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        StartCoroutine(FlashingGameOverText());
        _restartGameText.gameObject.SetActive(true);
    }

    IEnumerator FlashingGameOverText()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER!";
            yield return new WaitForSeconds(1.0f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

}
