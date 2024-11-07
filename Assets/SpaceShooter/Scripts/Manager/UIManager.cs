using System;
using System.Collections.Generic;
using SpaceShooter.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<Image> lifeIcons;
        [SerializeField] private TextMeshProUGUI scoreText;
        private int currentScore;


        public static UIManager Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            ResetScore();
        }

        private void ResetScore()
        {
            currentScore = 0;
            scoreText.text = currentScore.ToString();
        }

        public void RemoveLifeIcon(int currentPlayerLife)
        {
            lifeIcons[currentPlayerLife - 1].gameObject.SetActive(false);
        }

        public void AddToScore(int enemyPoint)
        {
            currentScore += enemyPoint;
            scoreText.text = currentScore.ToString();
        }
    }
}