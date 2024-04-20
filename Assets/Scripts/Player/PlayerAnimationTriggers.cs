using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger() //使用攻击动画123的event帧来调用
    {   //用一个碰撞体数组来接收一个圆形区域内的所有碰撞体
        AudioManager.instance.PlaySFX(2, null);
        //遍历这个碰撞体中所有的碰撞体，如果其中的Enemy碰撞体不为空，则调用所有Enemy的Damage方法
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {

            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                    player.stats.DoDamage(_target);

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.武器);

                if (weaponData != null)
                    weaponData.Effect(_target.transform);


            }
        }
    }
    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
