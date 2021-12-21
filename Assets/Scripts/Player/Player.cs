using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private List<Magic> _availableMagic;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private Animator _animator;

    private int _health;
    private int _money;
    private Magic _currentMagic;
    private bool _canAttack;
    private bool _isInvincible;
    private Vector2 _mousePosition;
    
    public int Health => _health;
    public int Money => _money;

    public event UnityAction Attacking;
    public event UnityAction<float> HealthChanged;
    public event UnityAction Hitted;
    public event UnityAction Dead;
    public event UnityAction<int> MoneyChanged;
    public event UnityAction<int> DamageChanged;
    public event UnityAction<Magic> MagicChanged;

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health * 1f / _maxHealth);
        MoneyChanged?.Invoke(_money);
        ChangeMagic(1);
        _canAttack = true;
        _isInvincible = false;
        _mousePosition = Input.mousePosition;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_canAttack)
            {
                if (_currentMagic.TryAttack(_attackPosition))
                    Attacking?.Invoke();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeMagic(1);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeMagic(2);
        
        if (_currentMagic is FireMagic fire && (Vector2)Input.mousePosition != _mousePosition)
        {
            _mousePosition = Input.mousePosition;
            var worldPosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            fire.MousePositionChanged(worldPosition);
        }
    }

    private void ChangeMagic(int magicNumber)
    {
        if (magicNumber <= _availableMagic.Count && magicNumber - 1 != _availableMagic.IndexOf(_currentMagic))
        {
            if (_currentMagic != null)
                _currentMagic.Exit();

            _currentMagic = _availableMagic[magicNumber - 1];
            _animator.runtimeAnimatorController = _currentMagic.Animator;
            _currentMagic.Init();
            MagicChanged?.Invoke(_currentMagic);
            DamageChanged?.Invoke(_currentMagic.Damage);
        }
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        MoneyChanged?.Invoke(_money);
    }

    public bool CanBuy(Magic magic)
    {
        return magic.Price <= _money && _availableMagic.Contains(magic) == false;
    }

    public void Buy(Magic magic)
    {
        if (CanBuy(magic))
        {
            _availableMagic.Add(magic);
            _money -= magic.Price;
            MoneyChanged?.Invoke(_money);
        }
    }
    public void TakeDamage(int damage)
    {
        if (_isInvincible == false)
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
    }

    private void Die()
    {
        Dead?.Invoke();
        _canAttack = false;
    }
}
