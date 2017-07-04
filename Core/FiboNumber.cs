namespace Core
{
    public class FiboNumber
    {
        public string Queue { get; set; }
        public int Previous { get; set; }
        public int Current { get; set; }
        public bool Finished { get; set; }

        public FiboNumber(string queue, int previous, int current, bool finished = false)
        {
            Queue = queue;
            Previous = previous;
            Current = current;
            Finished = finished;
        }
    }
}
