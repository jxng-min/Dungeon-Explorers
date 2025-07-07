using UnityEngine;
using ObjectPool;

[RequireComponent(typeof(Animator))]
public class Explosion : Skill
{
    #region Properties
    [field: SerializeField] public Animator Animator { get; private set; }
    #endregion Properties

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    #region Helper Methods
    public override void Resume()
    {
        Animator.speed = 1f;
    }

    public override void Stop()
    {
        Animator.speed = 0f;
    }

    protected override void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.EXPLOSION);
    }
    #endregion Helper Methods
}