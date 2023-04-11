using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button button1;
    public Button button2;

    void Start()
    {
        button1.onClick.AddListener(EnableButton2);
        button2.onClick.AddListener(DisableButton1);
    }

    void EnableButton2()
    {
        button2.interactable = true;
        button1.interactable = false;
    }

    void DisableButton1()
    {
        button1.interactable = true;
        button2.interactable = false;
    }
}
