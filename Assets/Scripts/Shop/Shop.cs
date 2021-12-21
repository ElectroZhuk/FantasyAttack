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

    private List<ItemCard> _cards;

    private void Awake()
    {
        _cards = new List<ItemCard>();

        foreach (var item in _availableMagic)
        {
            AddItem(item);
        }
    }

    private void OnEnable()
    {
        Time.timeScale = 0;

        foreach (var card in _cards)
        {
            if (card.Item is Magic magic)
                card.UpdateState(_player.CanBuy(magic));

            card.Selling += OnSellingStart;
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;

        foreach (var card in _cards)
            card.Selling -= OnSellingStart;
    }

    private void AddItem(Item item)
    {
        var card = Instantiate(_template, _container);
        card.Render(item);
        _cards.Add(card);
    }

    private void OnSellingStart(Item item, ItemCard card)
    {
        TrySell(item, card);
    }

    private bool TrySell(Item item, ItemCard card)
    {
        if (item is Magic magic && _player.CanBuy(magic))
        {
            _player.Buy(magic);
            card.UpdateState(false);

            return true;
        }

        return false;
    }
}
