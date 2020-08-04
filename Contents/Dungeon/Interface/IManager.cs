using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Managers
{
    public abstract class IManager : MonoBehaviour
    {
        protected List<GameObject> gameObjects = new List<GameObject>();


        public abstract IEnumerator Load_Resource();

    }
}