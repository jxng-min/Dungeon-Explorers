using System.Collections;
using UnityEngine;

public class MeleeCharacter : Character
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

        Animator.SetTrigger("Attack");
        StartCoroutine(Co_CreateDamageIndicator(enemy.transform.position, 0.5f));

        while (enemy != null)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            Animator.SetTrigger("Attack");
            StartCoroutine(Co_CreateDamageIndicator(enemy.transform.position, 0.5f));

            elapsed_time = 0f;
        }

        m_attack_coroutine = null;
    }

    private IEnumerator Co_CreateDamageIndicator(Vector3 position, float delay)
    {
        yield return new WaitForSeconds(delay);
        CreateDamageIndicator(position);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_current_radius);
    }
}
