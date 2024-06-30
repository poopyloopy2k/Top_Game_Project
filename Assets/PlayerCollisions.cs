using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    public int damageAmount = 20;
    private void OnCollisionEnter2D(Collision2D other)
    {
        // ������� ��������� ��� ������������
        Debug.Log("������������ �: " + other.transform.name);

        // �������� �� ��� "Enemy"
        if (other.transform.CompareTag("Enemy"))
        {
            // ��������� ���������� Player2DControl � ������ 
            Player2DControl player = GetComponent<Player2DControl>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
                StartCoroutine(GetHurt());
            }
        }
    }
    IEnumerator GetHurt()
    {
        Physics2D.IgnoreLayerCollision(6,8);
        GetComponent<Animator>().SetLayerWeight(1, 1);
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);
        Physics2D.IgnoreLayerCollision(6, 8,false);

    }
}
