using System;
using System.Collections.Generic;
using UnityEngine;

public class LogicCell : MonoBehaviour
{
    [SerializeField] private int logicId;
    [SerializeField] private GameObject x;
    
    private Action<int> _clickCallback;
    
    public void Init(Action<int> clickCallback)
    {
        _clickCallback = clickCallback;
        x.SetActive(false);
    }

    public void UpdateX(List<int> xLogicIdList)
    {
        x.SetActive(xLogicIdList.Contains(logicId));
    }
    
    public void OnClicked()
    {
        x.SetActive(!x.activeSelf);
        _clickCallback.Invoke(logicId);
    }
}
