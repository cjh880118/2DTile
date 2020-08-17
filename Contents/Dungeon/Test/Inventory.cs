using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JHchoi.Constants;

namespace JHchoi.Contents
{
    public class Inventory
    {
        private List<IItem> itempList;

        public Inventory()
        {
            itempList = new List<IItem>();
            Debug.Log("Inventory");


        }

        public void AddItem(IItem item)
        {
            itempList.Add(item);
        }

        public List<IItem> GetItemList()
        {
            return itempList;
        }

    }
}