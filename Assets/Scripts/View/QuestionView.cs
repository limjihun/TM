using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionView : View
{
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _t;
    [SerializeField] private TextMeshProUGUI _r;
    [SerializeField] private TextMeshProUGUI _c;
    
    [SerializeField] private GameObject _verifierCellPrefab;
    [SerializeField] private RectTransform _parentTransform;

    [SerializeField] private Image _buttonImage;
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private Button _nextButton;
    [SerializeField] private ScrollRect _scrollRect;

    private List<VerifierCell> _verifierCells = new();

    public override void Clear()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }
    
    public void OnEnable()
    {
        UpdateQuestionView();
        
        _t.SetText($"{_mainController.t}");
        _r.SetText($"{_mainController.r}");
        _c.SetText($"{_mainController.c}");

        UpdateCells();
    }
    
    public override void Init(List<Verifier> verifiers)
    {
        foreach (var verifier in _verifierCells)
        {
            Destroy(verifier.gameObject);
        }
        
        _verifierCells.Clear();
        
        foreach (var verifier in verifiers)
        {
            var cell = Instantiate(_verifierCellPrefab, _parentTransform).GetComponent<VerifierCell>();
            cell.Init(_mainController, verifier, OnSearchEnd);
            _verifierCells.Add(cell);    
        }
    }

    public void UpdateCells()
    {
        foreach (var cell in _verifierCells)
        {
            cell.UpdateIcon();
            cell.UpdateX();
        }
    }
    
    private void OnSearchEnd()
    {
        UpdateQuestionView();
        UpdateCells();
    }

    private void UpdateQuestionView()
    {
        var isQuestionEnd = _mainController.questionCount >= _mainController.maxQuestionCount;

        _title.SetText(isQuestionEnd ? $"라운드 결과" : $"질문하기 ({_mainController.questionCount + 1}/{_mainController.maxQuestionCount})");
        _buttonText.SetText(isQuestionEnd ? "라운드 종료" : "라운드 건너뛰기");
        _buttonImage.color = isQuestionEnd ? Constants.colorGreen : Constants.colorRed; 
        _nextButton.interactable = _mainController.questionCount >= 1;
    }
    
    public void OnClickNext()
    {
        if (_verifierCells.Exists(p => p.pendingAnim))
            return;
        
        _mainController.ChangeView(viewType, ViewType.SolveCode);
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
