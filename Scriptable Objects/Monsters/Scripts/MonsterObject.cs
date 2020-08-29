using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
[CreateAssetMenu(fileName = "new Monster", menuName = "Monster System/Monser")]
public class MonsterObject : ScriptableObject
{
    public MonsterType monsterType;
    public MonsterStatus data = new MonsterStatus();
#if UNITY_EDITOR
    public AnimatorController aniController;
#endif
}

[Serializable]
public class MonsterStatus
{
    public string Name;
    public int Hp;
    public float MoveSpeed;
    public int Attack;
}