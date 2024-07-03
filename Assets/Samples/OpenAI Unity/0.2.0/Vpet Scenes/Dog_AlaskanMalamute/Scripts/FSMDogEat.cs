using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMDogEat : FSMDog
{
    private static readonly int IsEat = Animator.StringToHash("IsEat");

    public FSMDogEat(FSMManagerBase<FSMCharacterState> manager, GameObject deer) : base(manager, deer)
    {
        ClassState = FSMCharacterState.Eat;
    }

    public sealed override FSMCharacterState ClassState { get; protected set; }
    public override void OnEnter()
    {
        dogAnimator.SetBool(IsEat, true);
    }

    public override void OnUpdate()
    {
        if (dogAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
        {
            manager.SetCurrentState(manager.GetState(FSMCharacterState.Movement));
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {
        dogAnimator.SetBool(IsEat, false);
    }

}
