using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public float widthOfHealthBar;
    public RectTransform width; // With respect to screen!

    private void Awake()
    {
        widthOfHealthBar = width.sizeDelta.x;
    }

    // Get the health bar functionally working: https://findnerd.com/list/view/Set-RectTransform-Right-Left-Top-and-Bottom-value-via-script/18939/
    public void AlterHealthBar(float aValue)
    {
        float newValue = GameManager.healthBar.GetComponent<RectTransform>().offsetMax.x + (aValue * -1);  // Right side of health bar
        GameManager.healthBar.GetComponent<RectTransform>().offsetMax = new Vector2(newValue, 40);
        widthOfHealthBar -= aValue;
    }

    public void ResetHealthBar()
    {
        GameManager.healthBar.GetComponent<RectTransform>().offsetMax = new Vector2(0, 40);
        widthOfHealthBar = width.sizeDelta.x;
    }
}
