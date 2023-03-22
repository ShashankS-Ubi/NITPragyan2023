using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _numberOfLives = 3;
    [SerializeField]
    private GameObject _gameEndPopup = null;
    [SerializeField]
    private Text _livesText = null;
    [SerializeField]
    private Text _scoreText = null;
    [SerializeField]
    private Text _finalScoreText = null;
    [SerializeField]
    private ParticleSystem _shipExplosionVFX = null;

    private static GameManager _instance = null;

    public static GameManager Instance{
        get
        {
            return _instance;
        }
    }

    public int Score { get; private set; }
    public int Lives { get; private set; }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Score = 0;
        Lives = _numberOfLives;
        _scoreText.text = "Score: " + Score.ToString();
        _livesText.text = "Lives: " + Lives.ToString();

        _gameEndPopup.SetActive(false);
        _shipExplosionVFX.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void SetScore(int score)
    {
        Score = score;
        _scoreText.text = "Score: " + Score.ToString();
    }

    public void SetLives(int lives)
    {
        Lives = lives;
        _livesText.text = "Lives: " + Lives.ToString();

        if(Lives <= 0)
        {
            _finalScoreText.text = "Score: " + Score.ToString();
            _gameEndPopup.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void PlayExplosionVFX(Vector3 pos)
    {
        _shipExplosionVFX.gameObject.SetActive(true);
        _shipExplosionVFX.transform.position = pos;
        _shipExplosionVFX.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
