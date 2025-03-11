using TMPro;
using UnityEngine;

public class MemoView : View
{
    [SerializeField] private TMP_InputField _inputField;

    public override void Clear()
    {
        _inputField.text = "";
    }

    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.None);
    }
}
