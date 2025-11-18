using UnityEngine;

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
            countManager.SetTargetScore(targetScore);
        }
    }
}
