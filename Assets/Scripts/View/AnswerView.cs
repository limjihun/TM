using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class AnswerView : View
{
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private LogicAnswerCell _logicAnswerCellPrefab;
    [SerializeField] private RectTransform _parentTransform;

    private List<LogicAnswerCell> _logicAnswerCells = new();
    public void OnEnable()
    {
        _desc.SetText($"정답은 {_mainController.x} {_mainController.y} {_mainController.z} 입니다.");
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
    }
    
    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.Menu);
    }

    public void OnClickHistory()
    {
        _mainController.ChangeView(ViewType.None, ViewType.History);
    }
}