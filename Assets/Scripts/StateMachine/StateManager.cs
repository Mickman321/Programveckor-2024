using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateManager<Estate> : MonoBehaviour where Estate : Enum
{
   protected Dictionary<Estate, BaseState<Estate>> States = new Dictionary<Estate, BaseState<Estate>>();

    protected BaseState<Estate> CurrentState;

    void Start()
    {
        CurrentState.EnterState();
    }
    void Update()
    {
        CurrentState.UpdateState();
    }
    void OnTriggerEnter(Collider other)
    {
        CurrentState.OnTriggerEnter(other);
    }
    void OnTriggerStay(Collider other)
    {
        CurrentState.OnTriggerStay(other);
    }
    void OnTriggerExit(Collider other)
    {
        CurrentState.OnTriggerExit(other);
    }
}
