using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMBase<T> where T: struct, Enum
{
    public abstract T ClassState { get; protected set; }
    //为什么添加这个
    protected FSMManagerBase<T> manager;

    protected FSMBase(FSMManagerBase<T> manager)
    {
        this.manager = manager;
    }
    protected FSMBase()
    {

    }

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
    public abstract void OnExit();
}
