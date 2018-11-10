using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EvilutionClass.MouseGenericInput;

namespace EvilutionClass
{

    /// <summary>
    /// A Manager that handles all inputs.
    /// </summary>
    public static class InputManager
    {
        private static object input_queue_lock = new object();
//        private static object message_queue_lock = new object();

                    /// <summary>
                    /// Returns the current GenericInput for processing.
                    /// </summary>
                    /// <returns>A GenericInput that needs to be process, null if the InputQueque is empty.</returns>
                    public static GenericInput Update()
        {
            lock(input_queue_lock)
            {
                if (InputQueue.Count == 0)
                {
                    return null;
                }
                else
                {
                    GenericInput gi = InputQueue.Dequeue();

                    if (gi is MouseGenericInput)
                    {
                        MouseGenericInput mgi = (MouseGenericInput)gi;

                        if (MouseGenericInputType.MousePressed == mgi.MouseInputType)
                        {
                            IsMouseDown = true;
                        }
                        else if (MouseGenericInputType.MouseReleased == mgi.MouseInputType)
                        {
                            if(IsMouseDown)
                            {
                                MouseGenericInput mouse_click_event = new MouseGenericInput(mgi.X, mgi.Y);
                                mouse_click_event.MouseInputType = MouseGenericInputType.MouseClick;
                                InputManager.AddInputItem(mouse_click_event);
                            }

                            IsMouseDown = false;
                        }
                    }
                    if (gi is GenericKeyboardInput)
                    {
                        GenericKeyboardInput ki = (GenericKeyboardInput)gi;
                        IsWKeyPress = ki.IsWKeyPress;
                        IsAKeyPress = ki.IsAKeyPress;
                        IsSKeyPress = ki.IsSKeyPress;
                        IsDKeyPress = ki.IsDKeyPress;
                    }

                    return gi;
                }
            }
        }

        /// <summary>
        /// Add a GenericInput into the InputQueque.
        /// </summary>
        /// <param name="gi">A GenericInput to add to the Queque.</param>
        /// <returns>Returns true if the GenericInput has been inserted successfully, else false.</returns>
        public static bool AddInputItem(GenericInput gi)
        {
            lock (input_queue_lock)
            {
                int count_before_add = InputQueue.Count;

                InputQueue.Enqueue(gi);

                int count_after_add = InputQueue.Count;

                if (count_after_add > count_before_add)
                    return true;
                else
                    return false;
            }
        }

        public static GenericInput PeekAndTake(Type t)
        {
            lock(input_queue_lock)
            {
                if(InputQueue.Count <= 0)
                    return null;

                GenericInput gi = InputQueue.Peek();
                if(gi.GetType() == t)
                {
                    return Update();
                }
                else
                {
                    return null;
                }
            }
        }

        private static Queue<GenericInput> InputQueue = new Queue<GenericInput>();
        public static bool IsMouseDown { get; set; }
        public static bool IsWKeyPress { get; set; }
        public static bool IsAKeyPress { get; set; }
        public static bool IsSKeyPress { get; set; }
        public static bool IsDKeyPress { get; set; }

    }
}
