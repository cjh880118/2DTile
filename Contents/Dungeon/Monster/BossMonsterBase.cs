using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JHchoi.Contents
{
    public abstract class BossMonsterBase : MonsterBase
    {
        // Start is called before the first frame update

        protected abstract IEnumerator Move_Pattern_1();
        protected abstract IEnumerator Move_Pattern_2();
        protected abstract IEnumerator Move_Pattern_3();
        protected abstract IEnumerator Move_Pattern_4();
        protected abstract IEnumerator Move_Pattern_5();
        protected abstract IEnumerator Move_Pattern_6();

        protected abstract void Attack_Pattern_1();
        protected abstract void Attack_Pattern_2();
        protected abstract void Attack_Pattern_3();
        protected abstract void Attack_Pattern_4();

    }
}