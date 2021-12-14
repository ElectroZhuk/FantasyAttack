using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class MinotaurStateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;
    [SerializeField] private List<State> _states;

    private Player _target;
    private State _currentState;

    public State CurrentState => _currentState;

    private void Start()
    {
        _target = GetComponent<Enemy>().Target;
    }
}
