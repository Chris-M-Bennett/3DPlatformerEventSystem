using EventSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics{
    public class TeleporterScript : MonoBehaviour
    {
        public UnityAction teleport;
        void OnEnable(){
            EventManager.Instance.StartListening("teleport", teleport);
        }

        void OnDisable(){
            if(EventManager.Instance != null){
                EventManager.Instance.StopListening("teleport", teleport);
            }
        }
    
        void TeleportHere(){
        
        }
    }
}
