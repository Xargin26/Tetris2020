using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecordScore : MonoBehaviour
{
    // Start is called before the first frame update
    public InputField _input;
    public GameObject _playerScorePanel;

    public void OnClick()
    {
        if(string.IsNullOrEmpty(_input.text))
        {
            Debug.Log("ÕÊº“Í«≥∆Œ™ø’£¨«Î ‰»ÎÕÊº“Í«≥∆£°");
        }
        else
        {
            this.transform.parent.gameObject.SetActive(false);
            _playerScorePanel.SetActive(true);
        }
    }
}
