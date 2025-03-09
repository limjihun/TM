public class ClearView : View
{
    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.Menu);
    }
}
