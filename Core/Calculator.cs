namespace Core
{
    public class Calculator
    {
        public FiboNumber Calc(FiboNumber fiboNumber)
        {
            return new FiboNumber(fiboNumber.Position, fiboNumber.Current, fiboNumber.Current + fiboNumber.Previous);
        }
    }
}
