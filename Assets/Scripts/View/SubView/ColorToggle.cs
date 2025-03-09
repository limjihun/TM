using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorToggle : Toggle
{
    private Color _color;
    private Image _image;
    private TextMeshProUGUI _text;
    private Action _onCallback;
    
    public void Start()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public void Init(Color color, Action onCallback)
    {
        _color = color;
        _onCallback = onCallback;
    }

    public void OnValueChanged(bool isOn)
    {
        _image.color = isOn ? _color : Color.white;
        interactable = !isOn;
        _text.color = isOn ? Color.white : Color.black;
        
        if (isOn)
            _onCallback.Invoke();
    }
}
