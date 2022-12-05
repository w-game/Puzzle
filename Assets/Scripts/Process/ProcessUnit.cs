using System;

namespace Common
{
    public class ProcessUnit
    {
        private string _tag;
        private Action<Process> _callback;

        public Process Owner { get; set; }

        public ProcessUnit(string tag, Action<Process> callback)
        {
            _tag = tag;
            _callback = callback;
        }

        public void Execute()
        {
            _callback?.Invoke(Owner);
        }
    }
}