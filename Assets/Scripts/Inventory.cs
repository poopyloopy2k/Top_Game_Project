using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons; // Список всех оружий
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject sword; // Меч
    [SerializeField] public int hotWeapon = -1; // Индекс активного оружия (меч всегда доступен под индексом -1)
    [SerializeField] private int maxWeapon = 2; // Максимальное количество оружий кроме меча

    private void Start()
    {
        if (sword != null)
        {
            weapons.Insert(0, sword);
            sword.SetActive(true);
        }
    }

    private void Update()
    {
        int scroll = Convert.ToInt32(Input.GetAxisRaw("Mouse ScrollWheel") * 10);
        if (weapons.Count > 0 && scroll != 0)
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
    }

    void SetHotWeapon(int hot)
    {
        hotWeapon = hot;
        foreach (GameObject g in weapons)
        {
            g.SetActive(false);
        }
        if (hotWeapon == -1)
        {
            sword.SetActive(true);
        }
        else if (weapons.Count > 0)
        {
            weapons[hotWeapon].SetActive(true);
        }
    }

    public void AddWeapon(GameObject weapon)
    {
        if (weapons.Count < maxWeapon)
        {
            GameObject w = Instantiate(weapon, hand);
            w.GetComponent<Weapon>().isPlayer = true;
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
