using Cinemachine;
using UnityEngine;

public class SpriteFlicker : MonoBehaviour
{
    private SpriteRenderer _renderer, _parentRenderer;
    [SerializeField] private float maxAlpha = .5f;
    private float alpha;
    [SerializeField] private float fadeSpeed = .3f;
    
    
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _parentRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    public void Flicker()
    {
        alpha = maxAlpha;
    }

    private void Update()
    {
        _renderer.sprite = _parentRenderer.sprite;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
        
        alpha -= fadeSpeed * maxAlpha * Time.deltaTime;
    }
}
