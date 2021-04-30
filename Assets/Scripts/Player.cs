using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;

    public UnityEvent OnHealthChanged;

    public float MaxHealth => _maxHealth;
    public float Health
    {
        get => _health;
        private set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            OnHealthChanged?.Invoke();
        } 
    }
        
    private void OnValidate()
    {
        _maxHealth = _maxHealth > 0 ? _maxHealth : 0;
        Health = _health;
    }

    public void ChangeHealth(float delta)
    {
        Health += delta;
    }
}
