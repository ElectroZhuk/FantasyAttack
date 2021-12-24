using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private float _originalSpeed;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _timeToBury;

    private int _health;
    private Player _target;
    private bool _canAttack;
    private List<Ability> _effects;

    public int Health => _health;
    public int Reward => _reward;
    public Player Target => _target;
    public IEnumerable Effects => _effects;
    public bool IsTargeted;
    public float Speed { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsBuried { get; private set; }

    public event UnityAction Dead;
    public event UnityAction Buried;

    protected virtual void Awake()
    {
        Speed = _originalSpeed;
        IsTargeted = false;
    }

    protected virtual void Start()
    {
        _health = _maxHealth;
        _canAttack = true;
        IsBuried = false;
        IsDead = false;
    }

    public virtual void Init(Player player)
    {
        _target = player;
    }

    public void Respawn()
    {
        _health = _maxHealth;
        _canAttack = true;
        IsBuried = false;
        IsDead = false;
    }

    public void BoostSpeed(float ratio, float duration, Ability ability = null)
    {
        if (ratio > 1)
            StartCoroutine(StartBoostingSpeed(ratio, duration, ability));
    }

    private IEnumerator StartBoostingSpeed(float ratio, float duration, Ability ability)
    {
        if (ability != null)
            _effects.Add(ability);

        float elapsedTime = 0;
        Speed = _originalSpeed * ratio;

        while (elapsedTime < duration)
        {
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        Speed = _originalSpeed;
        
        if (ability != null)
            _effects.Remove(ability);
    }

    public virtual void Attack(Player target)
    {
        if (_canAttack)
            target.TakeDamage(_damage);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            _health = 0;
            Die();
        }
    }

    protected void Die()
    {
        _target.AddMoney(Reward);
        _canAttack = false;
        IsDead = true;
        Dead?.Invoke();
    }

    public void Bury()
    {
        if (IsDead)
        {
            var distance = _renderer.sprite.bounds.max.y - _renderer.sprite.bounds.min.y;
            StartCoroutine(MoveDown(_timeToBury, distance));
        }
    }

    protected IEnumerator MoveDown(float time, float distance)
    {
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - distance), (distance * Time.deltaTime) / time);
            
            yield return null;

            elapsedTime += Time.deltaTime;
        }

        IsBuried = true;
        Buried?.Invoke();
        gameObject.SetActive(false);
    }
}
