using UnityEngine;
using System.Collections;


public class Enemy3 : MonoBehaviour
{
    public AudioClip ticClip;
    public AudioClip tacClip;

    public AudioClip deathSFX;
    public AudioClip shootSFX;

    public delegate void EnemyDieFunc(float points);
    public static event EnemyDieFunc onEnemyDied;

    private Animator anim;
    private bool isDead;
private AudioSource audioSource;
    void Start (){
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Ouch!");
        if (isDead) return;
        
        // todo - destroy the bullet
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Destroy(collision.gameObject);
            StartCoroutine(Die());
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.PlayerDied();
        }
        // todo - trigger death animation
    }

    public void PlayTicSound(){
        Debug.Log("tic");
        GetComponent<AudioSource>().PlayOneShot(ticClip);
    }
    public void PlayTacSound(){
        Debug.Log("Tac");
         GetComponent<AudioSource>().PlayOneShot(tacClip);
    }

    public void PlayDeathSound(){
        if (audioSource != null && deathSFX != null)
        {
            audioSource.PlayOneShot(deathSFX);
        }
        
    }

    public void PlayShootSound(){
        if (audioSource != null && deathSFX != null){
            audioSource.PlayOneShot(shootSFX);
        }
        
    }

    IEnumerator Die(){

        isDead = true;
        anim.SetTrigger("Die");

        yield return new WaitForSeconds(0.5f);

        onEnemyDied?.Invoke(30);

        Destroy(gameObject);
    }
}
