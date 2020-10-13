using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IStorageConstraint
    {
        public string Name { get; set; }
        public void Add<T>(List<T> list) where T : class, IStorageConstraint;

        public string BestParticipant<T>(List<T> list) where T : class, IStorageConstraint;
    }
}
