using System.Collections;
using UnityEngine;

public class RangedCharacter : Character
{
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

        while (enemy != null)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            Animator.SetTrigger("Attack");
            // TODO: 발사 로직 추가 필요
            elapsed_time = 0f;
        }

        m_attack_coroutine = null;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_current_radius);
    }
}
