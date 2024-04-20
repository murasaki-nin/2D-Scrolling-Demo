using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;
    private bool canRecover;

    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        canCreateClone = true;
        canRecover = true;

        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();
        //��һ����ײ������������һ��Բ�������ڵ�������ײ��
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //���������ײ�������е���ײ�壬������е�Enemy��ײ�岻Ϊ�գ���Ϊ��player������������Ϊtrue
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10; //����1����
                    player.anim.SetBool("SuccessfulCounterAttack", true);

                    player.skill.parry.UseSkill();
                    if (canRecover)
                    {
                        canRecover = false;
                        player.skill.parry.Recover();
                    }

                    if (canCreateClone) //����ʱ��������
                    {
                        canCreateClone = false; //��ֹͬʱ��������Ŀ��ʱ�ܴ������λ���ֻ�ᴴ��һ�Σ�
                        player.skill.parry.MakeMirageOnParry(hit.transform);
                    }
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
