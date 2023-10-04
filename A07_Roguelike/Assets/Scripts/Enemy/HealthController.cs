using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private CharacterStatsHandler _statsHandler;
    private float _timeSinceLastChange = float.MaxValue;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }

    public float MaxHealth => _statsHandler.CurrentStats.maxHealth;

    private void Awake()
    {
        _statsHandler = GetComponent<CharacterStatsHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }


    private void Update()
    {
        if (_timeSinceLastChange < healthChangeDelay) 
        {
            _timeSinceLastChange += Time.deltaTime;

            if (_timeSinceLastChange >= healthChangeDelay ) 
            {
                OnInvincibilityEnd?.Invoke();
            }
        }
    }

    public void LoadHealthController(HealthController hc)
    {
        CurrentHealth = hc.CurrentHealth;
    }
    public bool ChangeHealth(int change)
    {
        if(change == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        Debug.Log($"{gameObject.name} 가 {change} 만큼의 데미지를 입었습니다");
        _timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth > 0 ? CurrentHealth : 0;


        if(change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }

        if (CurrentHealth <= 0f)
        {
            OnDeath?.Invoke();
        }

        return true;
    }

    

}
