using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioClip[] characterSelect;
    [SerializeField] private AudioClip[] menuSounds;

    [SerializeField] private GameObject antiHoverPanel;


    private AudioSource audioSource;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    public void PlayCharacterSound(int character) 
    { 
        audioSource.clip = characterSelect[character];
        audioSource.Play();
    }

    public void PlayMenuSounds(int menuSound) 
    {
        audioSource.clip = menuSounds[menuSound];
        audioSource.Play();
    }

    public void TurnButtonInteractabeOn() 
    {
        StartCoroutine(InteractableWait());
    }

    IEnumerator InteractableWait() 
    {
        yield return new WaitForSeconds(1);
        antiHoverPanel.SetActive(false);
    }

}
