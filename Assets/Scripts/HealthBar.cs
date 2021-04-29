using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private float _changingSpeed;
    [SerializeField] private Image _fill;

    private Slider _slider;
    private float _value;
    private bool _isValueChanging = false;
    private Color _fillColor;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _value = _slider.value;

        if (_fill != null)
        {
            _fillColor = _fill.color;
        }
        else
        {
            Debug.LogError("Fill of health bar is not specified");
        }

    }

    public void ChangeValue(float delta)
    {
        _value += delta;
        _value = Mathf.Clamp(_value, _slider.minValue, _slider.maxValue);
        if (_isValueChanging == false)
        {
            StartCoroutine(ChangeValue());
        }
    }

    private IEnumerator ChangeValue()
    {
        _isValueChanging = true;

        if (_value > 0 && _fill != null)
        {
            _fill.color = _fillColor;
        }

        while (_slider.value != _value)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _value, _changingSpeed * Time.deltaTime);
            yield return null;
        }

        if (_slider.value == 0 && _fill != null)
        {
            _fill.color = new Color(0, 0, 0, 0);
        }

        _isValueChanging = false;
    }
}
