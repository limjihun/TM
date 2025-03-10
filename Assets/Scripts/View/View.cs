using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour
{
    [SerializeField] protected MainController _mainController;
    [SerializeField] protected ViewType _viewType;

    public ViewType viewType => _viewType;
    
    public virtual void Init(List<Verifier> verifiers)
    {
        
    }

    public virtual void Clear()
    {
        
    }
    
    public void Show(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}

public enum ViewType
{
    Clear,
    Fail,
    History,
    InputCode,
    Memo,
    Menu,
    Question,
    SolveCode,
    Verifiers,
}