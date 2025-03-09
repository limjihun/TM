public class FailView : View
{
    public void OnClickBack()
    {
        _mainController.ChangeView(viewType, ViewType.Question);
    }
}
