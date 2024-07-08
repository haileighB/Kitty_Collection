using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemLogic: MonoBehaviour
{
    [SerializeField] AudioSource collectionSound;
    [SerializeField] AudioSource meowSound;
    [SerializeField] AudioSource victorySound;
    [SerializeField] Image fishIcon;
    [SerializeField] Image catIcon;
    [SerializeField] Text catTooltip;
    [SerializeField] Text houseTooltip;
    bool hasFish = false;
    bool hasCat = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fish"))
        {
            GetFish(other);
        }
        else if (other.gameObject.CompareTag("Cat"))
        {
            if (hasFish)
            {
                GetCat(other);
            }
            else
            {
                StartCoroutine(ShowTip(catTooltip));    //display "you need fish to get cat" tip
            }
        }
        else if (other.gameObject.CompareTag("House"))
        {
            if (hasCat)
            {
                StartCoroutine(ProgressLevel());
            }
            else
            {
                StartCoroutine(ShowTip(houseTooltip));  //display "you need cat to finish level" tip
            }
        }
    }

    private void GetFish(Collider other)
    {
        Destroy(other.gameObject);
        hasFish = true;

        fishIcon.gameObject.SetActive(true);    //show fish in backpack

        collectionSound.Play();
        Debug.Log("Fish get");
    }

    private void GetCat(Collider other)
    {
        Destroy(other.gameObject);
        hasCat = true;
        hasFish = false;

        fishIcon.gameObject.SetActive(false);   //hide fish in backpack
        catIcon.gameObject.SetActive(true);     //show cat in backpack

        meowSound.Play();
        Debug.Log("Cat get");
    }

    private IEnumerator ShowTip(Text toolTip)
    {
        toolTip.enabled = true;
        yield return new WaitForSeconds(2);     //show tip for 2 seconds
        toolTip.enabled = false;
    }

    private IEnumerator ProgressLevel()
    {
        victorySound.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   //go to next scene
    }
}
