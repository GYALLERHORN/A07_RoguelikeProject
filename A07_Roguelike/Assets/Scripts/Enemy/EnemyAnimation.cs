using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    protected Animator animator;
    protected EnemyController controller;

    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<EnemyController>();
    }
}
