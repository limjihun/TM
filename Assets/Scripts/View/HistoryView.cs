using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryView : View
{
    [SerializeField] private HistoryCell _historyCellPrefab;
    [SerializeField] private RectTransform _parentTransform;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private GameObject _desc;
    [SerializeField] private VerifierCell _verifierCell;
    
    private List<HistoryCell> _historyCells = new();
    private List<Verifier> _verifiers;
    
    public override void Clear()
    {
        _scrollRect.verticalNormalizedPosition = 1;
        _desc.SetActive(true);
        _verifierCell.gameObject.SetActive(false);
    }

    public override void Init(List<Verifier> verifiers)
    {
        _verifiers = verifiers;
    }
    
    public void OnEnable()
    {
        _scrollRect.verticalNormalizedPosition = 1;
        _desc.SetActive(true);
        _verifierCell.gameObject.SetActive(false);
        
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

    public void OnClickAlphabet(string alphabet)
    {
        var verifier = _verifiers.Find(p => p.alphabet.ToString() == alphabet);
        if (verifier == null)
        {
            _desc.SetActive(true);
            _verifierCell.gameObject.SetActive(false);
        }
        else
        {
            _desc.SetActive(false);
            _verifierCell.gameObject.SetActive(true);
            _verifierCell.Init(_mainController, verifier);
        }
    }

    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.None);
    }
}
