using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _changingSpeed;
    [SerializeField] private Image _fill;
    [SerializeField] private Player _player;

    private Slider _slider;
    private float _neededValue;
    private bool _isValueChanging = false;
    private Color _fillColor;

    private void Awake()
    {
        _slider = GetComponent<Slider>();

        if (_fill != null)
        {
            _fillColor = _fill.color;
        }
        else
        {
            Debug.LogError("Fill of health bar is not specified");
        }

        if (_player == null)
        {
            Debug.LogError("Player of health bar is not specified");
        }
        else
        {
            ChangeValue();
        }
    }

    public void ChangeValue()
    {
        if (_player != null && _slider != null)
        {
            _neededValue = _player.Health / _player.MaxHealth * _slider.maxValue;
            _neededValue = Mathf.Clamp(_neededValue, _slider.minValue, _slider.maxValue);
            if (_isValueChanging == false)
            {
                StartCoroutine(ChangeSliderValue());
            }
        }
    }

    private IEnumerator ChangeSliderValue()
    {
        _isValueChanging = true;

        if (_neededValue > 0 && _fill != null)
        {
            _fill.color = _fillColor;
        }

        while (_slider.value != _neededValue)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _neededValue, _changingSpeed * Time.deltaTime);
            yield return null;
        }

        if (_slider.value == 0 && _fill != null)
        {
            _fill.color = new Color(0, 0, 0, 0);
        }

        _isValueChanging = false;
    }
}
