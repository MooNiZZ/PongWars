using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PongBall : MonoBehaviour
{
    [SerializeField] private Vector2 _initialVelocity = new Vector2(1, 1);

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private AudioSource _audioSource;
    
    private void Awake()
    {
        _rigidbody2D ??= GetComponent<Rigidbody2D>();
        _audioSource ??= GetComponent<AudioSource>();
        
        _initialVelocity.y = Random.Range(-2f, 2f);
        _initialVelocity.x += Random.Range(-2f, 2f);
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.velocity = _initialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = _rigidbody2D.velocity;
        _rigidbody2D.velocity = velocity;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _audioSource.Play();

        var velocity = _rigidbody2D.velocity;

        if (Mathf.Abs(velocity.x) < 1)
        {
            var randomValue = Random.Range(1f, 2f);
            velocity.y -= randomValue;
            velocity.x += randomValue;
            
            _rigidbody2D.velocity = velocity;
        }
        
        if (Mathf.Abs(velocity.y) < 1)
        {
            var randomValue = Random.Range(1f, 2f);
            velocity.y += randomValue;
            velocity.x -= randomValue;
            
            _rigidbody2D.velocity = velocity;
        }
    }
}
