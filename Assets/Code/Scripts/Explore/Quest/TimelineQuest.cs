using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineQuest : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] float _duration;
    [SerializeField] GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _duration -= Time.deltaTime;
        if(_duration < 0){
            player.SetActive(true);
            Destroy(gameObject);
        }
    }
}
