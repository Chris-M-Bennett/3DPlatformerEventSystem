using UnityEngine;
using UnityEngine.UI;

namespace EventSystem{
    public class GameManagerSingleton : GenericSingleton<GameManagerSingleton>
    {
        private int _score;
        private Text _scoreUI;
        public int Score{
            get => _score;
            set{
                _score = value;
                _scoreUI.text =$"Score: + {Score}";
            }
        }
        
        private int _health;
        private Text _healthUI;
        public int Health{
            get => _health;
            set{
                _health = value;
                _healthUI.text = $"Health: {Health}";
            }
        }
        
        private float _speed;
        
        public float Speed{
            get => _speed;
            set{
                _speed = value;
            }
        }
        

        public override void Awake()
        {
            base.Awake();
            _scoreUI = GameObject.Find("Score").GetComponent<Text>();
            if (_scoreUI == null)
            {
                Debug.LogError("Create Canvas UI Text called 'Score'");
            }
            _healthUI = GameObject.Find("Score").GetComponent<Text>();
            if (_healthUI == null)
            {
                Debug.LogError("Create Canvas UI Text called 'Health'");
            }
        }
    }
}
