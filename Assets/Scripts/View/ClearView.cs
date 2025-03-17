using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ClearView : View
{
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private LogicAnswerCell _logicAnswerCellPrefab;
    [SerializeField] private RectTransform _parentTransform;
    [SerializeField] private GameObject _copyButton;
    
    private List<LogicAnswerCell> _logicAnswerCells = new();
    public void OnEnable()
    {
        _desc.SetText($"{_mainController.userX} {_mainController.userY} {_mainController.userZ}는 정답이 맞습니다.");
        _copyButton.SetActive(_mainController.isDailyQuiz);
    }
    
    public override void Init(List<Verifier> verifiers)
    {
        foreach (var logicAnswerCell in _logicAnswerCells)
        {
            Destroy(logicAnswerCell.gameObject);
        }
        
        _logicAnswerCells.Clear();
        
        foreach (var verifier in verifiers)
        {
            var cell = Instantiate(_logicAnswerCellPrefab, _parentTransform).GetComponent<LogicAnswerCell>();
            cell.Init(verifier);
            _logicAnswerCells.Add(cell);    
        }
        
        _copyButton.SetActive(_mainController.isDailyQuiz);
    }
    
    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.Menu);
    }

    public void OnClickHistory()
    {
        _mainController.ChangeView(ViewType.None, ViewType.History);
    }

    public void OnClickCopyResult()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append("튜링머신 오늘의 문제");
        stringBuilder.AppendLine();
        stringBuilder.Append(DateTime.Today.ToString("yyyy-MM-dd"));
        stringBuilder.AppendLine();
        stringBuilder.Append($"{_mainController.historyList.Count}라운드 {_mainController.totalQuestionCount}번 질문으로 성공");
        stringBuilder.AppendLine();
        stringBuilder.Append($"https://limjihun.github.io/TM/");

        UniClipboard.SetText(stringBuilder.ToString());
    }
}
