using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

  public float volumeAdjustment = 1;
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void playGameObjectSound(GameObject gameObj, string soundName)
  {
    AudioSource[] sounds = gameObj.transform.GetComponentsInChildren<AudioSource>();
    foreach (AudioSource sound in sounds)
    {
      if (sound.name == soundName)
      {
        sound.Play();
      }
    }
  }
}
