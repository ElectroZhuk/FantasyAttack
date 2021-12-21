using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [Header("Shop")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _price;
    [SerializeField] private string _name;

    public Sprite Icon => _icon;
    public int Price => _price;
    public string Name => _name;
}
