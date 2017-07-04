namespace Core
{
    public interface IFiboNumberContract
    {
        int Current { get; set; }
        int Position { get; set; }
        int Previous { get; set; }

        FiboNumber GetNextToCalc();
    }
}