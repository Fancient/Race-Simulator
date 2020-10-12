using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Storage<T> where T : class, IStorageConstraint
    {
        private List<T> _list = new List<T>();

        public void AddToList(T value)
        {
            value.Add(_list);
        }
    }
}
