using System;
using System.Collections.Generic;

namespace Common
{
    public class Process
    {
        private bool Recycle { get; }
        public Process()
        {
            
        }

        public Process(bool recycle)
        {
            Recycle = recycle;
        }
        
        private List<ProcessUnit> _units = new();
        private ProcessUnit CurExecute { get; set; }
        public void Add(string tag, Action<Process> action)
        {
            var unit = new ProcessUnit(tag, action)
            {
                Owner = this
            };
            _units.Add(unit);
            CurExecute = unit;
            
            CurExecute.Execute();
        }

        public void Next()
        {
            var index = _units.IndexOf(CurExecute);
            index++;
            if (index >= _units.Count)
            {
                if (Recycle)
                {
                    index = 0;
                }
                else
                {
                    Complete();
                    return;
                }
            }

            CurExecute = _units[index];
            CurExecute.Execute();
        }

        private void Complete()
        {
            
        }
    }
}