using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvilutionClass
{

    /// <summary>
    /// A Manager that handles all Messages.
    /// </summary>
    public static class MessageManager
    {
        private static object Message_queue_lock = new object();

        /// <summary>
        /// Returns the current GenericMessage for processing.
        /// </summary>
        /// <returns>A GenericMessage that needs to be process, null if the MessageQueque is empty.</returns>
        public static GenericMessage Update()
        {
            lock (Message_queue_lock)
            {
                if (MessageQueue.Count == 0)
                {
                    return null;
                }
                else
                {
                    GenericMessage gm = MessageQueue.Dequeue();

                    return gm;
                }
            }
        }

        /// <summary>
        /// Add a GenericMessage into the MessageQueque.
        /// </summary>
        /// <param name="gm">A GenericMessage to add to the Queque.</param>
        /// <returns>Returns true if the GenericMessage has been inserted successfully, else false.</returns>
        public static bool AddMessageItem(GenericMessage gm)
        {
            lock (Message_queue_lock)
            {
                int count_before_add = MessageQueue.Count;

                MessageQueue.Enqueue(gm);

                int count_after_add = MessageQueue.Count;

                if (count_after_add > count_before_add)
                    return true;
                else
                    return false;
            }
        }

        public static GenericMessage PeekAndTake(Type t)
        {
            lock (Message_queue_lock)
            {
                if (MessageQueue.Count <= 0)
                    return null;

                GenericMessage gm = MessageQueue.Peek();
                if (gm.GetType() == t)
                {
                    return Update();
                }
                else
                {
                    return null;
                }
            }
        }

        private static Queue<GenericMessage> MessageQueue = new Queue<GenericMessage>();
    }
}