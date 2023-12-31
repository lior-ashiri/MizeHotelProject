﻿using MizeHotelProject.Storages.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MizeHotelProject
{
    public class ChainResource<T> where T : class
    {
        private readonly List<IReadableStorage<T>> _readOnlyStorages;
        private readonly List<IReadWriteAbleStorage<T>> _readWriteStorages;

        public ChainResource(IEnumerable<IReadableStorage<T>> storages)
        {
            var readableStorages = storages.Where(x=>x.CanWrite==false).ToList();
            var writeableStorages = storages.Where(x => x.CanWrite == true).Select(x => x as IReadWriteAbleStorage<T>).ToList();
            _readOnlyStorages = readableStorages;
            _readWriteStorages = writeableStorages.OrderBy(x => x.GetTimeToLive).ToList();
        }
        public async Task<T> GetValue()
        {
            var storagesToUpdate = new Stack<IReadWriteAbleStorage<T>>();
            T result = null;
            foreach (var storage in _readWriteStorages)
            {
                if (result != null)
                {
                    continue;
                }
                result = await storage.GetValue();
                if (result==null)
                {
                    storagesToUpdate.Push(storage);
                }
            }
            if (result == null)
            {
                result = await _readOnlyStorages.FirstOrDefault().GetValue();
            }
            if (result != null)
            {
                while (storagesToUpdate.Count > 0)
                {
                    var storge = storagesToUpdate.Pop();
                    await storge.Overrite(result);
                }
            }
            return result;
        }
    }
}