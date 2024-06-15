using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill2Logic : MonoBehaviour
{
    /*
    Drill2Logic is a copy and paste of Drill1Logic.
    Everything about the two scripts is identical besides the moves array.
    The order of the moves array dictates the paths and moves that are displayed in the drill.
    */
    public GameObject LeftPad;
    public GameObject RightPad;

    private Transform[] paths = new Transform[6];
    private Animator BorisAnimator;
    public GameObject path;

    public AudioSource Boris;
    public AudioClip leftJab;
    public AudioClip rightJab;
    public AudioClip leftHook;
    public AudioClip rightHook;
    public AudioClip leftUpperCut;
    public AudioClip rightUpperCut;
    public AudioClip HapticFeedback;
    public AudioClip PunchSound;

    private AudioClip[] clips = new AudioClip[6];
    private int movesCount;
    private int pathCount;
    private string[] moves = { "LeftHook", "RightJab", "LeftJab", "RightUpperCut", "LeftUpperCut", "RightHook" };


    private void Awake()
    {
        // build the audioclip array
        Boris = GetComponent<AudioSource>();
        clips[0] = leftHook;
        clips[1] = rightJab;
        clips[2] = leftJab;
        clips[3] = rightUpperCut;
        clips[4] = leftUpperCut;
        clips[5] = rightHook;

        //paths = GameObject.Find("Path").GetComponentsInChildren<Transform>(); // initialize the paths array
        Transform[] temp;
        temp = path.GetComponentsInChildren<Transform>();
        int i = 0;
        foreach (Transform t in temp) {
            if (t.parent == path.transform) {
                paths[i] = t;
                paths[i].gameObject.SetActive(false);
                i++;
            }
        }
        Activeness(moves.Length - 1, 0);
        BorisAnimator = GetComponent<Animator>();
        movesCount = 0;
        pathCount = 0;
    }

    private void MovesSwitch()
    {
        // make punch sound
        Boris.PlayOneShot(PunchSound);

        // decide which animation to play next
        if (movesCount == moves.Length - 1) {
            Boris.PlayOneShot(clips[0]);
            BorisAnimator.SetBool(moves[movesCount], false);
            BorisAnimator.SetBool(moves[0], true);
            Activeness(movesCount, 0);
        } else {
            Boris.PlayOneShot(clips[movesCount + 1]);
            BorisAnimator.SetBool(moves[movesCount], false);
            BorisAnimator.SetBool(moves[movesCount + 1], true);
            Activeness(movesCount, movesCount + 1);
        }

        movesCount++;

        /*
        // decide which punch path to show next
        if (pathCount < paths.Length - 1) {
            paths[pathCount].gameObject.SetActive(false);
            paths[pathCount + 1].gameObject.SetActive(true);
            pathCount++;
        } else {
            // turn off the previous strike path and turn on the next strike path
            paths[pathCount].gameObject.SetActive(false);
            paths[0].gameObject.SetActive(true);
            pathCount = 0;
        }
        */

    }

    private void Activeness(int current, int next)
    {
        foreach (Transform p in paths) {
            if (p.gameObject.name == moves[current]) {
                p.gameObject.SetActive(false);
            }

            if (p.gameObject.name == moves[next]) {
                p.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        // if you have reached the end of the array of animations
        if (movesCount > moves.Length - 1) movesCount = 0;
        OVRHapticsClip hapticsClip = new OVRHapticsClip(HapticFeedback);
        // Get Hook edge case
        if (LeftPad.GetComponent<HitOrNah>().rightControllerHit && moves[movesCount] == "RightHook") {
            // haptics
            OVRHaptics.RightChannel.Preempt(hapticsClip);

            MovesSwitch();
        }// get hook edge case
        else if (RightPad.GetComponent<HitOrNah>().leftControllerHit && moves[movesCount] == "LeftHook") {
            // haptics
            OVRHaptics.LeftChannel.Preempt(hapticsClip);

            MovesSwitch();
        } else if (LeftPad.GetComponent<HitOrNah>().leftControllerHit && moves[movesCount][0] == 'L') {
            // haptics
            OVRHaptics.LeftChannel.Preempt(hapticsClip);

            MovesSwitch();
        } else if (RightPad.GetComponent<HitOrNah>().rightControllerHit && moves[movesCount][0] == 'R') {
            // haptics
            OVRHaptics.RightChannel.Preempt(hapticsClip);

            MovesSwitch();
        }
    }
}
