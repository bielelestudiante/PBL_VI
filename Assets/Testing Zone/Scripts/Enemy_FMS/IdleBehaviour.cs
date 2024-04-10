using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : BaseBehaviour
{
    float _timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        _timer = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check triggers
        bool isTimeUp = CheckTime();
        bool isPlayerClose = CheckPlayer(animator.transform);

        animator.SetBool("IsPatrolling", isTimeUp);
        animator.SetBool("IsChasing", isPlayerClose);
    }

    private bool CheckTime()
    {
        _timer += Time.deltaTime;
        return _timer > 2;
    }
}
