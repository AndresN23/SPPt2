using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootOffsetTransform;
    public float moveSpeed = 5;
    private bool isDead = false;

    public AudioClip deathSFX;
    public AudioClip shootSFX;
    private AudioSource audioSource;

    Animator anim;
    void Start()
    {
        // todo - get and cache animator
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound(){
        if (audioSource != null && deathSFX != null){
            audioSource.PlayOneShot(deathSFX);
        }
    }

    public void PlayShootSound(){
        if(audioSource != null && shootSFX != null){
            audioSource.PlayOneShot(shootSFX);
        }
    }
    
    void Update()
    {

        if (isDead) return;

        float move = 0;
        if (Keyboard.current.aKey.isPressed){
            move = -1;
        }

        if (Keyboard.current.dKey.isPressed){
            move = 1;
        }

        transform.position += Vector3.right * move * moveSpeed * Time.deltaTime;

        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            GameObject shot = Instantiate(bulletPrefab, shootOffsetTransform.position, Quaternion.identity);
            Debug.Log("Bang!");

            // todo - destroy the bullet after 3 seconds
            Destroy(shot, 3f);
            // todo - trigger shoot animation

            GetComponent<Animator>().SetTrigger("shoot trigger");
            
        }

     }

     
void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
        collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
    {
        StartCoroutine(Die());
    }
}

    IEnumerator Die(){
        if (isDead) yield break;
        Debug.Log("Player dying animation triggered");

        isDead = true;
        anim.SetTrigger("die");
        yield return new WaitForSeconds(5.0f);
        GameManager.Instance.PlayerDied();
        }

}
 