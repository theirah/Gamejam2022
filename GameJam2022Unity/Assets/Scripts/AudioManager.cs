using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Toggle muteToggle;

    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioSource menuMusic;
    [SerializeField] AudioSource stage1RedMusic;
    [SerializeField] AudioSource stage2RedMusic;
    [SerializeField] AudioSource stage1WolfMusic;
    [SerializeField] AudioSource stage2WolfMusic;
    [SerializeField] AudioSource stage3BossMusic;

    //"SE" is sound effect

    [SerializeField] AudioSource trapSE;
    [SerializeField] AudioSource bigSwing1SE;
    [SerializeField] AudioSource bowPull1SE;
    [SerializeField] AudioSource bowReleaseSE;
    [SerializeField] AudioSource characterSwitchSE;
    [SerializeField] AudioSource chop1SE;
    [SerializeField] AudioSource deathSquishSE;
    [SerializeField] AudioSource footStep1SE;
    [SerializeField] AudioSource footStep2SE;
    [SerializeField] AudioSource hit1SE;
    [SerializeField] AudioSource hit2SE;
    [SerializeField] AudioSource hit3SE;
    [SerializeField] AudioSource menuSelectSE;
    [SerializeField] AudioSource menuStartSE;
    [SerializeField] AudioSource redDeathSE;
    [SerializeField] AudioSource wolfDeathSE;
    [SerializeField] AudioSource redHahSE;
    [SerializeField] AudioSource redHurt1SE;
    [SerializeField] AudioSource redHurt2SE;
    [SerializeField] AudioSource redHurt3SE;
    [SerializeField] AudioSource redHuwh1SE;
    [SerializeField] AudioSource redJumpSE;
    [SerializeField] AudioSource swing1SE;
    [SerializeField] AudioSource swing2SE;
    [SerializeField] AudioSource swing3SE;
    [SerializeField] AudioSource wolfHoohSE;
    [SerializeField] AudioSource wolfHuhSE;
    [SerializeField] AudioSource wolfHurt1SE;
    [SerializeField] AudioSource wolfHurt2SE;
    [SerializeField] AudioSource wolfHurt3SE;
    [SerializeField] AudioSource wolfJump1SE;
    [SerializeField] AudioSource wolfJump2SE;
    [SerializeField] AudioSource wolfScratch1SE;
    [SerializeField] AudioSource wolfScratch2SE;

    //Needed for playing appropriate music
    private int currentLevel = 1;
    private int maxLevel = 2;       //Maximum non-boss stage
    private bool bossLevel = false;

    private bool fadeMenuMusic = false;

    private bool isRed = true;       //keeps track of current character

    private int fadeCounter = 0;        //keeps counting each frame until fade in/fade out is finished
    private int fadeCounterMax = 100;   //when to stop the fade

    private float musicDefaultVolume = 1f;  //Needs to be balanced to sound effect levels

    public enum soundEffect
    {
        TRAP, BOWPULL, BOWRELEASE, CHARSWITCH, CHOP, DEATHSQUISH, PLAYERDEATH,
        FOOTSTEP, HIT, MENUMOVE, MENUSTART, YELL, HURT, JUMP, SWING, SCRATCH
    };          //Chooses type of sound effect to play

    bool switchToRed = false;       //triggered once during character switch then reset
    bool switchToWolf = false;

    bool switchingToRed = false;    //active during character switching and music crossfade
    bool switchingToWolf = false;

    bool advancingLevel = false;    //active while fading out level music to start next level

    //Starts playing menu music
    private void Awake()
    {
        if (menuMusic)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                MusicVolumeReset();
                StartStage1();
            }
        }
    }
    //Controls music fading events when switching characters
    private void Update()
    {
        if (switchToRed)
        {
            MusicVolumeReset();
            fadeCounter = 0;
            switchingToRed = true;
            switchToRed = false;
        }
        if (switchToWolf)
        {
            fadeCounter = 0;
            switchingToWolf = true;
            switchToWolf = false;
        }
        if (switchingToRed || switchingToWolf || advancingLevel)
        {
            FadeMusic();
        }
    }
    //Starts first level music
    public void PlayGame()
    {
        fadeMenuMusic = true;
        advancingLevel = true;
        PlaySoundEffect(soundEffect.MENUSTART);
    }
    private void StartStage1()
    {
        if (menuMusic)
            menuMusic.Stop();
        if (stage1RedMusic)
            stage1RedMusic.Play();
        if (stage1WolfMusic)
            stage1WolfMusic.Play();
    }
    //Fade music between levels
    public void StageEnd()
    {
        advancingLevel = true;
    }

    //Triggers sound and music events when switching characters
    public void SwitchToRed()
    {
        MusicVolumeReset();
        isRed = true;
        switchToRed = true;
        PlaySoundEffect(soundEffect.CHARSWITCH);
    }
    public void SwitchToWolf()
    {
        MusicVolumeReset();
        isRed = false;
        switchToWolf = true;
        PlaySoundEffect(soundEffect.CHARSWITCH);
    }
    //Triggers sound effects
    public void PlaySoundEffect(soundEffect soundEffectToPlay)
    {
        if (soundEffectToPlay == soundEffect.TRAP && trapSE)
        {
            if (!trapSE.isPlaying)
                trapSE.Play();
        }
        else if (soundEffectToPlay == soundEffect.BOWPULL && bowPull1SE)
        {
            if (!bowPull1SE.isPlaying)
                bowPull1SE.Play();
        }
        else if (soundEffectToPlay == soundEffect.BOWRELEASE && bowReleaseSE)
        {
            if (!bowReleaseSE.isPlaying)
                bowReleaseSE.Play();
        }
        else if (soundEffectToPlay == soundEffect.CHARSWITCH && characterSwitchSE)
        {
            {
                //Switch to wolf or red's music also with the crossfade
                if (!characterSwitchSE.isPlaying)
                    characterSwitchSE.Play();
            }
        }
        else if (soundEffectToPlay == soundEffect.CHOP && chop1SE)
        {
            if (!chop1SE.isPlaying)
                chop1SE.Play();
        }
        else if (soundEffectToPlay == soundEffect.DEATHSQUISH && deathSquishSE)
        {
            if (!deathSquishSE.isPlaying)
                deathSquishSE.Play();
        }
        else if (soundEffectToPlay == soundEffect.FOOTSTEP && footStep1SE)
        {
            if (!footStep1SE.isPlaying)
                footStep1SE.Play();
        }
        else if (soundEffectToPlay == soundEffect.HIT && hit1SE)
        {
            if (!hit1SE.isPlaying)
                hit1SE.Play();
        }
        else if (soundEffectToPlay == soundEffect.MENUMOVE && menuSelectSE)
        {
            if (!menuSelectSE.isPlaying)
                menuSelectSE.Play();
        }
        else if (soundEffectToPlay == soundEffect.MENUSTART && menuStartSE)
        {
            if (!menuStartSE.isPlaying)
                menuStartSE.Play();
        }

        else if (soundEffectToPlay == soundEffect.SWING && swing1SE)
        {
            if (!swing1SE.isPlaying)
                swing1SE.Play();
        }
        else if (soundEffectToPlay == soundEffect.SCRATCH && wolfScratch1SE)
        {
            if (!wolfScratch1SE.isPlaying)
                wolfScratch1SE.Play();
        }

        if (isRed)
        {

            if (soundEffectToPlay == soundEffect.YELL && redHuwh1SE)
            {
                if (!redHuwh1SE.isPlaying)
                    redHuwh1SE.Play();
            }

            else if (soundEffectToPlay == soundEffect.HURT && redHurt1SE)
            {
                if (!redHurt1SE.isPlaying)
                    redHurt1SE.Play();
            }
            else if (soundEffectToPlay == soundEffect.PLAYERDEATH && redDeathSE)
            {
                if (!redDeathSE.isPlaying)
                    redDeathSE.Play();
            }
            else if (soundEffectToPlay == soundEffect.JUMP && redJumpSE)
            {
                if (!redJumpSE.isPlaying)
                    redJumpSE.Play();
            }
        }
        else
        {
            if (soundEffectToPlay == soundEffect.YELL && wolfHoohSE)
            {
                if (!wolfHoohSE.isPlaying)
                    wolfHoohSE.Play();
            }

            else if (soundEffectToPlay == soundEffect.HURT && wolfHurt1SE)
            {
                if (!wolfHurt1SE.isPlaying)
                    wolfHurt1SE.Play();
            }
            else if (soundEffectToPlay == soundEffect.PLAYERDEATH && deathSquishSE)
            {
                if (!deathSquishSE.isPlaying)
                    deathSquishSE.Play();
            }
            else if (soundEffectToPlay == soundEffect.JUMP && wolfJump1SE)
            {
                if (!wolfJump1SE.isPlaying)
                    wolfJump1SE.Play();
            }
        }

    }

    // Controls menu UI for changing sound volume
    public void MuteAll()
    {
        float volume = volumeSlider.value;
        float attenuationValue = Mathf.Log10(volume) * 20;
        float musicVolume = musicVolumeSlider.value;
        float attenuationValueMusic = Mathf.Log10(musicVolume) * 20;

        if (muteToggle.isOn)
        {
            audioMixer.SetFloat("musicVol", -1000000000f);
            audioMixer.SetFloat("soundFXVol", -1000000000f);
        }
        else
        {
            audioMixer.SetFloat("musicVol", attenuationValueMusic);
            audioMixer.SetFloat("soundFXVol", attenuationValue);
        }

    }
    public void AdjustMusicVolume()
    {
        muteToggle.isOn = false;
        float volume = musicVolumeSlider.value;
        float attenuationValue = Mathf.Log10(volume) * 20f;
        if (musicVolumeSlider.value < .01f)
            attenuationValue = -1000f;

        audioMixer.SetFloat("musicVol", attenuationValue);
    }
    public void AdjustVolume()
    {
        muteToggle.isOn = false;
        float volume = volumeSlider.value;
        float attenuationValue = Mathf.Log10(volume) * 20f;
        if (volumeSlider.value < .01f)
            attenuationValue = -1000f;

        audioMixer.SetFloat("soundFXVol", attenuationValue);
        menuSelectSE.Play();
    }

    private void AdvanceLevel()
    {
        currentLevel++;
        if (currentLevel > maxLevel)
        {
            bossLevel = true;
            stage3BossMusic.Play();
        }
        else
        {
            if(currentLevel == 1)
            {
                stage1RedMusic.Play();
                stage1WolfMusic.Play();
            }
            else if (currentLevel == 2)
            {
                stage2RedMusic.Play();
                stage2WolfMusic.Play();
            }
            bossLevel = false;
        }
        
    }

    private void MusicVolumeReset()
    {
        menuMusic.volume = musicDefaultVolume;
        if(stage3BossMusic)
            stage3BossMusic.volume = musicDefaultVolume;
        if (isRed)
        {
            stage1RedMusic.volume = musicDefaultVolume;
            stage2RedMusic.volume = musicDefaultVolume;
            stage1WolfMusic.volume = 0f;
            stage2WolfMusic.volume = 0f;
        }
        else
        {
            stage1RedMusic.volume = 0f;
            stage2RedMusic.volume = 0f;
            stage1WolfMusic.volume = musicDefaultVolume;
            stage2WolfMusic.volume = musicDefaultVolume;
        }

        //Resets volume of music back to default
    }

    private void FadeMusic()
    {
        //Amount to increment volume.  Default is .01f
        float volumeIncrement = musicDefaultVolume / (fadeCounterMax * 1f);
        float volumeIncrementMax = musicDefaultVolume - volumeIncrement;
        fadeCounter++;

        if(advancingLevel)
        {
            if (fadeCounter >= fadeCounterMax)
            {
                if (fadeMenuMusic)
                {
                    menuMusic.Stop();
                    fadeMenuMusic = false;
                    advancingLevel = false;
                }

                if (bossLevel)
                    stage3BossMusic.Stop();
                else if (currentLevel == 1)
                {
                    stage1RedMusic.Stop();
                    stage1WolfMusic.Stop();
                }
                else if (currentLevel == 2)
                {
                    stage2RedMusic.Stop();
                    stage2WolfMusic.Stop();
                }
                advancingLevel = false;
                AdvanceLevel();

            }

            else if (fadeMenuMusic)
            {
                if (menuMusic.volume >= volumeIncrement)
                    menuMusic.volume -= volumeIncrement;
            }

            else if (bossLevel && stage3BossMusic)
            {
                {
                    if (stage3BossMusic.volume >= volumeIncrement)
                        stage3BossMusic.volume -= volumeIncrement;
                }
            }
            else if (isRed)
            {
                if (currentLevel == 1 && stage1RedMusic.volume >= volumeIncrement)
                    stage1RedMusic.volume -= volumeIncrement;
                else if (currentLevel == 2 && stage2RedMusic.volume >= volumeIncrement)
                    stage2RedMusic.volume -= volumeIncrement;
            }
            else if (!isRed)
            {
                if (currentLevel == 1 && stage1RedMusic.volume >= volumeIncrement)
                    stage1WolfMusic.volume -= volumeIncrement;
                else if (currentLevel == 2 && stage2RedMusic.volume >= volumeIncrement)
                    stage2WolfMusic.volume -= volumeIncrement;
            }

        }

        else if(switchingToRed && !bossLevel)
        {
            if (!bossLevel)
            {
                if (currentLevel == 1 && stage1RedMusic && stage1WolfMusic)
                {
                    if (stage1RedMusic.volume <= volumeIncrementMax)
                        stage1RedMusic.volume += volumeIncrement;
                    if (stage1WolfMusic.volume >= volumeIncrement)
                        stage1WolfMusic.volume -= volumeIncrement;
                }
                else if (currentLevel == 2 && stage2RedMusic && stage2WolfMusic)
                {
                    if (stage2RedMusic.volume <= volumeIncrementMax)
                        stage2RedMusic.volume += volumeIncrement;
                    if (stage2WolfMusic.volume >= volumeIncrement)
                        stage2WolfMusic.volume -= volumeIncrement;
                }
            }
        }
        else if (switchingToWolf && !bossLevel)
        {
            if (currentLevel == 1 && stage1RedMusic && stage1WolfMusic)
            {
                if (stage1RedMusic.volume >= volumeIncrement)
                    stage1RedMusic.volume -= volumeIncrement;
                if (stage1WolfMusic.volume <= volumeIncrementMax)
                    stage1WolfMusic.volume += volumeIncrement;
            }
            else if (currentLevel == 2 && stage2RedMusic && stage2WolfMusic)
            {
                if (stage2RedMusic.volume >= volumeIncrement)
                    stage2RedMusic.volume -= volumeIncrement;
                if (stage2WolfMusic.volume <= volumeIncrementMax)
                    stage2WolfMusic.volume += volumeIncrement;
            }
        }
        else if(!switchingToWolf && ! switchingToRed)     //Fade out music at end of level
        {
            if (currentLevel == 1 && stage1RedMusic && stage1WolfMusic)
            {
                if (stage1RedMusic.volume >= volumeIncrement)
                    stage1RedMusic.volume -= volumeIncrement;
                if (stage1WolfMusic.volume >= volumeIncrement)
                    stage1WolfMusic.volume -= volumeIncrement;
            }
            else if (currentLevel == 2 && stage2RedMusic && stage2WolfMusic)
            {
                if (stage2RedMusic.volume >= volumeIncrement)
                    stage2RedMusic.volume -= volumeIncrement;
                if (stage2WolfMusic.volume >= volumeIncrement)
                    stage2WolfMusic.volume -= volumeIncrement;
            }
        }

        if(fadeCounter > fadeCounterMax)
        {
            switchingToRed = false;
            switchingToWolf = false;
            fadeCounter = 0;
        }

    }



    }
