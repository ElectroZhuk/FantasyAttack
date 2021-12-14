using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Transform _container;
    [SerializeField] private ItemCard _template;
    [SerializeField] private List<Magic> _availableMagic;

    private void Start()
    {
        foreach (var item in _availableMagic)
        {
            AddItem<Magic>(item);
        }
    }

    private void AddItem<T>(Item item)
    {
        var card = Instantiate(_template, _container);
        card.Render(item);
        card.Selling += OnSellingStart;
    }

    private void OnSellingStart(Item item, ItemCard card)
    {
        if (TrySell(item))
        {
            card.Selling -= OnSellingStart;
            card.Render(item);
        }
    }

    private bool TrySell(Item item)
    {
        if (item.CanSell() && _player.CanBuy(item))
        {
            item.Sell();
            
            if (item is Magic)
                _player.Buy((Magic)item);

            return true;
        }

        return false;
    }
}
