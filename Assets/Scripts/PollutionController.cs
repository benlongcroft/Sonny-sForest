using System.Collections;
using System.Runtime.CompilerServices;
using Forest;
using Main;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class PollutionController : MonoBehaviour
    {
        [SerializeField]
        private Text _pollutionDisplayText;
        
        private float _pollutionCount = 0;
        private bool _generatorTickStarted = false;
        private Field[] _currentForest;
    
        // Start is called before the first frame update
        void Start()
        {
            _pollutionDisplayText = GetComponentInChildren<Text>();
            _currentForest = GameObject.Find("char").GetComponent<CharController>().myForest;
            StartCoroutine(GeneratorTick());
        }

        // Update is called once per frame
        void Update()
        {
            _pollutionDisplayText.text = _pollutionCount.ToString();
        }
        
        private IEnumerator GeneratorTick()
        {
            if (!_generatorTickStarted)
            {
               _generatorTickStarted = true;
               _pollutionCount += 1;
            }

            _generatorTickStarted = false;
            
            //Print the time of when the function is first called.
            Debug.Log("Started Coroutine at timestamp : " + Time.time);

            //yield on a new YieldInstruction that waits for 2 seconds.
            yield return new WaitForSeconds(2);

            //After we have waited 2 seconds print the time again.
            Debug.Log("Finished Coroutine at timestamp : " + Time.time);
            StartCoroutine(GeneratorTick());
        }
        
    }
}
