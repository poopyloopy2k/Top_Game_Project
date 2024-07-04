using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public GameManager gameManager;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(RestartGame);
    }

    private void RestartGame()
    {
        gameManager.RestartGame();
    }
}