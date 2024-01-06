using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Study
{
    public class TankPatrolState : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        // 상태 전이가 시작될 때 호출되고
        // 상태 기계는 이 상태 평가를 시작한다.
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        // OnStateEnter와 OnStateExit 콜백 사이 Update 프레임마다 호출된다
    
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        // 상태 전이가 끝날 떄 호출되며 이 상태 평가를 종료한다.
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        
        }
    
    }
}

