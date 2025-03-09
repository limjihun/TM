using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainController : MonoBehaviour
{
    [SerializeField] private View[] _views;

    public List<History> _historyList = new();
    public List<Verifier> _verifiers = new();
    public int round;
    public int t;
    public int r;
    public int c;
    public int questionCount;
    public readonly int maxQuestionCount = 3;
    public int totalQuestionCount;
    
    public void Start()
    {
        foreach (var view in _views)
        {
            view.Show(false);
        }
        
        _views.First(p => p.viewType == ViewType.Menu).Show(true);
    }
    public void StartGame()
    {
        Clear();
        SelectVerifiers();
        
        foreach (var view in _views)
        {
            view.Init(_verifiers);
        }
        
        ChangeView(ViewType.Menu, ViewType.Verifiers);
    }

    private void Clear()
    {
        _historyList.Clear();
        _verifiers.Clear();

        round = 1;
        questionCount = 0;
        totalQuestionCount = 0;
    }

    private void SelectVerifiers()
    {
        // for test
        _verifiers.Add(new Verifier(Alphabet.A, 4, 1));
        _verifiers.Add(new Verifier(Alphabet.B, 9, 1));
        _verifiers.Add(new Verifier(Alphabet.C, 18, 2));
        _verifiers.Add(new Verifier(Alphabet.D, 20, 1));
    }

    public void ChangeView(ViewType prev, ViewType next)
    {
        _views.First(p => p.viewType == prev).Show(false);
        _views.First(p => p.viewType == next).Show(true);
    }

    public void OnValidate()
    {
        _views = FindObjectsOfType<View>(true);
    }

    public void AddHistory()
    {
        _historyList.Add(new History(round, t, r, c));
    }

    public void SaveResult(Alphabet alphabet, CheckResult result)
    {
        var history = _historyList.Last();
        history.SaveResult(alphabet, result);
    }
    
    public CheckResult GetResult(Alphabet alphabet)
    {
        var history = _historyList.Last();
        return history.GetResult(alphabet);
    }
    
    public void NextRound()
    {
        round++;
        questionCount = 0;

        t = 0;
        r = 0;
        c = 0;
    }
}
