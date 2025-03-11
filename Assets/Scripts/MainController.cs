using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainController : MonoBehaviour
{
    [SerializeField] private View[] _views;

    private List<History> _historyList = new();
    public List<History> historyList => _historyList;
    private List<Verifier> _verifiers = new();
    public int round;
    public int t;
    public int r;
    public int c;
    public int questionCount;
    public readonly int maxQuestionCount = 3;
    public int totalQuestionCount;

    private int _x;
    private int _y;
    private int _z;
    
    public int userX;
    public int userY;
    public int userZ;
    
    // Setting
    public int verifierCount = 6;
    
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

        t = 0;
        r = 0;
        c = 0;
        
        _x = 0;
        _y = 0;
        _z = 0;
        userX = 0;
        userY = 0;
        userZ = 0;

        verifierCount = 6;
        
        foreach (var view in _views)
        {
            view.Clear();
        }
    }

    private void SelectVerifiers()
    {
        int verifierSelectCount = 0;
        while (true)
        {
            verifierSelectCount++;
            Debug.Log($"VerifierSelectCount : {verifierSelectCount}");
            _verifiers.Clear();
            
            // Select Verifier
            for (int i = 0; i < verifierCount; ++i)
            {
                var randomId = Random.Range(1, 49);
                var randomLogicId = Random.Range(1, Constants.maxLogicIds[randomId] + 1);
                _verifiers.Add(new Verifier((Alphabet)i, randomId, randomLogicId));
            }

            var answerCount = 0;
            var x = 0;
            var y = 0;
            var z = 0;
            
            // Validate Solo Answer
            for (int t = 1; t <= 5; ++t)
            {
                for (int r = 1; r <= 5; ++r)
                {
                    for (int c = 1; c <= 5; ++c)
                    {
                        bool isX = false;
                        foreach (var verifier in _verifiers)
                        {
                            if (verifier.Verify(t, r, c) == CheckResult.X)
                            {
                                isX = true;
                                break;
                            }
                        }

                        if (isX)
                            continue;

                        answerCount++;
                        if (answerCount > 1)
                        {
                            t = r = c = 6; // break;
                            continue;
                        }
                        
                        x = t;
                        y = r;
                        z = c;
                    }
                }
            }

            if (answerCount == 1)
            {
                _x = x;
                _y = y;
                _z = z;
                
                break;
            }
        }

        
        
        
        
        
        
        // for test
        // _verifiers.Add(new Verifier(Alphabet.A, 4, 1));
        // _verifiers.Add(new Verifier(Alphabet.B, 9, 1));
        // _verifiers.Add(new Verifier(Alphabet.C, 18, 2));
        // _verifiers.Add(new Verifier(Alphabet.D, 20, 1));
        //
        // _x = 1;
        // _y = 1;
        // _z = 1;
    }

    public void ChangeView(ViewType prev, ViewType next)
    {
        _views.FirstOrDefault(p => p.viewType == prev)?.Show(false);
        _views.FirstOrDefault(p => p.viewType == next)?.Show(true);
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

    public bool IsCorrect(int t, int r, int c)
    {
        userX = t;
        userY = r;
        userZ = c;
        return _x == t && _y == r && _z == c;
    }
}
