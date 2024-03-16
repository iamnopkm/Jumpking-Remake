using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingInMenu : MonoBehaviour
{
    [SerializeField] public GameObject settingMenu;

    private static bool onSetting = false;

    public void Setting()
    {
        if (onSetting == true)
        {
            settingMenu.SetActive(false);
            onSetting = false;
        }
        else
        {
            settingMenu.SetActive(true);
            onSetting = true;
        }
    }
}

