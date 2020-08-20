﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JHchoi.Contents
{
    public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
    {
        public ItemObject item;

        public void OnAfterDeserialize()
        {
        }

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
            EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
#endif
        }
    }
}