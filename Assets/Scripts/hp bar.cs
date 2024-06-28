using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpbar : MonoBehaviour
{
    Image hpBar;
    public float Maxhealth = 100f;
    public float HP;
    // Start is called before the first frame update
    void Start()
    {
       hpBar = GetComponent<Image>();
        HP = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = HP / Maxhealth;
    }
}
