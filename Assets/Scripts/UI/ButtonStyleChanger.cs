using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonStyleChanger : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button _button;
    [SerializeField] private Image _buttonImage;
    [SerializeField] private Sprite _enabledSprite;
    [SerializeField] private Sprite _disabledSprite;
    [Header("Text")]
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;

    private bool _lastState;

    private void Start()
    {
        _lastState = _button.interactable;
        SetSprite(_button.interactable);
        ChangeTextStyle(_button.interactable);
    }

    private void Update()
    {
        if (_button.interactable != _lastState)
        {
            SetSprite(_button.interactable);
            ChangeTextStyle(_button.interactable);
            _lastState = _button.interactable;
        }
    }

    private void SetSprite(bool state)
    {
        if (state == true)
            _buttonImage.sprite = _enabledSprite;
        else
            _buttonImage.sprite = _disabledSprite;
    }

    private void ChangeTextStyle(bool state)
    {
        Vector3 position = Vector3.zero;

        if (state == true)
        {
            _text.color = _enabledColor;
            position = _buttonImage.sprite.pivot / _buttonImage.sprite.pixelsPerUnit;
        }
        else
        {
            _text.color = _disabledColor;
            //position = new Vector3(_buttonImage.sprite.pivot.x, _buttonImage.sprite.pivot.y * 0.8125f);
            position = _buttonImage.sprite.pivot / _buttonImage.sprite.pixelsPerUnit;
        }

        _text.transform.localPosition = position;
    }
}
