using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class KickCollision : MonoBehaviour
{
    private Animator _animator;
    private bool _hasAnimator;
    private int _animIDDown;
    public bool isDown = false;
    private void Start()
    {
        _hasAnimator = TryGetComponent(out _animator);
        _animIDDown = Animator.StringToHash("Down");
    }
    private void OnTriggerEnter(Collider other)
    {
        //search all the parents!!
        Student_TPC tpc = other.GetComponentInParent<Student_TPC>();

        if( tpc != null
        && other.gameObject.layer == 7
        && tpc.Kicking
        && !isDown
        )
        {
            Debug.Log("kick detected!!!");
            isDown = true;
            _animator.SetTrigger(_animIDDown);
        }
    }
}
