using JHchoi.Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Managers
{
    public abstract class IManager : MonoBehaviour
    {
        protected List<GameObject> gameObjects = new List<GameObject>();

        public virtual IEnumerator Load_Resource() { yield return null; }
        public virtual IEnumerator Load_Resource(string name) { yield return null; }

        public virtual void Init_Manager() { }

    }
}