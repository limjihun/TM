using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputCodeView : View
{
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private ColorToggle[] _tToggles;
    [SerializeField] private ColorToggle[] _rToggles;
    [SerializeField] private ColorToggle[] _cToggles;

    [SerializeField] private Button _submitButton;
    
    public override void Clear()
    {
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
    }   

    public void OnEnable()
    {
        _desc.SetText($"{_mainController.round}라운드에 사용할 코드를 입력하세요.");

        var index = 1;
        foreach (var tToggle in _tToggles)
        {
            tToggle.isOn = _mainController.t == index;
            tToggle.Init(Constants.colorBlue, SaveT);

            ++index;
        }

        index = 1;
        foreach (var rToggle in _rToggles)
        {
            rToggle.isOn = _mainController.r == index;
            rToggle.Init(Constants.colorYellow, SaveR);

            ++index;
        }

        index = 1;
        foreach (var cToggle in _cToggles)
        {
            cToggle.isOn = _mainController.c == index;
            cToggle.Init(Constants.colorPurple, SaveC);

            ++index;
        }


        _submitButton.interactable = CanSubmit();
    }

    private void SaveT()
    {
        var t = Array.FindIndex(_tToggles, p => p.isOn) + 1;
        _mainController.t = t;
        
        _submitButton.interactable = CanSubmit();
    }

    private void SaveR()
    {
        var r = Array.FindIndex(_rToggles, p => p.isOn) + 1;
        _mainController.r = r;
        
        _submitButton.interactable = CanSubmit();
    }
    
    private void SaveC()
    {
        var c = Array.FindIndex(_cToggles, p => p.isOn) + 1;
        _mainController.c = c;
        
        _submitButton.interactable = CanSubmit();
    }

    private bool CanSubmit()
    {
        return _mainController.t != 0 && _mainController.r != 0 && _mainController.c != 0;
    }
    
    public void OnClickVerifiers()
    {
        _mainController.ChangeView(viewType, ViewType.Verifiers);
    }

    public void OnClickSubmit()
    {
        if (!CanSubmit())
            return;
        
        _mainController.AddHistory();
        _mainController.ChangeView(viewType, ViewType.Question);
    }
    
    public void OnClickHistory()
    {
        _mainController.ChangeView(ViewType.None, ViewType.History);
    }

    public void OnClickMemo()
    {
        _mainController.ChangeView(ViewType.None, ViewType.Memo);
    }
}
