using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyMenu : MonoBehaviour
{
    public void EasyPressed()
    {
        //����� ����� ��������������� ������� ��� ������ ������ ��������
        SceneManager.LoadScene("GameScene");
    }

    public void MiddlePressed()
    {
        //����� ����� ��������������� ������� ��� �������� ������ ��������
        SceneManager.LoadScene("GameScene");
    }

    public void HardPressed()
    {
        //����� ����� ��������������� ������� ��� ������� ������ ��������
        SceneManager.LoadScene("GameScene");
    }
}
