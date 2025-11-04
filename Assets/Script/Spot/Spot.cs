using System;
using UnityEngine;

//perching Spots
public class Spot : MonoBehaviour
{
    public bool IsOccupied { get; private set; } = false;

    public void Occupy() => IsOccupied = true;
    public void Vacate() => IsOccupied = false;
}