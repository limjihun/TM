using System;
using System.Collections.Generic;

public class History
{
    public int round;
    public int t;
    public int r;
    public int c;
    public CheckResult[] _checkResults = new CheckResult[6];

    public History(int round, int t, int r, int c)
    {
        this.round = round;
        this.t = t;
        this.r = r;
        this.c = c;

        Array.Fill(_checkResults, CheckResult.None);
    }

    public void SaveResult(Alphabet alphabet, CheckResult result)
    {
        _checkResults[(int)alphabet] = result;
    }

    public CheckResult GetResult(Alphabet alphabet)
    {
        return _checkResults[(int)alphabet];
    }
}

public enum CheckResult
{
    O,
    X,
    None,
}

public enum Alphabet
{
    A,
    B,
    C,
    D,
    E,
    F,
}