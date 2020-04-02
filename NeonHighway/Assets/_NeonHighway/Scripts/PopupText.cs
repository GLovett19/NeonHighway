using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{
    /* What this thing does 
     * 
     * When instantiated or set active 
     * 
     * Sets text value to that of the score given
     * Sets its position on the canvas equal to the position of the target given
     * Begins moving recursivley for the duration of its lifetime 
     * 
     * How does it move? 
     * upward slowing down to reach its apex
     * shifts side to side slightly like it is floating 
     * 
     * 
     * */
    public Text myText; // the text displaying the given score 
    public float lifetime;
    public float count;
    
    [Range(0,100)]
    public float verticalMotionScale;
    [Range(0, 100)]
    public float horizontalMotionScale;
    [Range(0, 10)]
    public float verticalMotionSpeed;
    [Range(0, 10)]
    public float horizontalMotionSpeed;
    private void Update()
    {
        count += Time.deltaTime;
        if (count <= lifetime)
        {
            
            //shift upwards 
           transform.localPosition = new Vector3(transform.localPosition.x,
                                                 (verticalMotionScale / 100) * Mathf.Sin(verticalMotionSpeed * count),
                                                 transform.localPosition.z);

            //Shift Left and right
            transform.localPosition = new Vector3( (horizontalMotionScale/100) * Mathf.Sin(horizontalMotionSpeed * count),
                                                  transform.localPosition.y,
                                                  transform.localPosition.z);
        
        }
        else
        {
            transform.SetParent(FindObjectOfType<ObjectPoolingManager>().transform, false);
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }

}
