using TMPro;
using UnityEngine;

public class ClearView : View
{
    [SerializeField] private TextMeshProUGUI _desc;

    public void OnEnable()
    {
        _desc.SetText($"{_mainController.userX} {_mainController.userY} {_mainController.userZ}는 정답이 맞습니다.");
    }
    
    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.Menu);
    }
}
