using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMDog : FSMBase<FSMCharacterState>
{
    protected Animator dogAnimator;
    protected CharacterController characterController;
    protected FSMDog(FSMManagerBase<FSMCharacterState> manager, GameObject dog) 
    {
        dogAnimator = dog.GetComponent<Animator>();
        characterController = dog.GetComponent<CharacterController>();
    }

}
