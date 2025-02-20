using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    private SpriteRenderer _renderer;
    [SerializeField] private float maxAlpha = .5f;
    private float alpha;
    [SerializeField] private float fadeSpeed = .3f;
    
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    public void Flicker()
    {
        alpha = maxAlpha;
    }

    private void FixedUpdate()
    {
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
        
        alpha -= fadeSpeed * maxAlpha;
    }
}
