using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void EasyPressed()
    {
        //здесь будет устанавливаться условия для лёгкого уровня сложнсти
        SceneManager.LoadScene("GameScene");
    }

    public void MiddlePressed()
    {
        //здесь будет устанавливаться условия для среднего уровня сложнсти
        SceneManager.LoadScene("GameScene");
    }

    public void HardPressed()
    {
        //здесь будет устанавливаться условия для тяжёлого уровня сложнсти
        SceneManager.LoadScene("GameScene");
    }
}
