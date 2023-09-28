using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector2 _direction;
    private bool _isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestory = true;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!_isReady)
        {
            return;
        }

        _currentDuration += Time.deltaTime;

        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = _direction * _attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestory);
        }
        else if(_attackData.target.value == (_attackData.target.value | (1 << collision.gameObject.layer)))
        {
            HealthController healthSystem = collision.GetComponent<HealthController>();
            if(healthSystem != null) 
            {
                healthSystem.ChangeHealth(-_attackData.power);
                if (_attackData.isOnKnockback)
                {
                    TopDownMovement movement = collision.GetComponent<TopDownMovement>();
                    KnockBack knockBack = collision.GetComponent<KnockBack>();

                    if(knockBack != null)
                    {
                        knockBack.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
                    }
                    else if(movement != null)
                    {
                        movement.ApplyKnockback(transform, _attackData.knockbackPower, _attackData.knockbackTime);
                    }
                }
            }

            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }



    public void InitializeAttack(Vector2 direction, RangedAttackData attackData, ProjectileManager projectileManager)
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        UpdateProjectilSprite();
        _trailRenderer.Clear();
        _currentDuration = 0;
        _spriteRenderer.color = attackData.projectileColor;

        transform.right = _direction;

        _isReady = true;
    }

    private void UpdateProjectilSprite()
    {
        transform.localScale = Vector3.one * _attackData.size;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {

        }
        gameObject.SetActive(false);
    }
}
