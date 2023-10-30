using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject titleScreen;

    private void Start()
    {
        titleScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            titleScreen.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
