namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Ink.Runtime;    

    public class InkExternal : MonoBehaviour
    {        
        public int ParseCamera;        
        public void Bind(Story story)
        {
            story.BindExternalFunction("playcamera", (string cameravalue) =>
                {
                    ParseCamera = int.Parse(cameravalue);

                    switch (ParseCamera)
                    {
                        case 1:
                            Debug.Log("TEST-1");                                                        
                            Debug.Log("Nilai = " + ParseCamera);
                            break;
                        case 2:
                            Debug.Log("TEST-2");                            
                            Debug.Log("Nilai = " + ParseCamera);
                            break;
                    }
                });
        }

        public void Unbind(Story story)
        {
            story.UnbindExternalFunction("playcamera");
        }
    }
}