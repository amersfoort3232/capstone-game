using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// This Class send the Result Value to the TMP text
/// </summary>
public class Result : MonoBehaviour
{

    private TMP_Text _result;
    private int total;

    public int sceneIndex = 1;

    public bool KeepCounting { get; private set; }

    private void Awake()
    {
        KeepCounting = true;
        // cash the text component into _result variable
        _result = GetComponent<TMP_Text>();
    }

    public void PutValue(int value)
    {
        if (KeepCounting)
        {
            // add the value sent from other classes
            total += value;
            //right the Total value into text.
            _result.text = total.ToString();
        }


    }

    private void Update()
    {
        SendFinalHint();
        if (!SendFinalHint())
        {
            Time.timeScale = 0.1f;  
        }
    }

    public bool SendFinalHint()
    {
        if (total >= 100)
        {
            Debug.Log("you WOn");
            _result.text = "Congratulations";
            KeepCounting = false;
            SceneManager.LoadScene("BoxingFinish");  
            return false;
        }
        return true;
    }


}
