using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]

public class Coin : MonoBehaviour
{
    [SerializeField] private float _cooldownSpawn;

    private UnityEvent _isCollected = new UnityEvent();
    private AudioSource _audioCollect;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioCollect = GetComponent<AudioSource>();
        _audioCollect.loop = false;
    }

    private void OnEnable()
    {
        _isCollected.AddListener(_audioCollect.Play);
        _isCollected.AddListener(HideSprite);
    }

    private void OnDisable()
    {
        _isCollected.RemoveListener(_audioCollect.Play);
        _isCollected.RemoveListener(HideSprite);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out CharacterMover player) && _spriteRenderer.enabled == true)
        {
            _isCollected.Invoke();
            StartCoroutine(Spawning());
        }
    }

    private void HideSprite()
    {
        _spriteRenderer.enabled = false;
    }

    private IEnumerator Spawning()
    {
        yield return new WaitForSeconds(_cooldownSpawn);
        _spriteRenderer.enabled = true;
    }
}
