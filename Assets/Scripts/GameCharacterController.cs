using System.Collections;
using UnityEngine;
using DG.Tweening;

public class GameCharacterController : MonoBehaviour
{
    public Spot CurrentSpot { get; private set; }
    public float moveTime = 0.5f;
    public bool isTarget = false;

    public void SpawnAtSpot(Spot spot)
    {
        CurrentSpot = spot;
        transform.position = spot.transform.position;
        spot.SetOccupied(true);
    }

    public Tween MoveToSpotTween(Spot destination)
    {
        if (destination == null) return null;

        if (CurrentSpot != null)
        {
            CurrentSpot.SetOccupied(false);
            CurrentSpot = destination;
        }

        Vector3 endPos = destination.transform.position;

        Tween moveTween = transform.DOMove(endPos, moveTime)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                CurrentSpot.SetOccupied(true);
            });

        return moveTween;
    }
}
