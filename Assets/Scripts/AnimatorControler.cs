using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : MonoBehaviour
{
    public Animator _animator;

    private void Awake()
    {

    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical") || Input.GetButtonDown("Horizontal"))
        {
           _animator.SetFloat("moving", 1f);
        }
        else if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal"))
        {
            _animator.SetFloat("moving", 0f);
        }
    }
}
