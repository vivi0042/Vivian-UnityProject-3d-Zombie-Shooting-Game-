using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SceneManagement; 


public class Player : MonoBehaviour
{
    public int HP = 100;

    public GameObject gameOverUI;

    public bool isDead;

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");
            PlayerDead();
            isDead = true;
        }
        else 
        {
            print("Player Hit");
        }
    }

    private void PlayerDead()
    {
        GetComponent<PlayerMotor>().enabled = false;

        //dying animation
        GetComponentInChildren<Animator>().enabled = true;

        GetComponent<ScreenBlackOut>().StartFade();
        StartCoroutine(ShowGameOverUI());

    }

    private IEnumerator ShowGameOverUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(3f);

        // Unlock and show the cursor
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;

        SceneManager.LoadScene("MainMenu");
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if(isDead==false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
            
        }

    }
}
