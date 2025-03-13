using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicAnswerCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alphabet;
    [SerializeField] private Image _image;

    public void Init(Verifier verifier)
    {
        _alphabet.SetText(verifier.alphabet.ToString());
        _image.sprite = Resources.Load<Sprite>($"logic/c{verifier.id:00}{verifier.logicId:00}");
    }
}
