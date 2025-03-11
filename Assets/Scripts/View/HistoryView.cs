using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryView : View
{
    [SerializeField] private HistoryCell _historyCellPrefab;
    [SerializeField] private RectTransform _parentTransform;
    [SerializeField] private ScrollRect _scrollRect;
    
    private List<HistoryCell> _historyCells = new();

    public override void Clear()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }

    public void OnEnable()
    {
        _scrollRect.verticalNormalizedPosition = 1;

        foreach (var historyCell in _historyCells)
        {
            Destroy(historyCell.gameObject);
        }
        
        _historyCells.Clear();
        
        foreach (var history in _mainController.historyList)
        {
            var cell = Instantiate(_historyCellPrefab, _parentTransform).GetComponent<HistoryCell>();
            cell.Init(history);
            _historyCells.Add(cell);    
        }
    }

    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.None);
    }
}
