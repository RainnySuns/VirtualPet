using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMDogPlayball : FSMDog
{
    private static readonly int X = Animator.StringToHash("X");
    private Vector3 _moveStep;
    private sbyte _speed = 8;
    public FSMDogPlayball(FSMManagerBase<FSMCharacterState> manager, GameObject dog) : base(manager, dog)
    {
        ClassState = FSMCharacterState.Movement;
        _moveStep = new Vector3();
    }

    public sealed override FSMCharacterState ClassState { get; protected set; }
    public override void OnEnter()
    {

    }

    public override void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            manager.SetCurrentState(manager.GetState(FSMCharacterState.Idle));
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }

    public void OnAnimatorMove()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        _moveStep.Set(h * Time.deltaTime * _speed, 0, v * Time.deltaTime * _speed);
        characterController.Move(_moveStep);
        if (h != 0 || v != 0)
        {
            dogAnimator.SetFloat(X, _speed);
        }
        else
        {
            dogAnimator.SetFloat(X, 0);
        }
    }

}
