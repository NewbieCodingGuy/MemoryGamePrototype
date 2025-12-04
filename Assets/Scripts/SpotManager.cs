using System.Collections.Generic;
using UnityEngine;

public class SpotManager : MonoBehaviour
{
    [Header("Spot Setup")]
    public GameObject spotPrefab;
    public int spotCount = 12;
    public Rect playArea; 
    public float spotRadius = 0.5f;
    public List<Spot> Spots { get; private set; } = new List<Spot>();

    public void SetupSpots()
    {
        foreach (Transform t in transform) Destroy(t.gameObject);
        Spots.Clear();

        int tries = 0;
        int created = 0;
        while (created < spotCount && tries < 1000)
        {
            tries++;
            Vector2 pos = new Vector2(
                Random.Range(playArea.xMin + spotRadius, playArea.xMax - spotRadius),
                Random.Range(playArea.yMin + spotRadius, playArea.yMax - spotRadius)
            );

            if (!IsOverlapping(pos))
            {
                var go = Instantiate(spotPrefab, pos, Quaternion.identity, transform);
                var spot = go.GetComponent<Spot>();
                spot.Id = created;
                Spots.Add(spot);
                created++;
            }
        }

        if (created < spotCount)
        {
            Debug.LogWarning($"Only created {created} spots out of {spotCount}. Increase tries/area.");
        }
    }

    private bool IsOverlapping(Vector2 pos)
    {
        float rr = spotRadius * 2f;
        foreach (var s in Spots)
        {
            if (Vector2.Distance(pos, s.transform.position) < rr) return true;
        }
        return false;
    }

    public Spot GetRandomAvailableSpot(Spot exclude = null)
    {
        var list = new List<Spot>();
        foreach (var s in Spots)
        {
            if (s == exclude) continue;
            if (!s.IsOccupied) list.Add(s);
        }
        if (list.Count == 0) return null;
        return list[Random.Range(0, list.Count)];
    }

    /// <summary>
    /// Get a random valid spot for the target to move to
    /// </summary>
    public Spot GetRandomValidSpotForTarget(Spot targetCurrent, Spot playerCurrent, IReadOnlyList<Spot> currentSequence)
    {
        var set = new HashSet<Spot>(currentSequence);
        var candidates = new List<Spot>();
        foreach (var s in Spots)
        {
            if (s == targetCurrent) continue; 
            if (s == playerCurrent) continue;
            if (set.Contains(s)) continue;
            if (s.IsOccupied) continue;
            candidates.Add(s);
        }

        if (candidates.Count == 0) return null;
        return candidates[Random.Range(0, candidates.Count)];
    }

    public void DestroyAllSpots()
    {
        foreach (Transform t in transform) Destroy(t.gameObject);
        Spots.Clear();
    }
}
