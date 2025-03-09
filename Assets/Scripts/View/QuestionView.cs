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

    private List<VerifierCell> _verifierCells = new();

    public void OnEnable()
    {
        _title.SetText($"질문하기 ({_mainController.questionCount + 1}/{_mainController.maxQuestionCount})");
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
        }
    }

    private readonly Color colorGreen = new Color(49/255f, 178/255f, 97/255f);
    private readonly Color colorRed = new Color(221/255f, 73/255f, 75/255f);
    
    private void OnSearchEnd()
    {
        _mainController.questionCount++;
        var isQuestionEnd = _mainController.questionCount >= _mainController.maxQuestionCount;

        _title.SetText(isQuestionEnd ? $"라운드 결과" : $"질문하기 ({_mainController.questionCount + 1}/{_mainController.maxQuestionCount})");
        _buttonText.SetText(isQuestionEnd ? "다음 라운드" : "정답 제출");
        _buttonImage.color = isQuestionEnd ? colorGreen : colorRed;
    }
    
    public void OnClickNext()
    {
        var isQuestionEnd = _mainController.questionCount >= _mainController.maxQuestionCount;
        if (isQuestionEnd)
            _mainController.NextRound();
        
        _mainController.ChangeView(viewType, isQuestionEnd ? ViewType.InputCode : ViewType.SolveCode);
    }

    public void OnClickHistory()
    {
        _mainController.ChangeView(viewType, ViewType.History);
    }

    public void OnClickMemo()
    {
        _mainController.ChangeView(viewType, ViewType.Memo);
    }
}
