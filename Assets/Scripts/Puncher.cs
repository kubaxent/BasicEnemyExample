using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Puncher : MonoBehaviour{
   
    Coroutine punchCor;
    void Update(){
        if(Input.GetMouseButtonDown(0)){
            if(punchCor==null)punchCor = StartCoroutine(PunchCor());
        }
    }
    
    IEnumerator PunchCor(){
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward,out hit,2f,Physics.AllLayers)){
            AICharacterControl ai = hit.transform.root.GetComponent<AICharacterControl>();
            if(ai!=null){
                ai.Die();
            }

            yield return null;

            Rigidbody[] rigs = hit.transform.root.GetComponentsInChildren<Rigidbody>();
            for(int i = 0; i < rigs.Length; i++){
                rigs[i].AddForce(transform.forward*15f,ForceMode.Impulse);
            }
        }
        punchCor = null;
    }
    
}
