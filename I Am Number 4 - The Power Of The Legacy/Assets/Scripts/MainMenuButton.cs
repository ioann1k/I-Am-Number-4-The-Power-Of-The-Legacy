using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    Button menuItem;
    Text menuItemText;

    private void Awake()
    {
        menuItem = GetComponent<Button>();
        menuItemText = menuItem.transform.GetChild(0).GetComponent<Text>();
    }

    public void OnMouseOver()
    {
        menuItemText.color = Color.white;
        menuItemText.fontSize = 120;
    }

    public void OnMouseExit()
    {
        Color32 greyColor = new Color32(152, 152, 152, 255);
        menuItemText.color = greyColor;
        menuItemText.fontSize = 80;
    }
}
