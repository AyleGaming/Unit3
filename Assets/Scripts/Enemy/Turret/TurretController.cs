using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretController : MonoBehaviour, IStatusChangeable
{
    [SerializeField] public Transform weaponTip;
    [SerializeField] public Colors turretColor;

    private TurretState _currentState;
    public float _checkRadius = 0.4f;
    private float rotationSpeed = 10f;
    private bool turretStatus;

    public Transform _agent;
    public Transform _player;

    private void Awake()
    {
        _currentState = new TurretIdleState(this);
        _player = GameObject.FindWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentState.OnStateEnter();
        StatusManager.Instance?.RegisterStatusChangeable(this, turretColor);
    }

    // Update is called once per frame
    void Update()
    {
        _currentState.OnStateUpdate();
        // rotate turret
        if (turretStatus)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }

    public void ChangeState(TurretState state)
    {
        _currentState.OnStateExit();
        _currentState = state;
        _currentState.OnStateEnter();
    }

    public void SetStatus(bool status)
    {
        turretStatus = status;
    }

    public bool GetStatus()
    {
        return turretStatus;
    }
}
