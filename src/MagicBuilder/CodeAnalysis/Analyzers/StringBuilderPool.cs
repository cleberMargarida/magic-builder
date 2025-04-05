using System.Text;

namespace MagicBuilder.CodeAnalysis.Analyzers;

internal sealed class StringBuilderPool
{
    private const int maximumRetained = 32;
    private readonly StringBuilder[] items = new StringBuilder[maximumRetained];
    private int index = 0;

    public StringBuilder Rent()
    {
        if (index <= 0)
        {
            return new StringBuilder();
        }

        lock (items)
        {
            if (index > 0)
            {
                index--;
                var sb = items[index];
                items[index] = null!;
                return sb;
            }
        }

        return new StringBuilder();
    }

    public void Return(StringBuilder sb)
    {
        if (sb.Capacity > 1024)
        {
            return;
        }

        sb.Clear();

        if (index >= maximumRetained)
        {
            return;
        }

        lock (items)
        {
            if (index < maximumRetained)
            {
                items[index] = sb;
                index++;
            }
        }
    }
}
