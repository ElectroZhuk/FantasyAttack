using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private List<Magic> _magic;
    [SerializeField] private Transform _attackPosition;

    private int _health;
    private int _money;
    private Magic _currentMagic;
    private bool _canAttack;
    private bool _isInvincible;
    
    public int Health => _health;
    public int Money => _money;

    public event UnityAction Attacking;
    public event UnityAction<float> HealthChanged;
    public event UnityAction Hitted;
    public event UnityAction Dead;
    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health * 1f / _maxHealth);
        MoneyChanged?.Invoke(_money);
        _currentMagic = _magic[0];
        _canAttack = true;
        _isInvincible = false;

        foreach (var magic in _magic)
        {
            magic.Sell();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_canAttack)
            {
                _currentMagic.Attack(_attackPosition);
                Attacking?.Invoke();
            }
        }
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        MoneyChanged?.Invoke(_money);
    }

    public bool CanBuy(Item item)
    {
        return item.Price <= _money;
    }

    public void Buy(Magic magic)
    {
        if (CanBuy(magic))
        {
            _magic.Add(magic);
            _money -= magic.Price;
        }
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        Hitted?.Invoke();
        HealthChanged?.Invoke(_health * 1f / _maxHealth);

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    private void Die()
    {
        Dead?.Invoke();
        _canAttack = false;
    }
}
