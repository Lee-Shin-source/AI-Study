using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public class SelectWaypointState : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var tankAi = animator.gameObject.GetComponent<TankAi>();
            tankAi.SetNextPoint();
        }
    }

}
