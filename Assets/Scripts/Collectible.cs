using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Collectible : MonoBehaviour
{
    public int collectibleScore = 10;
    public GameObject mesh;
    public ParticleSystem dieParticle;
    private SphereCollider sCollider;
    [HideInInspector]public CollectibleSpawner spawner;

    private void Start()
    {
        spawner = GetComponentInParent<CollectibleSpawner>();
        sCollider = GetComponentInChildren<SphereCollider>();
    }
    public void Die()
    {
        sCollider.enabled = false;
        dieParticle.Play();
        Taptic.Light();
        transform.DOScale(1.2f, 0.5f).OnComplete(()=> {
            transform.localScale = Vector3.one;
            spawner.AddToPool(this);
            mesh.SetActive(false);
            sCollider.enabled = true;
        });
    }

    public void Spawn(Vector3 pos)
    {
        dieParticle.Stop();
        mesh.SetActive(true);
        transform.position = pos;
    }
}
