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

    public enum VO { IntroNotPlayed, IntoPlayed, Part1Start1, Part1Start2, Part1Start3, Part1After, Part1Reminder, Part1Finish, Part2Start, Part2Reminder, Part2Finish, Part3Start, Part3Finish, Scores1, Scores2, Scores3, PostGame}

    private AudioSource[] sndSourceCh = new AudioSource[4];

    // Start is called before the first frame update
    void Start()
    {        
        //only the host can change sounds
        if (!GameManager.Instance.isHost)
            return;

        sndSourceCh[0] = GetComponents<AudioSource>()[0]; //channel 0 is for music
        sndSourceCh[1] = GetComponents<AudioSource>()[1]; //channel 1 is for voiceover
        sndSourceCh[2] = GetComponents<AudioSource>()[2]; //channel 2 is for gameplay sfx
        sndSourceCh[3] = GetComponents<AudioSource>()[3]; //channel 3 is for background sfx
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
                PlaySoundSource(sndSFXClips[(int)SFX.CartIn], 3);
                break;
            case SFX.CartOut:
                PlaySoundSource(sndSFXClips[(int)SFX.CartOut], 3);
                break;
            case SFX.DrumRoll4s:
                PlaySoundSource(sndSFXClips[(int)SFX.DrumRoll4s], 2);
                break;
            case SFX.SwitchOff:
                PlaySoundSource(sndSFXClips[(int)SFX.SwitchOff], 3);
                break;
            case SFX.SwitchOn:
                PlaySoundSource(sndSFXClips[(int)SFX.SwitchOn], 3);
                break;
            case SFX.MarkerLine:
                PlaySoundSource(sndSFXClips[(int)SFX.MarkerLine], 2);
                break;
            case SFX.MarkerPlayer:
                PlaySoundSource(marker_players[Random.Range(0, marker_players.Length - 1)], 2); //pick random sound
                break;
            case SFX.MarkerPrompt:
                PlaySoundSource(marker_prompt[Random.Range(0, marker_prompt.Length - 1)], 2); //pick random sound
                break;
            case SFX.PageSlide:
                PlaySoundSource(pageslide[Random.Range(0, pageslide.Length - 1)], 3); //pick random sound
                break;
            case SFX.ScreenDown:
                PlaySoundSource(sndSFXClips[(int)SFX.ScreenDown], 3);
                break;
            case SFX.ScreenUp:
                PlaySoundSource(sndSFXClips[(int)SFX.ScreenUp], 3);
                break;
            case SFX.TimerEnd:
                PlaySoundSource(sndSFXClips[(int)SFX.TimerEnd], 3);
                break;
        }
    }

    public void PlayMusic(GameState state)
    {
        //only the host can change music
        if (!GameManager.Instance.isHost)
            return;

        switch (state)
        {
            case GameState_Title:
                PlaySoundSource(sndMusic[(int)MUS.Lobby], 0, false);
                break;
            case GameState_Matchmaking:
                PlaySoundSource(sndMusic[(int)MUS.Lobby], 0, false);
                break;
            case GameState_HLobby:
                PlaySoundSource(sndMusic[(int)MUS.Lobby], 0, false);
                break;
            case GameState_HIntro:
                PlaySoundSource(sndMusic[(int)MUS.Lobby], 0, false);
                break;
            case GameState_HPrompt:
                PlaySoundSource(sndMusic[(int)MUS.Host], 0, false);
                break;
            case GameState_HPostAnswers:
                PlaySoundSource(sndMusic[(int)MUS.Host], 0, false);
                break;
            case GameState_HSpectrum:
                PlaySoundSource(sndMusic[(int)MUS.Host], 0, false);
                break;
            case GameState_HResults:
                PlaySoundSource(sndMusic[(int)MUS.Host], 0, false);
                break;
            case GameState_HCurrentScores:
                PlaySoundSource(sndMusic[(int)MUS.Host], 0, false);
                break;
            case GameState_HFinalScores:
                PlaySoundSource(sndMusic[(int)MUS.FinalScores], 0, false);
                break;
            case GameState_HPostGame:
                PlaySoundSource(sndMusic[(int)MUS.FinalScores], 0, false);
                break;
            case GameState_HWait:
                PlaySoundSource(sndMusic[(int)MUS.Waiting], 0, false);
                break;
        }
    }

    public void PlayVO (VO vo)
    {
        //only the host can change sounds
        if (!GameManager.Instance.isHost)
            return;

        PlaySoundSource(sndVOClips[(int)vo], 1);
    }

    void PlaySoundSource(AudioClip clip, int channel, bool overwrite = true)
    {
        if (!overwrite)
        {   //dont play sound if it is already playing
            if (sndSourceCh[channel].clip == clip)
                    return;
        }

        sndSourceCh[channel].Stop();
        sndSourceCh[channel].clip = clip;
        sndSourceCh[channel].Play();
    }
}
