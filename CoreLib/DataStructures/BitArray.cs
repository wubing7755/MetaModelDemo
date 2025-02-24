using System.Collections;
using System.Text;

namespace CoreLib.DataStructures;

public sealed class BitArray : ICloneable, IEnumerator<bool>, IEnumerable<bool>
{
    private readonly bool[] field;

    private int position = -1;

    public BitArray(int n)
    {
        if(n < 1)
        {
            field = new bool[0];
        }

        field = new bool[n];
    }

    public BitArray(string sequence)
    {
        if(sequence.Length <= 0)
        {
            throw new ArgumentException("Sequence must been greater than or equal to 1");
        }

        field = new bool[sequence.Length];

        ThrowIfSequenceIsInvalid(sequence);
        Compile(sequence);
    }

    public BitArray(bool[] bits) => field = bits;

    private int Length => field.Length;

    public bool this[int offset]
    {
        get => field[offset];
        private set => field[offset] = value;
    }

    public void Compile(string sequence)
    {
        if(sequence.Length > field.Length)
        {
            throw new ArgumentException($"{nameof(sequence)} must be not longer than the bit array length");
        }

        ThrowIfSequenceIsInvalid(sequence);

        // for appropriate scaling
        var tmp = string.Empty;
        if(sequence.Length < field.Length)
        {
            var difference = field.Length - sequence.Length;

            for(var i = 0; i < difference; i++)
            {
                tmp += "0";
            }

            tmp += sequence;
            sequence = tmp;
        }

        // actual compile procedure
        for(var i = 0; i < sequence.Length; i++)
        {
            field[i] = sequence[i] == '1';
        }
    }

    private static void ThrowIfSequenceIsInvalid(string sequence)
    {
        if(!Match(sequence))
        {
            throw new ArgumentException("The sequence may only contain ones or zeros");
        }
    }

    private static bool Match(string sequence) => sequence.All(ch => ch == '0' || ch == '1');
    
    public object Clone()
    {
        var theClone = new BitArray(Length);

        for (var i = 0; i < Length; i++)
        {
            theClone[i] = field[i];
        }
        
        return theClone;
    }

    public IEnumerator<bool> GetEnumerator() => this;

    IEnumerator IEnumerable.GetEnumerator() => this;

    public bool MoveNext()
    {
        if (position + 1 >= field.Length)
        {
            return false;
        }

        position++;
        return true;
    }

    public void Reset()
    {
        position = -1;
    }

    public bool Current => field[position];

    object? IEnumerator.Current => field[position];

    public void Dispose() { }

    public static BitArray operator &(BitArray one, BitArray two)
    {
        var sequence1 = one.ToString();
        var sequence2 = two.ToString();

        var result = new StringBuilder();
        var tmp = new StringBuilder();

        if (one.Length != two.Length)
        {
            int difference;
            if (one.Length > two.Length)
            {
                difference = one.Length - two.Length;

                for (var i = 0; i < difference; i++)
                {
                    tmp.Append('0');
                }

                tmp.Append(two);
                sequence2 = tmp.ToString();
            }
            else
            {
                difference = two.Length - one.Length;
                
                for (var i = 0; i < difference; i++)
                {
                    tmp.Append('0');
                }

                tmp.Append(one);
                sequence1 = tmp.ToString();
            }
        }

        var len = one.Length > two.Length ? one.Length : two.Length;
        var ans = new BitArray(len);

        for (var i = 0; i < one.Length; i++)
        {
            result.Append(sequence1[i].Equals('1') && sequence2[i].Equals('1') ? '1' : '0');
        }
        
        ans.Compile(result.ToString().Trim());

        return ans;
    }

    public static BitArray operator <<(BitArray other, int n)
    {
        var ans = new BitArray(other.Length + n);

        for (var i = 0; i < other.Length; i++)
        {
            ans[i] = other[i];
        }

        return ans;
    }

    public static BitArray operator ^(BitArray one, BitArray two)
    {
        var sequence1 = one.ToString();
        var sequence2 = two.ToString();
        var tmp = string.Empty;
        
        var len = one.Length > two.Length ? one.Length : two.Length;
        var ans = new BitArray(len);

        var sb = new StringBuilder();

        for (var i = 0; i < len; i++)
        {
            _ = sb.Append(sequence1[i] == sequence2[i] ? '0' : '1');
        }

        var result = sb.ToString().Trim();
        ans.Compile(result);

        return ans;
    }
}