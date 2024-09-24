using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class Item : MonoBehaviour
{
    private SpriteRenderer visual;
    private ParticleSystem collectParticle;
    private bool collected = false;

    private StudioEventEmitter emitter;

    private void Awake() 
    {
        visual = this.GetComponentInChildren<SpriteRenderer>();
        collectParticle = this.GetComponentInChildren<ParticleSystem>();
        collectParticle.Stop();
    }

    private void Start()
    {
        emitter = AudioManager.instance.InitializeEventEmitter(FMODEvents.instance.itemIdle, this.gameObject);
        emitter.Play();
    }

    private void OnTriggerEnter2D() 
    {
        if (!collected) 
        {
            collectParticle.Play();
            CollectItem();
        }
    }

    private void CollectItem() 
    {
        collected = true;
        visual.gameObject.SetActive(false);

        emitter.Stop();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.itemCollected, this.transform.position);

        GameEventsManager.instance.ItemCollected();
    }

}
