using System.Collections;

namespace CoreLib.DataStructures;

public class Timeline<TValue> : ICollection<(DateTime Time, TValue Value)>, IEquatable<Timeline<TValue>>
{
    private readonly List<(DateTime Time, TValue Value)> timeline = new();
    
    public Timeline()
    {
    }

    public Timeline(DateTime time, TValue value)
        => timeline = new List<(DateTime, TValue)>
        {
            (time, value),
        };

    public Timeline(params (DateTime, TValue)[] timeline)
        => this.timeline = timeline
            .OrderBy(pair => pair.Item1)
            .ToList();

    public int TimesCount => GetAllTimes().Length;

    public TValue[] this[DateTime time]
    {
        get => GetValuesByTime(time);
        set
        {
            var overridenEvents = timeline.Where(@event => @event.Time == time).ToList();
            foreach (var @event in overridenEvents)
            {
                timeline.Remove(@event);
            }

            foreach (var v in value)
            {
                Add(time, v);
            }
        }
    }
    
    public DateTime[] GetAllTimes()
        => timeline.Select(t => t.Time).Distinct().ToArray();

    public TValue[] GetValuesByTime(DateTime time)
        => timeline.Where(pair => pair.Time == time)
            .Select(pair => pair.Value)
            .ToArray();

    public void Add(DateTime time, TValue value)
    {
        timeline.Add((time, value));
    }

    public void Add(params (DateTime, TValue)[] timeline)
    {
        this.timeline.AddRange(timeline);
    }

    public (DateTime Time, TValue Value)[] ToArray()
        => timeline.ToArray();

    public override bool Equals(object? obj)
        => obj is Timeline<TValue> otherTimeline && this == otherTimeline;

    public IEnumerator<(DateTime Time, TValue Value)> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Add((DateTime Time, TValue Value) item)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public bool Contains((DateTime Time, TValue Value) item)
    {
        throw new NotImplementedException();
    }

    public void CopyTo((DateTime Time, TValue Value)[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public bool Remove((DateTime Time, TValue Value) item)
    {
        throw new NotImplementedException();
    }

    public int Count { get; }
    public bool IsReadOnly { get; }
    public bool Equals(Timeline<TValue>? other)
    {
        throw new NotImplementedException();
    }
}