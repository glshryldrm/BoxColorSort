using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Color color;
    private void Start()
    {
        SetColor(color);
    }
    void SetColor(Color c)
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color = c;
    }
    public void CreateFX()
    {
        GameObject fx = Instantiate(GameAssets.Instance.particlePrefab);
        fx.transform.position = transform.position;
        var mainModule = fx.GetComponent<ParticleSystem>().main;
        mainModule.startColor = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].color;
        GameObject.Destroy(fx, mainModule.startLifetime.constant + 1f);
    }
}
