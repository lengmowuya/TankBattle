using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject explosionPrefab;

    public Sprite BrokenSprite;
    public AudioClip dieAudio;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Die(){
        PlayerManager.Instance.isDefeat = true;
        Instantiate(explosionPrefab,transform.position,transform.rotation);
        sr.sprite = BrokenSprite;
        AudioSource.PlayClipAtPoint(dieAudio,transform.position);
    }
}
