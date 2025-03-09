using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Verifier
{
    public static int[] maxLogicIds = new[]
    {
        -1, 2, 3, 3, 3, 2, 2, 2, 4, 4, // 0~9 
        4, 3, 3, 3, 3, 3, 2, 4, 2, 3, // 10~19
        3, 2, 3, 3, 3, 3, 3, 3, 3, 3, // 20~29
        3, 3, 3, 6, 3, 3, 3, 3, 3, 6, // 30~39
        9, 9, 6, 6, 6, 6, 6, 6, 9     // 40~48
    };

    public Alphabet alphabet;
    public int id;
    public int logicId;
    public int maxLogicId => maxLogicIds[logicId];
    public List<int> xLogicId = new();
    
    public Verifier(Alphabet alphabet, int id, int logicId)
    {
        this.alphabet = alphabet;
        this.id = id;
        this.logicId = logicId;
    }

    public void CheckX(int logicId)
    {
        if (xLogicId.Contains(logicId))
            xLogicId.Remove(logicId);
        else
            xLogicId.Add(logicId);
    }
    
    public CheckResult Verify(int t, int r, int c)
    {
        var result = _Verify(t, r, c) ? CheckResult.O : CheckResult.X;
        Debug.Log($"Verify result : {result.ToString()}");

        return result;
    }

    private List<int> _trc = new();
    private bool _Verify(int t, int r, int c)
    {
        Debug.Log($"Verify {t} {r} {c} with Verifier {id}{logicId:00}.");

        _trc.Clear();
        _trc.Add(t);
        _trc.Add(r);
        _trc.Add(c);
        
        var index = id * 100 + logicId;
        switch (index)
        {
            case 101:
                return t == 1;
            case 102:
                return t > 1;
            
            case 201:
                return t < 3;
            case 202:
                return t == 3;
            case 203:
                return t > 3;
            
            case 301:
                return r < 3;
            case 302:
                return r == 3;
            case 303:
                return r > 3;
            
            case 401:
                return r < 4;
            case 402:
                return r == 4;
            case 403:
                return r > 4;
            
            case 501:
                return t % 2 == 0;
            case 502:
                return t % 2 == 1;
            
            case 601:
                return r % 2 == 0;
            case 602:
                return r % 2 == 1;
            
            case 701:
                return c % 2 == 0;
            case 702:
                return c % 2 == 1;

            case 801:
                return _trc.Count(p => p == 1) == 0;
            case 802:
                return _trc.Count(p => p == 1) == 1;
            case 803:
                return _trc.Count(p => p == 1) == 2;
            case 804:
                return _trc.Count(p => p == 1) == 3;
            
            case 901:
                return _trc.Count(p => p == 3) == 0;
            case 902:
                return _trc.Count(p => p == 3) == 1;
            case 903:
                return _trc.Count(p => p == 3) == 2;
            case 904:
                return _trc.Count(p => p == 3) == 3;                
            
            case 1001:
                return _trc.Count(p => p == 4) == 0;
            case 1002:
                return _trc.Count(p => p == 4) == 1;
            case 1003:
                return _trc.Count(p => p == 4) == 2;
            case 1004:
                return _trc.Count(p => p == 4) == 3;   
            
            case 1101:
                return t < r;
            case 1102:
                return t == r;
            case 1103:
                return t > r;
            
            case 1201:
                return t < c;
            case 1202:
                return t == c;
            case 1203:
                return t > c;
            
            case 1301:
                return r < c;
            case 1302:
                return r == c;
            case 1303:
                return r > c;
            
            case 1401:
                return t < r && t < c;
            case 1402:
                return r < t && r < c;
            case 1403:
                return c < t && c < r;
            
            case 1501:
                return t > r && t > c;
            case 1502:
                return r > t && r > c;
            case 1503:
                return c > t && c > r;
            
            case 1601:
                return _trc.Count(p => p % 2 == 0) > _trc.Count(p => p % 2 == 1);
            case 1602:
                return _trc.Count(p => p % 2 == 0) < _trc.Count(p => p % 2 == 1);
            
            case 1701:
                return _trc.Count(p => p % 2 == 0) == 0;
            case 1702:
                return _trc.Count(p => p % 2 == 0) == 1;
            case 1703:
                return _trc.Count(p => p % 2 == 0) == 2;
            case 1704:
                return _trc.Count(p => p % 2 == 0) == 3;
            
            case 1801:
                return (t + r + c) % 2 == 0;
            case 1802:
                return (t + r + c) % 2 == 1;
            
            case 1901:
                return t + r < 6;
            case 1902:
                return t + r == 6;
            case 1903:
                return t + r > 6;
            
            case 2001:
                return t == r && r == c && c == t;
            case 2002:
                return !(t == r && r == c && c == t) && (t == r || r == c || c == t);
            case 2003:
                return t != r && t != c && r != c;
            
            case 2101:
                return (t == r && r == c && c == t) || (t != r && t != c && r != c);
            case 2102:
                return !(t == r && r == c && c == t) && (t == r || r == c || c == t);
            
            case 2201:
                return t < r && r < c;
            case 2202:
                return t > r && r > c;
            case 2203:
                return !(t < r && r < c) && !(t > r && r > c);
            
            case 2301:
                return t + r + c < 6;
            case 2302:
                return t + r + c == 6;
            case 2303:
                return t + r + c > 6;
            
            case 2401:
                return t + 1 == r && r + 1 == c;
            case 2402:
                return (t + 1 == r && r + 1 != c) || (t + 1 != r && r + 1 == c);
            case 2403:
                return t + 1 != r && r + 1 != c;
            
            case 2501:
                return (t + 1 != r && r + 1 != c) || (t - 1 != r && r - 1 != c);
            case 2502: // 오름내림 같이 볼 필요는 없음. 121도 오름 내림 숫자 두 개 연속으로 봄
                return (t + 1 == r && r + 1 != c) || (t + 1 != r && r + 1 == c) || (t - 1 == r && r - 1 != c) || (t - 1 != r && r - 1 == c);
            case 2503:
                return (t + 1 == r && r + 1 == c) || (t - 1 == r && r - 1 == c);
            
            case 2601:
                return t < 3;
            case 2602:
                return r < 3;
            case 2603:
                return c < 3;
            
            case 2701:
                return t < 4;
            case 2702:
                return r < 4;
            case 2703:
                return c < 4;
            
            case 2801:
                return t == 1;
            case 2802:
                return r == 1;
            case 2803:
                return c == 1;
            
            case 2901:
                return t == 3;
            case 2902:
                return r == 3;
            case 2903:
                return c == 3;
            
            case 3001:
                return t == 4;
            case 3002:
                return r == 4;
            case 3003:
                return c == 4;
            
            case 3101:
                return t > 1;
            case 3102:
                return r > 1;
            case 3103:
                return c > 1;
            
            case 3201:
                return t > 3;
            case 3202:
                return r > 3;
            case 3203:
                return c > 3;
            
            case 3301:
                return t % 2 == 0;
            case 3302:
                return r % 2 == 0;
            case 3303:
                return c % 2 == 0;
            case 3304:
                return t % 2 == 1;
            case 3305:
                return r % 2 == 1;
            case 3306:
                return c % 2 == 1;
            
            case 3401:
                return t <= r && t <= c;
            case 3402:
                return r <= c && r <= t;
            case 3403:
                return c <= t && c <= r;
            
            case 3501:
                return t >= r && t >= c;
            case 3502:
                return r >= c && r >= t;
            case 3503:
                return c >= t && c >= r;
            
            case 3601:
                return (t + r + c) % 3 == 0;
            case 3602:
                return (t + r + c) % 4 == 0;
            case 3603:
                return (t + r + c) % 5 == 0;
            
            case 3701:
                return t + r == 4;
            case 3702:
                return t + c == 4;
            case 3703:
                return r + c == 4;
            
            case 3801:
                return t + r == 6;
            case 3802:
                return t + c == 6;
            case 3803:
                return r + c == 6;
            
            case 3901:
                return t == 1;
            case 3902:
                return r == 1;
            case 3903:
                return c == 1;
            case 3904:
                return t > 1;
            case 3905:
                return r > 1;
            case 3906:
                return c > 1;
            
            case 4001:
                return t < 3;
            case 4002:
                return r < 3;
            case 4003:
                return c < 3;
            case 4004:
                return t == 3;
            case 4005:
                return r == 3;
            case 4006:
                return c == 3;
            case 4007:
                return t > 3;
            case 4008:
                return r > 3;
            case 4009:
                return c > 3;
            
            case 4101:
                return t < 4;
            case 4102:
                return r < 4;
            case 4103:
                return c < 4;
            case 4104:
                return t == 4;
            case 4105:
                return r == 4;
            case 4106:
                return c == 4;
            case 4107:
                return t > 4;
            case 4108:
                return r > 4;
            case 4109:
                return c > 4;
            
            case 4201:
                return t < r && t < c;
            case 4202:
                return r < c && r < t;
            case 4203:
                return c < t && c < r;
            case 4204:
                return t > r && t > c;
            case 4205:
                return r > c && r > t;
            case 4206:
                return c > t && c > r;
            
            case 4301:
                return t < r;
            case 4302:
                return t == r;
            case 4303:
                return t > r;
            case 4304:
                return t < c;
            case 4305:
                return t == c;
            case 4306:
                return t > c;
            
            case 4401:
                return r < t;
            case 4402:
                return r == t;
            case 4403:
                return r > t;
            case 4404:
                return r < c;
            case 4405:
                return r == c;
            case 4406:
                return r > c;
            
            case 4501:
                return _trc.Count(p => p == 1) == 0;
            case 4502:
                return _trc.Count(p => p == 1) == 1;
            case 4503:
                return _trc.Count(p => p == 1) == 2;
            case 4504:
                return _trc.Count(p => p == 3) == 0;
            case 4505:
                return _trc.Count(p => p == 3) == 1;
            case 4506:
                return _trc.Count(p => p == 3) == 2;
            
            case 4601:
                return _trc.Count(p => p == 3) == 0;
            case 4602:
                return _trc.Count(p => p == 3) == 1;
            case 4603:
                return _trc.Count(p => p == 3) == 2;
            case 4604:
                return _trc.Count(p => p == 4) == 0;
            case 4605:
                return _trc.Count(p => p == 4) == 1;
            case 4606:
                return _trc.Count(p => p == 4) == 2;
            
            case 4701:
                return _trc.Count(p => p == 1) == 0;
            case 4702:
                return _trc.Count(p => p == 1) == 1;
            case 4703:
                return _trc.Count(p => p == 1) == 2;
            case 4704:
                return _trc.Count(p => p == 4) == 0;
            case 4705:
                return _trc.Count(p => p == 4) == 1;
            case 4706:
                return _trc.Count(p => p == 4) == 2;
            
            case 4801:
                return t < r;
            case 4802:
                return t < c;
            case 4803:
                return r < c;
            case 4804:
                return t == r;
            case 4805:
                return t == c;
            case 4806:
                return r == c;
            case 4807:
                return t > r;
            case 4808:
                return t > c;
            case 4809:
                return r > c;
            
            default:
            {
                Debug.LogError($"Verify {t} {r} {c}. Verifier {id} - {logicId} not exist");
                return false;
            }
        }
    }
}
