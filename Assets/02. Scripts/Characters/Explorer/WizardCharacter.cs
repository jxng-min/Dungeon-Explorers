using System.Collections;
using UnityEngine;

public abstract class WizardCharacter : Character
{
    protected Collider2D m_target;

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

        m_target = null;
        float min_distance = float.MaxValue;
        foreach (var hit in hits)
        {
            float distance = Vector2.Distance(transform.position, hit.transform.position);
            if (distance < min_distance)
            {
                m_target = hit;
                min_distance = distance;
            }
        }

        if (m_target != null)
        {
            if (m_attack_coroutine != null)
            {
                return;
            }

            m_attack_coroutine = StartCoroutine(Co_Attack());
        }
    }

    private IEnumerator Co_Attack()
    {
        float elapsed_time = 0f;

        var enemy_ctrl = m_target.GetComponent<EnemyCtrl>();
        if (enemy_ctrl == null)
        {
            yield break;
        }

        while (!enemy_ctrl.IsDead)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            if (!enemy_ctrl.IsDead)
            {
                Animator.SetTrigger("Attack");
                Invoke("Magic", 1f);
            }

            elapsed_time = 0f;
        }

        m_is_attack = false;
        m_attack_coroutine = null;
    }

    protected abstract void Magic();
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_current_radius);
    }
}
