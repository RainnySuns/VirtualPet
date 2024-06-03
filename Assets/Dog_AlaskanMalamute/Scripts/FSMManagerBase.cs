using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMManagerBase<T> where T : struct, Enum
{
    public Dictionary<T, FSMBase<T>> fsmDictionary;
    public FSMBase<T> CurrentState
    {
        get; protected set;
    }

    protected FSMManagerBase(Dictionary<T, FSMBase<T>> dictionary)
    {
        fsmDictionary = dictionary;
    }

    protected FSMManagerBase()
    {
        fsmDictionary = new Dictionary<T, FSMBase<T>>();
    }

    public FSMBase<T> GetState(T state)
    {
        return fsmDictionary[state];
    }

    public void AddState(FSMBase<T> fsmState)
    {
        fsmDictionary.Add(fsmState.ClassState, fsmState);
    }

    public void DeleteState(T state)
    {
        fsmDictionary.Remove(state);
    }

    public virtual void Update()
    {
        CurrentState?.OnUpdate();
    }

    public virtual void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }

    public void SetCurrentState(FSMBase<T> state)
    {
        CurrentState?.OnExit();
        CurrentState = state;
        CurrentState?.OnEnter();
    }


}
