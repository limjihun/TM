using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VerifiersView : View
{
    [SerializeField] private GameObject _verifierCellPrefab;
    [SerializeField] private RectTransform _parentTransform;

    private List<VerifierCell> _verifierCells = new();
    
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

    public void OnClickNext()
    {
        _mainController.ChangeView(viewType, ViewType.InputCode);
    }
}
