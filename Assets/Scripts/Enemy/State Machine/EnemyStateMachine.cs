using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;
    [SerializeField] private State _deadState;

    private Player _target;
    private Enemy _enemy;
    private State _currentState;

    public State CurrentState => _currentState;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void Start()
    {
        _target = _enemy.Target;
        Reset(_startState);
    }

    private void OnEnable()
    {
        _enemy.Dead += OnDead;
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();

        if (nextState != null)
            Transit(nextState);
    }

    private void OnDisable()
    {
        _enemy.Dead -= OnDead;
    }

    private void OnDead()
    {
        Reset(_deadState);
        //enabled = false;
    }

    private void Reset(State startState)
    {
        var states = GetComponents<State>();

        foreach (var state in states)
            state.enabled = false;

        var transitions = GetComponents<Transition>();

        foreach (var transition in transitions)
            transition.enabled = false;

        _currentState = startState;

        if (startState != null)
            startState.Enter(_target, _enemy);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target, _enemy);
    }
}
