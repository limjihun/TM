using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : View
{
    [SerializeField] private ColorToggle[] _toggles;
    [SerializeField] private Button _startButton;

    public override void Clear()
    {
        foreach (var toggle in _toggles)
        {
            toggle.isOn = false;
        }
    }  
    
    public void OnEnable()
    {
        _toggles[0].Init(Constants.colorBlue, OnValueChanged);
        _toggles[1].Init(Constants.colorYellow, OnValueChanged);
        _toggles[2].Init(Constants.colorPurple, OnValueChanged);

        _startButton.interactable = false;
    }

    private void OnValueChanged()
    {
        bool isOn = false;
        for (int i = 0; i < _toggles.Length; ++i)
        {
            var toggle = _toggles[i];
            if (toggle.isOn)
            {
                switch (i)
                {
                    case 0: _mainController.verifierCount = 4;
                        break;
                    case 1: _mainController.verifierCount = 5;
                        break;
                    case 2: _mainController.verifierCount = 6;
                        break;
                }

                isOn = true;
                break;
            }
        }

        _startButton.interactable = isOn;
    }

    public void OnClickHelp()
    {
        _mainController.ChangeView(ViewType.None, ViewType.Help);    
    }
    
    public void OnClickStart()
    {
        _mainController.StartGame();
    }

    public void OnClickDailyQuiz()
    {
        _mainController.StartDailyGame();
    }

    public void OnClickBuy()
    {
        Application.OpenURL("https://www.happybaobab.com/shop/item.php?it_id=CD05C-33551");
    }
}
