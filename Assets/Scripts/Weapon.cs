using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject item;
    [SerializeField] private GameObject bulletPrefab; // ������ ����
    [SerializeField] private Transform firePoint; // ����� ��������
    [SerializeField] private float bulletSpeed = 10f; // �������� ����

    void Update()
    {
        RotateWeapon();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
    }

    void RotateWeapon()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
