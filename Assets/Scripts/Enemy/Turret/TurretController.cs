using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TurretController : MonoBehaviour
{
    [SerializeField] public Transform weaponTip;
    private TurretState _currentState;
    public float _checkRadius = 0.4f;

    public Transform _agent;
    public Transform _player;

    private void Awake()
    {
        _currentState = new TurretIdleState(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();
    }

    public void ChangeState(TurretState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }
}
