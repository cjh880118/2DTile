using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Constants
{
    public enum LocalizingType
    {
        KR,
        JP,
        EN,
        CH,
    }

    public enum ModeType
    {
        None,
        Easy,
        Normal,
        Hard,
    }

    public enum EndType
    {
        Play,
        Legend,
        Result,
        Max
    }

    public class PiezoData
    {
        public int Index = 0;
        public int Power = 0;   // 최소 0 ~ 최대 255
    }
}
