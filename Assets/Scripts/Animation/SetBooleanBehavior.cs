using UnityEngine;

public class SetBooleanBehavior : StateMachineBehaviour
{
    public string boolName;
    public bool updateOnState;
    public bool valueOnEnter, valueOnExit;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnState)
        {
            animator.SetBool(boolName, valueOnEnter);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateOnState)
        {
            animator.SetBool(boolName, valueOnExit);
        }
    }
}
