using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OgreAnimation : EnemyAnimation
{
    private OgreEvents _ogreEvents;

    protected override void Awake()
    {
        base.Awake();
        _ogreEvents = (OgreEvents)_events;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _ogreEvents.Jumping += OnJumping;
        _ogreEvents.Boosting += OnBoosting;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _ogreEvents.Jumping -= OnJumping;
        _ogreEvents.Boosting -= OnBoosting;
    }

    private void OnJumping()
    {
        _animator.SetTrigger(OgreAnimatorController.Params.Jumping);
    }

    private void OnBoosting()
    {
        _animator.SetTrigger(OgreAnimatorController.Params.Boosting);
    }
}
