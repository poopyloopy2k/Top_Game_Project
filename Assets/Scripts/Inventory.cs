using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons; // Список всех оружий
    [SerializeField] private Transform hand; // Рука персонажа, к которой крепится оружие
    [SerializeField] private GameObject sword; // Меч
    [SerializeField] private GameObject pistol; // Пистолет
    [SerializeField] public int hotWeapon = -1; // Индекс активного оружия (меч всегда доступен под индексом -1)
    [SerializeField] private int maxWeapon = 2; // Максимальное количество оружий кроме меча

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
        // Переключение оружия с помощью колесика мыши
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

        // Переключение оружия с помощью клавиш 1 и 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetHotWeapon(-1); // Выбор меча
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (weapons.Count > 1)
            {
                SetHotWeapon(1); // Выбор второго оружия в списке (обычно это первое добавленное оружие)
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
        if (weapons.Count < maxWeapon + 1) // +1 потому что меч всегда доступен
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
