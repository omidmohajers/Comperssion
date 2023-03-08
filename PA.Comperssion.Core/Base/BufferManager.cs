using System.Collections.Generic;

namespace PA.Comperssion.Core
{
    public class BufferManager<T>
    {
        Queue<T> slices = new Queue<T>();
        private object locker = new object();
        public delegate void SliceEventHandler(object sender, object data);
        public event SliceEventHandler DataAdded = null;
        public event SliceEventHandler DataRemoved = null;

        public BufferManager(long clientID)
        {
            ClientID = clientID;
        }
        public void SetBuffer(T data)
        {
            lock (locker)
            {
                slices.Enqueue(data);
            }
            if (DataAdded != null)
                DataAdded(this, data);
        }

        public T GetBuffer()
        {
            lock (locker)
            {
                if (slices.Count == 0)
                    return default(T);
                T obj = slices.Dequeue();
                if (DataRemoved != null)
                    DataRemoved(this, obj);
                return obj;
            }
        }

        public int Count
        {
            get
            {
                return slices.Count;
            }
        }

        public long ClientID { get; private set; }
    }
}
