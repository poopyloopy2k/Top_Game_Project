using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void EasyPressed()
    {
        //����� ����� ��������������� ������� ��� ������ ������ ��������
        SceneManager.LoadScene("Game");
    }

    public void MiddlePressed()
    {
        //����� ����� ��������������� ������� ��� �������� ������ ��������
        SceneManager.LoadScene("Game");
    }

    public void HardPressed()
    {
        //����� ����� ��������������� ������� ��� ������� ������ ��������
        SceneManager.LoadScene("Game");
    }
}
