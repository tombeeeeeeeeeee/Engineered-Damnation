using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestHUD : MonoBehaviour
{
    public TMP_Text money;
    DataManager dataManager;

    void Start()
    {
        dataManager = DataManager.dataManager;
    }

    void Update()
    {
        money.text = dataManager.money.ToString();

        if (Input.GetKeyDown(KeyCode.T))
        {
            dataManager.money++;
        }
    }
}
