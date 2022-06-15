using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class PlayerNameInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private Button continueButton;

    public static string DisplayName { get; private set; }

    private void Start()
    {
        continueButton.onClick.AddListener(SetPlayerName);
    }

    public void SetPlayerName()
    {
        DisplayName = nameInputField.text;
    }
}
