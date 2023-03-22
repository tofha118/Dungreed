using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource bgmSource;
    public AudioSource effectSource;

    public AudioClip HitAudio;
    public AudioClip[] boss_Audioclip; // 0 : BGM, 1 : LAUGH , 2 : BULLET, 3 : LASER
    public AudioClip[] SkelDog_Audio; //0 Die , 1 Attack
    public AudioClip[] AbyssGuardian_Audio; //0 Die , 1 Attack , 2 AttackImage
    public AudioClip[] Arsha_Audio; //0 Die , 1 Attack , 2 AttackImage
    public AudioClip[] Player_Audio; // 0 walk , 1 Dash , 2 Hit
    public AudioClip[] Player_Weapon; // 0 Shortsworld , 1 shotgun
    public AudioClip[] UI_Audio; // 0 타운BGM 1 던전BGM 2 상자소리 3 죽는소리 4 던전입장 5 인벤토리 오픈
    // 6 던전 방이동 7 로비 BGM 8 음식먹는소리 9 음식상점BGM 10 아이템획득 11 상점BGM 12 코인먹는소리

    public float bgmSave;
    public float effectSave;

    [SerializeField]
    private Slider bgmSlider;

    [SerializeField]
    private Slider effectSlider;

    void Start()
    {
        if(PlayerPrefs.HasKey("bgmVolume"))
        {
            bgmSave = PlayerPrefs.GetFloat("bgmVolume");

            bgmSource.volume = bgmSave;
        }
        if (PlayerPrefs.HasKey("effectVolume"))
        {
            effectSave = PlayerPrefs.GetFloat("effectVolume");

            effectSource.volume = effectSave;
        }
    }

    void Volume_Update()
    {
        if(bgmSlider != null)
        {
            bgmSave = bgmSlider.value;
            bgmSource.volume = bgmSave;
        }
        if(effectSlider != null)
        {
            effectSave = effectSlider.value;
            effectSource.volume = effectSave;
        }
    }

    public void Volume_Save()
    {
        PlayerPrefs.SetFloat("bgmVolume", bgmSave);
        PlayerPrefs.SetFloat("effectVolume", effectSave);
    }

    void Update()
    {
        Volume_Update();
    }
}
