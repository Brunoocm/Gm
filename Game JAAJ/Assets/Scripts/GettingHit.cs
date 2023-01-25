using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHit : MonoBehaviour
{
    [Header("Normal Bullet")]
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;

    [Header("Freeze Bullet")]
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material freezeFlashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float freezeDuration;

    private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine flashRoutine;
    private Coroutine freezeRoutine;

    private bool cdCoroutine;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Bullet") && !cdCoroutine)
        {
            if(other.GetComponent<BulletScript>().hasFreeze)
            {
                freezeDuration = other.GetComponent<BulletScript>().freezeDuration;
                FreezeFlash();
            }
            else if(!cdCoroutine)
            {
                Flash();
            }
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
        {

            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }
    public void FreezeFlash()
    {
        if (freezeRoutine != null)
        {

            StopCoroutine(freezeRoutine);
        }

        freezeRoutine = StartCoroutine(FreezeRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }
    private IEnumerator FreezeRoutine()
    {
        cdCoroutine = true;

        spriteRenderer.material = freezeFlashMaterial;

        yield return new WaitForSeconds(freezeDuration);

        spriteRenderer.material = originalMaterial;

        cdCoroutine = false;

        flashRoutine = null;
    }
}
