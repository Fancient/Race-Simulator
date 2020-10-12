using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class RaceLength : IStorageConstraint
    {
        public string Name { get; set; }
        public void Add<T>(List<T> list) where T : class, IStorageConstraint
        {
            list.Add(this as T);
        }

        public TimeSpan Time { get; set; }
    }
}
