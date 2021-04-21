using System.Collections.Generic;
using UnityEngine.Events;

namespace EventSystem{
    public sealed class EventManager : GenericSingleton<EventManager>{
        public Dictionary<string, UnityEvent> _eventDictionary;

        public override void Awake(){
            base.Awake();
            if(Instance._eventDictionary == null){
                Instance._eventDictionary = new Dictionary<string, UnityEvent>();
            }
        }

        public void StartListening(string eventName, UnityAction listener){
            UnityEvent thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
                //Event name is in dictionary
                thisEvent.AddListener(listener);
            }else{
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Instance._eventDictionary.Add(eventName, thisEvent);
            } 
        }
        public void StopListening(string eventName, UnityAction listener){
            if (Instance._eventDictionary == null) return;
            UnityEvent thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
                thisEvent.RemoveListener(listener);
            }
        }
        public void TriggerEvent(string eventName){
            UnityEvent thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
                thisEvent.Invoke();
            } 
        }
    }
}
