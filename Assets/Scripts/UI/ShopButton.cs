using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnShopButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnShopButtonClicked);
    }

    private void OnShopButtonClicked()
    {
        if (_shop.isActiveAndEnabled == false)
            _shop.gameObject.SetActive(true);
    }
}
