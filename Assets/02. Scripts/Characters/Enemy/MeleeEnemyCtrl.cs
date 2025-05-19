using System.Collections;
using UnityEngine;

public class MeleeEnemyCtrl : EnemyCtrl
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
        }
    }

    private IEnumerator Co_Attack(Collider2D enemy)
    {
        float elapsed_time = 0f;

        var character = enemy.GetComponent<Character>();
        if (character == null)
        {
            m_attack_coroutine = null;
            yield break;
        }

        while (!character.IsDead)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            if (!character.IsDead)
            {
                Animator.SetTrigger("Attack");
                StartCoroutine(Co_Damage(character, 0.5f));
            }

            elapsed_time = 0f;
        }

        m_is_attack = false;
        m_attack_coroutine = null;
    }

    private IEnumerator Co_Damage(Character explorer, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (explorer.IsDead || !m_is_attack)
        {
            yield break;
        }

        explorer.UpdateHP(-m_current_atk);
        CreateDamageIndicator(explorer.transform.position);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, m_current_radius);
    }
}
