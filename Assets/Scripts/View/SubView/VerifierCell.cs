using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VerifierCell : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _alphabet;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject[] _icons;

    private MainController _mainController;
    private Verifier _verifier;
    private Action _searchCallback;
    
    public void Init(MainController mainController, Verifier verifier, Action searchCallback = null)
    {
        _mainController = mainController;
        _verifier = verifier;
        _searchCallback = searchCallback;
        
        _alphabet.SetText(verifier.alphabet.ToString());
        _image.sprite = Resources.Load<Sprite>($"cards/TM_GameCards_KR-{verifier.id:00}");
        foreach (var icon in _icons)
        {
            icon.SetActive(false);
        }
    }

    public void UpdateIcon()
    {
        foreach (var icon in _icons)
        {
            icon.SetActive(false);
        }

        var result = _mainController.GetResult(_verifier.alphabet);
        _icons[(int)result].SetActive(true);
    }
    
    public void OnClickSearch()
    {
        _icons[2].SetActive(false); // Search
        _icons[3].SetActive(true); // Robot Anim

        StartCoroutine(PlaySearchAnim());
    }

    private IEnumerator PlaySearchAnim()
    {
        yield return new WaitForSeconds(2f);
        
        var result = _verifier.Verify(_mainController.t, _mainController.r, _mainController.c);
        _mainController.SaveResult(_verifier.alphabet, result);

        UpdateIcon();

        _searchCallback.Invoke();
    }
}
