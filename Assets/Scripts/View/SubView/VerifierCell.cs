using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VerifierCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alphabet;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject[] _icons;
    [SerializeField] private GameObject[] _grids;

    private MainController _mainController;
    private Verifier _verifier;
    private Action _searchCallback;
    private LogicCell[] _logicCells;
    private bool _pendingAnim;
    public bool pendingAnim => _pendingAnim;
    
    public void Init(MainController mainController, Verifier verifier, Action searchCallback = null)
    {
        _mainController = mainController;
        _verifier = verifier;
        _searchCallback = searchCallback;
        
        _alphabet.SetText(verifier.alphabet.ToString());
        _image.sprite = Resources.Load<Sprite>($"cards/TM_GameCards_KR-{verifier.id:00}");
        foreach (var icon in _icons)
        {
            icon.SetActive(false);
        }

        foreach (var grid in _grids)
        {
            grid.SetActive(false);
        }

        var gridIndex = 0;
        switch (verifier.maxLogicId)
        {
            case 2: gridIndex = 0;
                break;
            case 3 : gridIndex = 1;
                break;
            case 4 : gridIndex = 2;
                break;
            case 6 : gridIndex = 3;
                break;
            case 9 : gridIndex = 4;
                break;
        }
        
        _grids[gridIndex].SetActive(true);
        _logicCells = _grids[gridIndex].GetComponentsInChildren<LogicCell>();
        foreach (var logicCell in _logicCells)
        {
            logicCell.Init(OnClickLogic);
        }
    }

    private void OnClickLogic(int logicId)
    {
        _verifier.CheckX(logicId);
    }

    public void UpdateX()
    {
        foreach (var logicCell in _logicCells)
        {
            logicCell.UpdateX(_verifier.xLogicIdList);
        }
    }
    
    public void UpdateIcon()
    {
        if (_pendingAnim)
            return;
        
        foreach (var icon in _icons)
        {
            icon.SetActive(false);
        }
        
        var result = _mainController.GetResult(_verifier.alphabet);
        if (result == CheckResult.None && _mainController.questionCount >= _mainController.maxQuestionCount)
            return;
        
        _icons[(int)result].SetActive(true);
    }
    
    public void OnClickSearch()
    {
        if (_mainController.questionCount >= _mainController.maxQuestionCount)
            return;
        
        _icons[2].SetActive(false); // Search
        _icons[3].SetActive(true); // Robot Anim

        _mainController.questionCount++;
        _mainController.totalQuestionCount++;
        StartCoroutine(PlaySearchAnim());
    }

    private IEnumerator PlaySearchAnim()
    {
        _pendingAnim = true;
        yield return new WaitForSeconds(2f);
        _pendingAnim = false;
        
        var result = _verifier.Verify(_mainController.t, _mainController.r, _mainController.c);
        _mainController.SaveResult(_verifier.alphabet, result);

        UpdateIcon();

        _searchCallback.Invoke();
    }
}
