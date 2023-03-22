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
    public AudioClip[] UI_Audio; // 0 Ÿ��BGM 1 ����BGM 2 ���ڼҸ� 3 �״¼Ҹ� 4 �������� 5 �κ��丮 ����
    // 6 ���� ���̵� 7 �κ� BGM 8 ���ĸԴ¼Ҹ� 9 ���Ļ���BGM 10 ������ȹ�� 11 ����BGM 12 ���θԴ¼Ҹ�

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
