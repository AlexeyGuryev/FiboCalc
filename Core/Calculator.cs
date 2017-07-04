namespace Core
{
    public class Calculator
    {
        public int Calc(int prev, int cur)
        {
            if (cur < 1)
            {
                cur = 1;
            }
            return cur + prev;
        }
    }
}
