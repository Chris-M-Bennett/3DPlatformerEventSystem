using EventSystem;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Interface{
    public class HUDScript : GenericSingleton<HUDScript>
    {
        
        [SerializeField, Header("UI Element showing time remaining")] private Text timerText;
        [SerializeField, Header("UI Element showing collectibles collected")] private Text collectText;
        [SerializeField, Header("UI Element showing player's health")] private Text healthText;
        [SerializeField, Header("How many minutes timer should count down from")] private float timerMinutes;
        [SerializeField, Header("How many seconds timer should count down from")] private float timerSeconds;
        private float _totalSeconds;
        public UnityAction increaseCollected, increaseTime, increaseHealth;
        private int _collected;

        public int Collected{
            get => _collected;
            set{
                //Debug.Log("Has been collected");
                _collected = value;
                collectText.text = $"Collected:{_collected}";
            }
        }

        private float _health;

        public float Health{
            get => _health;
            set{
                _health = value;
                healthText.text = $"Health:{_health}";
            }
        }
        
        void OnEnable(){
            EventManager.Instance.StartListening("collected", increaseCollected);
            EventManager.Instance.StartListening("moreTime", increaseTime);
            EventManager.Instance.StartListening("healthUp", increaseHealth);
        }

        void OnDisable(){
            if(EventManager.Instance != null){
                EventManager.Instance.StopListening("collected", increaseCollected);
                EventManager.Instance.StopListening("moreTime", increaseTime);
                EventManager.Instance.StopListening("healthUp", increaseHealth);
            }
        }
        
        private void OnValidate(){
            timerSeconds = Mathf.Clamp(timerSeconds,0, 59);
        }
        
        public override void Awake(){
            base.Awake();
            increaseCollected = OnCollect;
            increaseHealth = OnHealthPickup;
            increaseTime = OnTimePickup;
                
            //Checks that score counter exists on UI
            collectText = GameObject.Find("Score Text").GetComponent<Text>();
            if (collectText == null)
            {
                Debug.LogError("Create Canvas UI Text called Counter Text!");
            }
            //Checks that health monitor exists on UI
            healthText = GameObject.Find("Health Text").GetComponent<Text>();
            if (healthText == null)
            {
                Debug.LogError("Create Canvas UI Text called Health Text!");
            }
            //Checks that timer exists on UI
            timerText = GameObject.Find("Timer Text").GetComponent<Text>();
            if (timerText == null)
            {
                Debug.LogError("Create Canvas UI Text called Timer Text!");
            }
            _totalSeconds = timerMinutes * 60 +timerSeconds;
        }
        private void LateUpdate(){
            if(_totalSeconds > 0f){
                _totalSeconds -= 1 * Time.deltaTime;
                timerMinutes = Mathf.RoundToInt(_totalSeconds/60);
                timerSeconds = Mathf.RoundToInt(_totalSeconds%60);
                //Check if seconds is two digits, adds leading zero if not
                timerText.text = timerSeconds > 9 ? $"{timerMinutes}:{timerSeconds}" 
                    : $"{timerMinutes}:0{timerSeconds}";
            }
        }

        public void OnCollect(){
            _collected++;
            collectText.text = $"Score:{_collected}";
        }
        
        public void OnTimePickup(){
            timerSeconds += 20;
            if (timerSeconds > 59){
                timerSeconds = 59;
                timerMinutes += 1;
            }
        }
        
        public void OnHealthPickup(){
            if (_health > 100){
                _health++;
                healthText.text = $"Health:{_health}%";
            }
        }
    }
}
