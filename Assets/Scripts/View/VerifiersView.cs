using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VerifiersView : View
{
    [SerializeField] private GameObject _verifierCellPrefab;
    [SerializeField] private RectTransform _parentTransform;
    [SerializeField] private ScrollRect _scrollRect;
    
    private List<VerifierCell> _verifierCells = new();

    public override void Clear()
    {
        _scrollRect.verticalNormalizedPosition = 1;
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
            cell.Init(_mainController, verifier);
            _verifierCells.Add(cell);    
        }
    }

    public void OnEnable()
    {
        _scrollRect.verticalNormalizedPosition = 1;

        foreach (var verifierCell in _verifierCells)
        {
            verifierCell.UpdateX();
        }
    }

    public void OnClickNext()
    {
        if (_mainController.prevViewType == ViewType.Menu)
            _mainController.ChangeView(viewType, ViewType.InputCode);
        else
            _mainController.BackToPrevView();

    }
}
