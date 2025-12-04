using System.Collections.Generic;
using UnityEngine;

public class SequenceManager : MonoBehaviour
{
    public List<Spot> SequenceSpots { get; private set; } = new List<Spot>();

    public void ClearSequence()
    {
        SequenceSpots.Clear();
    }

    public void AppendSpot(Spot spot)
    {
        if (SequenceSpots.Contains(spot)) return;
        SequenceSpots.Add(spot);
    }

    public Spot RemoveFirst()
    {
        if (SequenceSpots.Count == 0) return null;
        var s = SequenceSpots[0];
        SequenceSpots.RemoveAt(0);
        return s;
    }
}
