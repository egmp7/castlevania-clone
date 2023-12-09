using UnityEngine;

public class Attack1Behaviour : StateMachineBehaviour
{
    private PlayerController2D _playerController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _playerController = animator.GetComponent<PlayerController2D>();
        // controller needs to know when to reduce the speed
        _playerController.SetIsAttacking(true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // controller needs to know when to stop reducing the speed;
        _playerController.SetIsAttacking(false);
    }

}
