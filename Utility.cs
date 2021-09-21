using System.Collections;

public static class Utility
{
    
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random rand = new System.Random(seed);

        for(int i = 0; i < array.Length - 1; i++)
        {
            int randIdx = rand.Next(i, array.Length);
            T temp = array[randIdx];
            array[randIdx] = array[i];
            array[i] = temp;
        }

        return array;
    }

}
