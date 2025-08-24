using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;
    AudioSource audioPlayer;

    [SerializeField] AudioClip keyboard1;
    [SerializeField] AudioClip keyboard2;
    [SerializeField] AudioClip keyboard3;
    [SerializeField] AudioClip buy;
    [SerializeField] AudioClip sell;
    [SerializeField] AudioClip newBanner;
    [SerializeField] AudioClip purchaseFail;
    [SerializeField] AudioClip rollTick;
    [SerializeField] AudioClip rollFinal;

    private void Awake()
    {
        Instance = this;
        audioPlayer = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        int option = Random.Range(0, 2);
        switch (option)
        {
            case 0:
                audioPlayer.PlayOneShot(keyboard1);
                break;
            case 1:
                audioPlayer.PlayOneShot(keyboard2);
                break;
            case 2:
                audioPlayer.PlayOneShot(keyboard3);
                break;
        }
    }

    public void PlayBuy()
    {
        audioPlayer.PlayOneShot(buy);
    }

    public void PlaySell()
    {
        audioPlayer.PlayOneShot(sell);
    }

    public void PlayNewBanner()
    {
        audioPlayer.PlayOneShot(newBanner);
    }

    public void PlayPurchaseFail()
    {
        audioPlayer.PlayOneShot(purchaseFail);
    }

    public void PlayRollTick()
    {
        audioPlayer.PlayOneShot(rollTick);
    }

    public void PlayRollFinal()
    {
        audioPlayer.PlayOneShot(rollFinal);
    }
}
