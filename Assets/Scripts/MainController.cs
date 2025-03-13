using System;
using System.Collections.Generic;
using System.IO;
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
    public int x => _x;
    public int y => _y;
    public int z => _z;
    
    public int userX;
    public int userY;
    public int userZ;
    
    // Setting
    public int verifierCount = 0;
    
    private List<int> _ids = Enumerable.Range(1, 48).ToList();
    private List<Tuple<int, int, int>> _tuples = new();
    
    public void Start()
    {
        foreach (var view in _views)
        {
            view.Show(false);
        }
        
        _views.First(p => p.viewType == ViewType.Menu).Show(true);

        MakeTuples();
        
        // string directory = Path.GetDirectoryName("Assets/Resources/cards/");
        // RenameFilesInDirectory(directory);
    }

    void RenameFilesInDirectory(string directoryPath)
    {
        // 디렉토리가 존재하는지 확인
        if (Directory.Exists(directoryPath))
        {
            // 디렉토리 내 모든 파일 가져오기
            string[] files = Directory.GetFiles(directoryPath);

            // 각 파일에 대해 이름 변경
            foreach (string filePath in files)
            {
                if (Path.GetExtension(filePath).Contains(".meta"))
                    continue;
                
                string directory = Path.GetDirectoryName(filePath);
                string oldFileName = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);

                // 파일 이름을 5자로 줄이기
                string newFileName = oldFileName.Length > 18 ? oldFileName.Substring(0, 18) : oldFileName;

                // 새 파일 경로 생성
                string newFilePath = Path.Combine(directory, newFileName + extension);

                // 파일 이름 변경
                File.Move(filePath, newFilePath);
                Debug.Log($"File renamed: {oldFileName + extension} to {newFileName + extension}");
            }
        }
        else
        {
            Debug.LogError("The directory does not exist.");
        }
    }

    // 가능한 모든 숫자쌍
    private void MakeTuples()
    {
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 1; j <= 5; j++)
            {
                for (int k = 1; k <= 5; k++)
                {
                    _tuples.Add(Tuple.Create(i, j, k));
                }
            }
        }    
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
        
        foreach (var view in _views)
        {
            view.Clear();
        }
    }

    private List<Tuple<int, int, int>> _tempTuples = new();
    private void SelectVerifiers()
    {
        int verifierSelectCount = 0;
        while (true)
        {
            verifierSelectCount++;
            Debug.Log($"VerifierSelectCount : {verifierSelectCount}");
            _verifiers.Clear();

            var list = GetRandomSelection(_ids, verifierCount);
            // Select Verifier
            for (int i = 0; i < verifierCount; ++i)
            {
                var randomId = list[i];
                var randomLogicId = Random.Range(1, Constants.maxLogicIds[randomId] + 1);
                _verifiers.Add(new Verifier((Alphabet)i, randomId, randomLogicId));
            }
            
            // Validate Solo Answer
            var tuples = new List<Tuple<int, int, int>>(_tuples);
            bool hasWrongVerifier = false;
            foreach (var verifier in _verifiers)
            {
                _tempTuples.Clear();

                foreach (var tuple in tuples)
                {
                    if (verifier.Verify(tuple.Item1, tuple.Item2, tuple.Item3) == CheckResult.X)
                        _tempTuples.Add(tuple);
                }

                // 이 검증기로 제외되는 조합이 존재하지 않음
                if (_tempTuples.Count == 0)
                {
                    hasWrongVerifier = true;
                    break;
                }

                foreach (var tuple in _tempTuples)
                {
                    tuples.Remove(tuple);
                }
            }

            if (!hasWrongVerifier && tuples.Count == 1)
            {
                var solution = tuples[0];
                _x = solution.Item1;
                _y = solution.Item2;
                _z = solution.Item3;
                
                break;
            }
        }
    }
    
    private List<T> GetRandomSelection<T>(List<T> list, int count)
    {
        if (count > list.Count)
        {
            throw new ArgumentException("Count cannot be greater than the number of elements in the list.");
        }

        for (int i = 0; i < count; i++)
        {
            int j = Random.Range(i, list.Count);
            // Swap elements at indices i and j
            (list[i], list[j]) = (list[j], list[i]);
        }

        return list.GetRange(0, count);
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
