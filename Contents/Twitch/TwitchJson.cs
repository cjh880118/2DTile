using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class TwitchJson
{
    public int chatter_count;

    public Chatters chatters;

    [Serializable]
    public class Chatters
    {
        public string[] moderators;
        public string[] staff;
        public string[] admin;
        public string[] global_mods;
        public string[] viewers;
    }
}

