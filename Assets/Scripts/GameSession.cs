using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10.0f)][SerializeField] float gameSpeed = 1.0f;

    [SerializeField] int score = 0;
    [SerializeField] int pointsPerBlock = 5;

    [SerializeField] TMPro.TMP_Text scoreText;

    [SerializeField] bool isAutoPlayEnabled = false;

    private void Awake()
    {
        int count = FindObjectsOfType<GameSession>().Length;
        if (count > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    internal void UpdateScore()
    {
        score += pointsPerBlock;
        scoreText.text = score.ToString();
    }

    public void ClearScore()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return this.isAutoPlayEnabled;
    }
}
