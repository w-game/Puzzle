namespace GameSystem
{
    public abstract class Challenge
    {
        public int MaxCount { get; protected set; }
        public int CurCount { get; protected set; }
        public int Timer;

        public void Init()
        {
            OnInt();
        }

        protected virtual void OnInt() { }

        public void Refresh()
        {
            OnRefresh();
        }
        protected virtual void OnRefresh() { }

        public abstract void CheckProcess(RemoveUnit removeUnit);

        public void Complete()
        {
            OnComplete();
        }
        protected virtual void OnComplete() { }
    }
}