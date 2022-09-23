namespace Services.Randomizer
{
    public interface IRandomizer
    {
        int Range(int min, int max);
        float Range(float min, float max);
    }
}