using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour
{
    public int targetScore = 8;
    public GameObject nextScene;
    public GameObject nextLevelObject;
    public GameObject winTextObject;

    private PlayerController countManager;

    void Start()
    {
        countManager = FindAnyObjectByType<PlayerController>();

        if (countManager != null)
        {
            countManager.nextScene = nextScene;
            countManager.nextLevelObject = nextLevelObject;
            countManager.winTextObject = winTextObject;

            int sceneIndex = SceneManager.GetActiveScene().buildIndex - 1; // -1 = excluding main menu
            if (sceneIndex >= 0)
            {
                countManager.targetScore = targetScore * (int)Mathf.Pow(2, sceneIndex); //pow calculates numbers raised to a power
            }
        }
    }
}
