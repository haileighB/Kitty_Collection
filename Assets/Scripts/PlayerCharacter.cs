using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField] AudioSource deathSound;
    [SerializeField] AudioSource enemyDeathSound;
    [SerializeField] Text[] tutorialTips;
    bool dead = false;

    private void Update()
    {
        //die when fall off
        if (GetComponent<Transform>().position.y < -3f && !dead)
        {
            Die();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy Body"))
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<PlayerControl>().enabled = false;
            Die();
        }

        if (collision.gameObject.CompareTag("Enemy Head"))
        {
            enemyDeathSound.Play();
            Destroy(collision.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Platform 2")){
            StartCoroutine(ShowTip(tutorialTips[0]));   //show snakes tip
        }
        else if(other.gameObject.CompareTag("Platform 4"))
        {
            StartCoroutine(ShowTip(tutorialTips[1]));   //show fish tip
        }
    }

    void Die()
    {
        Invoke(nameof(ReloadLevel), .5f);   //wait before reloading level instead of instant
        dead = true;
        deathSound.Play();
        Debug.Log("Death");
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator ShowTip(Text toolTip)
    {
        toolTip.enabled = true;
        toolTip.GetComponentInChildren<Image>().enabled = true;
        yield return new WaitForSeconds(3);
        toolTip.enabled = false;
        toolTip.GetComponentInChildren<Image>().enabled = false;
    }
}