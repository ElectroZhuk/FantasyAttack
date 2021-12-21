using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCloseButton : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    [SerializeField] private Button _buttton;

    private void OnEnable()
    {
        _buttton.onClick.AddListener(OnCloseButtonClicked);
    }

    private void OnDisable()
    {
        _buttton.onClick.RemoveListener(OnCloseButtonClicked);
    }

    private void OnCloseButtonClicked()
    {
        _shop.gameObject.SetActive(false);
    }
}
