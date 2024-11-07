using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter.Manager
{
    public class SceneManagement : MonoBehaviour
    {
        public static SceneManagement Instance;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void LoadSameScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }
    }
}