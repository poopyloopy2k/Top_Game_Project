using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void EasyPressed()
    {
        SceneManager.LoadScene(1);
        Application.LoadLevel(1);
    }

    public void MiddlePressed()
    {
        SceneManager.LoadScene(1);
        Application.LoadLevel(1);
    }

    public void HardPressed()
    {
        SceneManager.LoadScene(1);
        Application.LoadLevel(1);
    }
}
