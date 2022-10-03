using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OnKeyPressBehaviour : MonoBehaviour
{
    [SerializeField] private RatPlayer ratPlayer;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip violinSFX;
    [SerializeField] private AudioClip tromboneSFX;
    [SerializeField] private AudioClip triangleSFX;
    [SerializeField] private AudioClip recorderSFX;
    [SerializeField] private AudioClip cymbalSFX;

    [SerializeField] private List<KeyCode> violinKeys;
    [SerializeField] private List<KeyCode> tromboneKeys;
    [SerializeField] private List<KeyCode> triangleKeys;
    [SerializeField] private List<KeyCode> recorderKeys;
    [SerializeField] private List<KeyCode> cymbalKeys;

    private Dictionary<AudioClip, List<KeyCode>> keyCodes = new Dictionary<AudioClip, List<KeyCode>>();

    [Header("Musicians Change")]
    [SerializeField] private Sprite[] musiciansNeutral;
    [SerializeField] private Sprite[] musiciansError;
    [SerializeField] private SpriteRenderer[] musicianSpriteRenders;
    [SerializeField] private KeyCode[] keyboardKeys;


    private void Start()
    {
        keyCodes.Add(violinSFX, violinKeys);
        keyCodes.Add(tromboneSFX, tromboneKeys);
        keyCodes.Add(triangleSFX, triangleKeys);
        keyCodes.Add(recorderSFX, recorderKeys);
        keyCodes.Add(cymbalSFX, cymbalKeys);
    }

    private void OnGUI()
    {
        Event keyPressed = Event.current;
        if (keyPressed.isKey)
        {
            foreach (KeyValuePair<AudioClip, List<KeyCode>> key in keyCodes)
            {
                foreach (KeyCode value in key.Value)
                {
                    if (keyPressed.keyCode == value)
                    {
                        //if wrong key press
                        if (keyPressed.keyCode != ratPlayer.GetLeftKey() &&
                            keyPressed.keyCode != ratPlayer.GetRightKey() &&
                            keyPressed.keyCode != ratPlayer.GetJumpKey())
                        {
                            audioSource.clip = key.Key;
                            audioSource.Play();

                            for (int i = 0; i < keyboardKeys.Length; i++) 
                            {
                                if (keyPressed.keyCode == keyboardKeys[i]) 
                                {
                                    musicianSpriteRenders[i].sprite = musiciansError[i];
                                    StartCoroutine(SwitchBackSprite(i));
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator SwitchBackSprite(int index) 
    {
        yield return new WaitForSeconds(0.5f);
        musicianSpriteRenders[index].sprite = musiciansNeutral[index];
    }
}
