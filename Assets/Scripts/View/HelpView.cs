using UnityEngine;
using UnityEngine.UI;

public class HelpView : View
{
    [SerializeField] private ScrollRect _scrollRect;
    
    public void OnEnable()
    {
        _scrollRect.verticalNormalizedPosition = 1;
    }

    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.None);
    }
}
