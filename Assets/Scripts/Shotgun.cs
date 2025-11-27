using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shotgun : MonoBehaviour
{
    public GunSystem shotgun;

    public Image image;
    public TextMeshProUGUI tmPro;

    public Sprite slug;
    public Sprite spread;
    
    int _originalDmg;

    private void Start()
    {
        _originalDmg = shotgun.dmg;
        image.sprite = spread;
        tmPro.text = "Spread Shot";
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            changeStats();
        }
    }

    private void changeStats()
    {
        if(shotgun.dmg == _originalDmg)
        {
            shotgun.dmg = 300;
            shotgun.spread = 0;
            shotgun.bulletsPerTap = 1;
            image.sprite = slug;
            tmPro.text = "Slug Shot";
        }
        else
        {
            shotgun.dmg = 25;
            shotgun.spread = 0.06f;
            shotgun.bulletsPerTap = 10;
            image.sprite = spread;
            tmPro.text = "Spread Shot";
        }
    }
}
