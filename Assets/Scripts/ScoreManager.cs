using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    PlayerController healthUpdate;
    int Health;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        healthUpdate = GameObject.FindGameObjectWithTag("Character").GetComponent<PlayerController>();
        Health = healthUpdate.Health;
        healthText.text = "Health: " + Health.ToString();

    }

    public void HealthChange(int change)
    {
        Health += change;
        UpdateHealthUI();
    } 

    void UpdateHealthUI()
    {
        healthText.text = "Health: " + Health.ToString();

    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
