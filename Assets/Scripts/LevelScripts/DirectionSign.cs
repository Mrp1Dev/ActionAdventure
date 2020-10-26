using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    East,
    NorthEast,
    North,
    NorthWest,
    West,
    SouthWest,
    South,
    SouthEast
}

public class DirectionSign : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private Transform arrow;

    private void OnValidate()
    {
        if (arrow != null)
        {
            var rot = arrow.eulerAngles;
            rot.z = (int)direction * 45;
            arrow.eulerAngles = rot;
        }
    }
}