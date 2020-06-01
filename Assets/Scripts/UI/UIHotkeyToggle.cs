using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHotkeyToggle : MonoBehaviour
{
    [SerializeField] private KeyCode _keyCode = KeyCode.I;
    [SerializeField] private GameObject _gameObjectToToggle;

    void Update()
    {
        if (PlayerInput.Instance.GetKeyDown(_keyCode))
        {
            _gameObjectToToggle.SetActive(!_gameObjectToToggle.activeSelf);
        }
    }
}
