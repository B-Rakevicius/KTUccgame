using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference ambience { get; private set; }

    [field: Header("Music")]
    [field: SerializeField] public EventReference music { get; private set; }

    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootsteps { get; private set; }

    [field: Header("Item SFX")]
    [field: SerializeField] public EventReference itemCollected { get; private set; }
    [field: SerializeField] public EventReference itemIdle { get; private set; }

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
