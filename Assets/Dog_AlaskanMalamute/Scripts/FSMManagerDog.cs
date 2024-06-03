using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMManagerDog : FSMManagerBase<FSMCharacterState>
{
    private FSMDogMove _fsmDog;
    public FSMDogMove FsmDog
    {
        get
        {
            _fsmDog = CurrentState as FSMDogMove;
            return _fsmDog;
        }
    }

    public FSMManagerDog(Dictionary<FSMCharacterState, FSMBase<FSMCharacterState>> dictionary) : base(dictionary)
    {

    }

    public void OnAnimatorMove()
    {
        FsmDog?.OnAnimatorMove();
    }

}
