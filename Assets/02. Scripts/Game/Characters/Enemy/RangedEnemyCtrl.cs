using System.Collections;
using UnityEngine;

public class RangedEnemyCtrl : EnemyCtrl
{
    protected void FixedUpdate()
    {
        if (GameManager.Instance.GameState != GameEventType.PLAYING)
        {
            return;
        }

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
        }
    }

    private IEnumerator Co_Attack(Collider2D enemy)
    {
        float elapsed_time = 0f;

        var explorer = enemy.GetComponent<Character>();
        if (explorer == null)
        {
            yield break;
        }        

        while (!explorer.IsDead)
        {
            while (elapsed_time <= m_current_cooltime)
            {
                yield return new WaitUntil(() => GameManager.Instance.GameState == GameEventType.PLAYING);

                elapsed_time += Time.deltaTime;
                yield return null;
            }

            if (!explorer.IsDead)
            {
                Animator.SetTrigger("Attack");
                Invoke("CreateBullet", 0.8f);
            }

            elapsed_time = 0f;
        }

        m_is_attack = false;
        m_attack_coroutine = null;
    }

    public void CreateBullet()
    {
        if (!m_is_attack)
        {
            return;
        }

        var bullet_obj = ObjectManager.Instance.GetObject((Script as RangedEnemy).Bullet);
        bullet_obj.transform.position = transform.position + Vector3.up * 0.25f;

        var arrow = bullet_obj.GetComponent<Arrow>();
        arrow.Initialize(m_current_atk, 8f, LayerMask.GetMask("EXPLORER"), Vector2.left);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_current_radius);
    }
}
