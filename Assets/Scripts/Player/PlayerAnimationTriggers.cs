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

    private void AttackTrigger() //ʹ�ù�������123��event֡������
    {   //��һ����ײ������������һ��Բ�������ڵ�������ײ��
        AudioManager.instance.PlaySFX(2, null);
        //���������ײ�������е���ײ�壬������е�Enemy��ײ�岻Ϊ�գ����������Enemy��Damage����
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {

            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                    player.stats.DoDamage(_target);

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.����);

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