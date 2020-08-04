using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using JHchoi.Contents;

namespace JHchoi.Module
{
    public class UICamera : MonoBehaviour
    {
        public Camera[] _Camera = null;
        public Canvas[] _Canvas = null;
        public CanvasScaler[] _CanvasScaler = null;

        public void Setup(int targetDisplay)
        {
            //for (int i = 0; i < _Camera.Length; i++)
            //    _Camera[i].targetDisplay = targetDisplay;
        }
   
    }
}