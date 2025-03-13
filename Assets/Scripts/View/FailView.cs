using TMPro;
using UnityEngine;

public class FailView : View
{
    [SerializeField] private TextMeshProUGUI _desc;

    public void OnEnable()
    {
        _desc.SetText($"{_mainController.userX} {_mainController.userY} {_mainController.userZ}는 정답이 아닙니다.");
    }

    public void OnClickNext()
    {
        _mainController.NextRound();
        _mainController.ChangeView(viewType, ViewType.InputCode);
    }

    public void OnClickAnswer()
    {
        _mainController.ChangeView(viewType, ViewType.Answer);
    }
}
