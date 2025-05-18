using System.Collections;
using UnityEngine;

public class MeleeCharacter : Character
{
    protected override void FixedUpdate()
    {
        // TODO: 게임 상태가 PLAYING이 아닐 경우 리턴

        if (!m_is_dead && m_knockback_coroutine == null)
        {
            if (!m_is_attack)
            {
                MoveTowardsTower();
            }

            Attack();
        }
    }

    protected override void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, m_current_radius, m_enemy_layer);
        if (hits.Length == 0)
        {
            m_is_attack = false;
            return;
        }

        Animator.SetBool("IsMove", false);

        m_is_attack = true;
        Rigidbody.linearVelocity = Vector2.zero;

        Collider2D closest = null;
        float min_distance = float.MaxValue;
        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < min_distance)
            {
                closest = hit;
                min_distance = distance;
            }
        }

        if (closest != null)
        {
            if (m_attack_coroutine != null)
            {
                return;
            }
            
            m_attack_coroutine = StartCoroutine(Co_Attack(closest));
            // Enemy target = closest.GetComponent<Enemy>();
            // if (target != null)
            // {
            //     target.UpdateHP(m_current_atk);

            //     Animator.SetTrigger("Attack");
            // }
        }
    }

    private IEnumerator Co_Attack(Collider2D enemy)
    {
        float elapsed_time = 0f;

        Animator.SetTrigger("Attack");
        while (enemy != null)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                // TODO: WaitUntil을 이용하여 게임 상태가 PLAYING일 때까지 대기

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            Animator.SetTrigger("Attack");
            elapsed_time = 0f;
        }

        m_attack_coroutine = null;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_current_radius);
    }
}
