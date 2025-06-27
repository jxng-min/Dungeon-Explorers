using UnityEngine;
using ObjectPool;

[RequireComponent(typeof(Animator))]
public class Explosion : MonoBehaviour
{
    [field: SerializeField] public Animator Animator;

    public void Return()
    {
        ObjectManager.Instance.ReturnObject(gameObject, ObjectType.EXPLOSION);
    }
}
