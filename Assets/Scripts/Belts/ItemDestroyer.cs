﻿using Conveyor;
using UnityEngine;

namespace Belts
{
    public class ItemDestroyer : MonoBehaviour
    {
        private int _position;
        private DictionaryItemsGenerator _generator;

        public void CheckPosition(DictionaryItemsGenerator generators, int positions)
        {
            _position = positions;
            _generator = generators;
        }

        public void DestroyThis()
        {
            _generator.ShiftItemsInList(_position);
        }
    }
}
