using UnityEngine;

public class Constants
{
    public static readonly Color colorGreen = new Color(49/255f, 178/255f, 97/255f);
    public static readonly Color colorRed = new Color(221/255f, 73/255f, 75/255f);
    public static readonly Color colorBlue = new Color(103/255f, 183/255f, 222/255f);
    public static readonly Color colorYellow = new Color(250/255f, 181/255f, 11/255f);
    public static readonly Color colorPurple = new Color(124/255f, 87/255f, 177/255f);

    public static readonly int[] maxLogicIds = new[]
    {
        -1, 2, 3, 3, 3, 2, 2, 2, 4, 4, // 0~9 
        4, 3, 3, 3, 3, 3, 2, 4, 2, 3, // 10~19
        3, 2, 3, 3, 3, 3, 3, 3, 3, 3, // 20~29
        3, 3, 3, 6, 3, 3, 3, 3, 3, 6, // 30~39
        9, 9, 6, 6, 6, 6, 6, 6, 9     // 40~48
    };
}


