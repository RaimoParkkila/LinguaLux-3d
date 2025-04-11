    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    //using UnityEngine.ProBuilder.MeshOperations;

    public class MuteAudio : MonoBehaviour
    {
        // Start is called before the first frame update
       public void  MuteToggle(bool muted)
        {
            if (muted)
            {
                AudioListener.volume = 0;
                Debug.Log("A");
        
            }
            else

            {
            AudioListener.volume = 1;
            //AudioListener.volume = 0;
            Debug.Log("B");
            }

        }

 
 
 
    }
