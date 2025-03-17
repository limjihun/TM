using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _t;
    [SerializeField] private TextMeshProUGUI _r;
    [SerializeField] private TextMeshProUGUI _c;
    [SerializeField] private Image[] _images;
    
    [SerializeField] private Sprite _o;
    [SerializeField] private Sprite _x;

    private Action<string> _clickCallback;
    
    public void Init(History history, Action<string> clickCallback)
    {
        _t.SetText($"{history.t}");
        _r.SetText($"{history.r}");    
        _c.SetText($"{history.c}");
        _clickCallback = clickCallback;

        for (int i = 0; i < _images.Length; ++i)
        {
            var image = _images[i];
            switch (history.GetResult((Alphabet)i))
            {
                case CheckResult.None:
                    image.color = new Color(1, 1, 1, 0);
                    break;
                case CheckResult.O:
                    image.color = new Color(1, 1, 1, 1);
                    image.sprite = _o;
                    break;
                case CheckResult.X:
                    image.color = new Color(1, 1, 1, 1);
                    image.sprite = _x;
                    break;
            }
        }
    }

    public void OnClicked(string alphabet)
    {
        _clickCallback.Invoke(alphabet);
    }
}
