using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GestureRecognizer : MonoBehaviour
{

   public MagicManager magicManager;
   public TMP_Text gestureText;
   
   public void RecognizeGesture(string ObjectName)
   {
      if (ObjectName == "Fireball")
      {
         magicManager.spawnFireball();
      }
      else if (ObjectName == "Lightning")
      {
         magicManager.spawnLightning();
      }
      else
      {
         gestureText.text = "No gesture recognized";
      }
     
      
   }




}
