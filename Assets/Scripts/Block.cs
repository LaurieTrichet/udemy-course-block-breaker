using System;
using UnityEngine;

public class Block : MonoBehaviour
{

    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparkles;
    [SerializeField] Sprite[] hitSprites;

    private GameManager gameManager;

    //Debug
    [SerializeField] int timesHit;
    [SerializeField] int maxHits;

    private void Start()
    {
        timesHit = 0;
        CountBreakableBlocks();
        maxHits = hitSprites.Length + 1;
    }
    

    private void CountBreakableBlocks()
    {
        if (IsBreakable())
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsBreakable())
        {
            OnBlockHit();
        }
    }

    private bool IsBreakable()
    {
        return gameObject.tag == "Breakable";
    }

    private void OnBlockHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {

            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("There is no sprite for " + hitSprites + "at index " + spriteIndex);
        }
    }

    private void DestroyBlock()
    {
        PlaySounds();
        gameManager.BlockDestroyed();
        Destroy(gameObject);
        ShowSparkles();
    }

    private void PlaySounds()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void ShowSparkles()
    {
        var sparkle = Instantiate(blockSparkles, this.transform.position, this.transform.rotation);
        Destroy(sparkle, 2);
    }
}
