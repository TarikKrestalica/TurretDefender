using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeValue;
    private float startDuration;
    [SerializeField] private float duration;
    // Start is called before the first frame update
    private void Awake()
    {
        startDuration = duration;
        SetCurrentDuration(startDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gameManager.IsPlaying())
            return;

        if (startDuration <= 0f)
        {
            return;
        }
        startDuration -= Time.deltaTime;
        float timeRounded = (float)Math.Round(startDuration, 2);  // Round the time to a certain number of decimal places: https://forum.unity.com/threads/rounding-to-2-decimal-places.211666/
        SetCurrentDuration(timeRounded);
    }

    public void SetCurrentDuration(float newTime)
    {
        timeValue.text = newTime.ToString();
    }

    public void SetStartingDuration(float time)
    {
        startDuration = time;
    }

    public float GetCurrentDuration()
    {
        return startDuration;
    }

    public float GetLimit()
    {
        return duration;
    }
}
