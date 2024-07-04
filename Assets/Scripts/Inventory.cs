using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons; // ������ ���� ������
    [SerializeField] private Transform hand; // ���� ���������, � ������� �������� ������
    [SerializeField] private GameObject sword; // ���
    [SerializeField] private GameObject pistol; // ��������
    [SerializeField] public int hotWeapon = -1; // ������ ��������� ������ (��� ������ �������� ��� �������� -1)
    [SerializeField] private int maxWeapon = 2; // ������������ ���������� ������ ����� ����

    private void Start()
    {
        if (sword != null)
        {
            weapons.Insert(0, sword);
            sword.SetActive(true);
        }

        if (pistol != null)
        {
            weapons.Add(pistol);
            pistol.SetActive(false);
        }

        SetHotWeapon(hotWeapon);
    }

    private void Update()
    {
        HandleWeaponSwitching();
    }

    private void HandleWeaponSwitching()
    {
        // ������������ ������ � ������� �������� ����
        int scroll = Convert.ToInt32(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
        if (scroll != 0)
        {
            hotWeapon += scroll;
            if (hotWeapon > weapons.Count - 1)
            {
                hotWeapon = -1;
            }
            if (hotWeapon < -1)
            {
                hotWeapon = weapons.Count - 1;
            }
            SetHotWeapon(hotWeapon);
        }

        // ������������ ������ � ������� ������ 1 � 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetHotWeapon(-1); // ����� ����
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons.Count > 1)
            {
                SetHotWeapon(1); // ����� ������� ������ � ������ (������ ��� ������ ����������� ������)
            }
        }
    }

    private void SetHotWeapon(int hot)
    {
        hotWeapon = hot;
        foreach (GameObject g in weapons)
        {
            g.SetActive(false);
        }
        if (hotWeapon == -1)
        {
            if (sword != null)
            {
                sword.SetActive(true);
            }
        }
        else if (hotWeapon >= 0 && hotWeapon < weapons.Count)
        {
            weapons[hotWeapon].SetActive(true);
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        if (weapons.Count < maxWeapon + 1) // +1 ������ ��� ��� ������ ��������
        {
            GameObject w = Instantiate(weapon, hand);
            w.GetComponent<Weapon>();
            weapons.Add(w);
            SetHotWeapon(weapons.IndexOf(w));
        }
        else
        {
            RemoveWeapon(weapons[hotWeapon]);
            AddWeapon(weapon);
        }
    }

    public void RemoveWeapon(GameObject weapon)
    {
        var weaponComponent = weapon.GetComponent<Weapon>();
        if (weaponComponent != null)
        {
            Instantiate(weaponComponent.item, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            weapons.Remove(weapon);
            Destroy(weapon);
        }
    }
}
