using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models
{
    public class RoundaboutList<T> : IList<T>
    {
        private List<T> list = new List<T>();
        public void SetContents(List<T> list) { this.list = list; }
        public T this[int index] 
        { 
            get
            {
                while(index < 0)
                {
                    index += list.Count;
                }
                return list[index % list.Count];
            }
            set
            {
                while (index < 0)
                {
                    index += list.Count;
                }
                list[index % list.Count] = value;
            } 
        }

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item) => list.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();

        public int IndexOf(T item) => list.IndexOf(item);

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
        }

        public bool Remove(T item) => list.Remove(item);

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
