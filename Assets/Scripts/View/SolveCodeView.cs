using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolveCodeView : View
{
    [SerializeField] private ColorToggle[] _tToggles;
    [SerializeField] private ColorToggle[] _rToggles;
    [SerializeField] private ColorToggle[] _cToggles;

    [SerializeField] private Button _submitButton;

    private int _t;
    private int _r;
    private int _c;

    public override void Clear()
    {
        _t = 0;
        _r = 0;
        _c = 0;
        
        foreach (var tToggle in _tToggles)
        {
            tToggle.isOn = false;
        }

        foreach (var rToggle in _rToggles)
        {
            rToggle.isOn = false;
        }

        foreach (var cToggle in _cToggles)
        {
            cToggle.isOn = false;
        }
        
        _submitButton.interactable = CanSubmit();
    }

    public void OnEnable()
    {
        Clear();
    }
    
    public override void Init(List<Verifier> verifiers)
    {
        var index = 1;
        foreach (var tToggle in _tToggles)
        {
            tToggle.Init(Constants.colorBlue, SaveT);

            ++index;
        }

        index = 1;
        foreach (var rToggle in _rToggles)
        {
            rToggle.Init(Constants.colorYellow, SaveR);

            ++index;
        }

        index = 1;
        foreach (var cToggle in _cToggles)
        {
            cToggle.Init(Constants.colorPurple, SaveC);

            ++index;
        }

        _submitButton.interactable = CanSubmit();
    }

    private void SaveT()
    {
        var t = Array.FindIndex(_tToggles, p => p.isOn) + 1;
        _t = t;
        
        _submitButton.interactable = CanSubmit();
    }

    private void SaveR()
    {
        var r = Array.FindIndex(_rToggles, p => p.isOn) + 1;
        _r = r;
        
        _submitButton.interactable = CanSubmit();
    }
    
    private void SaveC()
    {
        var c = Array.FindIndex(_cToggles, p => p.isOn) + 1;
        _c = c;
        
        _submitButton.interactable = CanSubmit();
    }

    private bool CanSubmit()
    {
        return _t != 0 && _r != 0 && _c != 0;
    }
    
    public void OnClickNext()
    {
        _mainController.NextRound();
        _mainController.ChangeView(viewType, ViewType.InputCode);
    }

    public void OnClickSubmit()
    {
        if (!CanSubmit())
            return;

        if (_mainController.IsCorrect(_t, _r, _c))
        {
            _mainController.ChangeView(viewType, ViewType.Clear);
        }
        else
        {
            _mainController.ChangeView(viewType, ViewType.Fail);
        }
    }
}
