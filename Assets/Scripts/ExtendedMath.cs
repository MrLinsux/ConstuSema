using UnityEngine.UI;

public class Interval
{
    float a, b;

    public Interval(float a, float b)
    {
        if (a <= b)
        {
            this.a = a; this.b = b;
        }
        else
        {
            this.a = b; this.b = a;
        }
    }

    public bool NumberInInterval(float number)
    {
        return (this.a <= number) && (this.b >= number);
    }
}

public class MathSet
{
    // TODO: think about it
}