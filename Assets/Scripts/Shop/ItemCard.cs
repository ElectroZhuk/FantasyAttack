using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _buyButtonText;
    [SerializeField] private Image _itemIcon;
    [SerializeField] private TMP_Text _name;

    private Item _item;

    public event UnityAction<Item, ItemCard> Selling;

    public void Render(Item item)
    {
        _item = item;
        _buyButtonText.text = item.Price.ToString();
        _itemIcon.sprite = item.Icon;
        _name.text = item.Name;

        if (item.CanSell() == false)
        {
            _buyButton.interactable = false;
        }
    }

    private void OnEnable()
    {
        _buyButton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(OnBuyButtonClicked);
    }

    private void OnBuyButtonClicked()
    {
        Selling?.Invoke(_item, this);
    }
}
