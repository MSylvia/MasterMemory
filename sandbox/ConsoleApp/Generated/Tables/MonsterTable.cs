﻿using ConsoleApp;
using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System.IO;
using System;

namespace ConsoleApp.Tables
{
   public sealed partial class MonsterTable : TableBase<Monster>
   {
        readonly Func<Monster, int> primaryIndexSelector;


        public MonsterTable(Monster[] sortedData)
            : base(sortedData)
        {
            this.primaryIndexSelector = x => x.MonsterId;
        }


        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public Monster FindByMonsterId(int key)
        {
            var lo = 0;
            var hi = data.Length - 1;
            while (lo <= hi)
            {
				var mid = (int)(((uint)hi + (uint)lo) >> 1);
                var selected = data[mid].MonsterId;
                var found = (selected < key) ? -1 : (selected > key) ? 1 : 0;
                if (found == 0) { return data[mid]; }
                if (found < 0) { lo = mid + 1; }
                else { hi = mid - 1; }
            }
            return default;
        }

        public Monster FindClosestByMonsterId(int key, bool selectLower = true)
        {
            return FindUniqueClosestCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, key, selectLower);
        }

        public RangeView<Monster> FindRangeByMonsterId(int min, int max, bool ascendant = true)
        {
            return FindUniqueRangeCore(data, primaryIndexSelector, System.Collections.Generic.Comparer<int>.Default, min, max, ascendant);
        }

    }
}