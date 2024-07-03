using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    private FSMManagerDog _manager;

    private void OnAnimatorMove()
    {
        _manager.OnAnimatorMove();
    }

    private void Update()
    {
        _manager.Update();
    }

    private void FixedUpdate()
    {
        _manager.FixedUpdate();
    }

    private void Awake()
    {

        var list = new Dictionary<FSMCharacterState, FSMBase<FSMCharacterState>>();
        _manager = new FSMManagerDog(list);
        _manager.AddState(new FSMDogEat(_manager, gameObject));
        _manager.AddState(new FSMDogMove(_manager, gameObject));
        _manager.SetCurrentState(_manager.GetState(FSMCharacterState.Movement));
    
    }

}
