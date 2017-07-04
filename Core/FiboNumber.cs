namespace Core
{
    public class FiboNumber : IFiboNumberContract
    {
        public int Position { get; set; }
        public int Previous { get; set; }
        public int Current { get; set; }

        public FiboNumber(int position, int previous, int current)
        {
            Position = position;
            Previous = previous;
            Current = current;
        }

        public FiboNumber GetNextToCalc()
        {
            return new FiboNumber(Position + 1, Previous, Current);
        }
    }
}
