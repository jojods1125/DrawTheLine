using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sndManager : MonoBehaviour
{
    public AudioClip[] sndMusic;
    public AudioClip[] sndSFXClips;
    public AudioClip[] marker_players;
    public AudioClip[] marker_prompt;
    public AudioClip[] pageslide;
    public AudioClip[] sndVOClips;

    [HideInInspector]
    public enum SFX { CartIn, CartOut, DrumRoll4s, SwitchOff, SwitchOn, MarkerLine, MarkerPlayer, MarkerPrompt, PageSlide, ScreenDown, ScreenUp, TimerEnd };
    [HideInInspector]
    public enum MUS { Lobby, Host, Waiting, FinalScores }

    public enum VO { IntroNotPlayed, IntoPlayed, Part1Start1, Part1Start2, Part1Start3, Part1After, Part1Reminder, Part1Finish, Part2Start, Part2Reminder, Part2Finish, Part3Start, Part3Finish, Scores1a, Scores1b, Scores1c, Scores2a, Scores2b, Scores2c, Scores3a, Scores3b, PostGame}

    private AudioSource[] sndSourceCh = new AudioSource[4];

    private void InitSources()
    {
        sndSourceCh[0] = GetComponents<AudioSource>()[0]; //channel 0 is for music
        sndSourceCh[1] = GetComponents<AudioSource>()[1]; //channel 1 is for voiceover
        sndSourceCh[2] = GetComponents<AudioSource>()[2]; //channel 2 is for gameplay sfx
        sndSourceCh[3] = GetComponents<AudioSource>()[3]; //channel 3 is for background sfx

        sndSourceCh[0].volume = 0.25f; //hardcoded music volume because the scene was being edited elsewhere
        sndSourceCh[0].loop = true;
    }

    public void PlaySFX(SFX name)
    {
        //only the host can change sounds
        if (!GameManager.Instance.isHost)
            return;

        //switch case to hardcode which channel to use for each sfx
        switch (name)
        {
            case SFX.CartIn:
                PlaySoundSource(sndSFXClips[(int)SFX.CartIn], 3, true);
                break;
            case SFX.CartOut:
                PlaySoundSource(sndSFXClips[(int)SFX.CartOut], 3, true);
                break;
            case SFX.DrumRoll4s:
                PlaySoundSource(sndSFXClips[(int)SFX.DrumRoll4s], 2, true);
                break;
            case SFX.SwitchOff:
                PlaySoundSource(sndSFXClips[(int)SFX.SwitchOff], 3, true);
                break;
            case SFX.SwitchOn:
                PlaySoundSource(sndSFXClips[(int)SFX.SwitchOn], 3, true);
                break;
            case SFX.MarkerLine:
                PlaySoundSource(sndSFXClips[(int)SFX.MarkerLine], 2, true);
                break;
            case SFX.MarkerPlayer:
                PlaySoundSource(marker_players[Random.Range(0, marker_players.Length - 1)], 2, true); //pick random sound
                break;
            case SFX.MarkerPrompt:
                PlaySoundSource(marker_prompt[Random.Range(0, marker_prompt.Length - 1)], 2, true); //pick random sound
                break;
            case SFX.PageSlide:
                PlaySoundSource(pageslide[Random.Range(0, pageslide.Length - 1)], 3, true); //pick random sound
                break;
            case SFX.ScreenDown:
                PlaySoundSource(sndSFXClips[(int)SFX.ScreenDown], 3, true);
                break;
            case SFX.ScreenUp:
                PlaySoundSource(sndSFXClips[(int)SFX.ScreenUp], 3, true);
                break;
            case SFX.TimerEnd:
                PlaySoundSource(sndSFXClips[(int)SFX.TimerEnd], 3, true);
                break;
        }
    }

    public void PlayMusic(MUS state)
    {
        //only the host can change music
        if (!GameManager.Instance.isHost)
            return;

        PlaySoundSource(sndMusic[(int)state], 0, false);

        //switch (state)
        //{
        //    case MUS.Lobby:
        //        PlaySoundSource(sndMusic[0], 0, false);
        //        break;
        //    case MUS.Host:
        //        PlaySoundSource(sndMusic[1], 0, false);
        //        break;
        //    case MUS.Waiting:
        //        PlaySoundSource(sndMusic[2], 0, false);
        //        break;
        //    case MUS.FinalScores:
        //        PlaySoundSource(sndMusic[3], 0, false);
        //        break;
        //}
    }

    public void StopMusic()
    {
        sndSourceCh[0].Stop();
    }

    public void PlayVO (VO vo)
    {
        //only the host can change sounds
        if (!GameManager.Instance.isHost)
            return;

        PlaySoundSource(sndVOClips[(int)vo], 1, true);
    }

    void PlaySoundSource(AudioClip clip, int channel, bool overwrite)
    {
        //initialize sound sources when first calling them
        if(sndSourceCh[channel] == null)
            InitSources();

        if (!overwrite)
        {
            //dont play sound if it is already playing
            if (sndSourceCh[channel].clip == clip)
                return;
        }

        sndSourceCh[channel].Stop();
        sndSourceCh[channel].clip = clip;
        sndSourceCh[channel].Play();
    }
}
