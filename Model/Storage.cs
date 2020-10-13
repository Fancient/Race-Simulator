using System.Collections.Generic;

namespace Model
{
    public class Storage<T> where T : class, IStorageConstraint
    {
        private List<T> _list = new List<T>();

        public void AddToList(T value)
        {
            value.Add(_list);
        }

        public string BestParticipant()
        {
            if (_list.Count == 0)
                return "";
            return _list[0].BestParticipant(_list);
        }

        public List<T> GetList()
        {
            return _list;
        }
    }
}