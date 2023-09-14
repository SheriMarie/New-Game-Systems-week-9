using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Game Systems/Player/Interact
[AddComponentMenu("Game Systems/Player/Interact")]
public class Interact : MonoBehaviour
{
    //RAY - A ray is an infinite line (in 1 direction) starting (extending) from a starting origin and going in some...specified direction 
    //RAYCASTING - instructions for Sending the ray, from an origin point, in the direction, for a search length, against all/specified colliders in the scene, on the layer of search 
    //RAYCAST HIT - the structure of data used to get information back from the rays collision with a collider 
    //LAYER - objects in the scene can be placed on a layer (a layer is like a group)
    //LAYERMASK - Layer mask is the chosen layer or layers we are searching/interacting with 

    #region Update   
    private void Update()
    {
        //if our interact key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            //create a ray
            Ray interactRay;
            //this ray is shooting out from the main cameras screen point center of screen
            interactRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
            //create hit info
            RaycastHit hitInfo;
            //if this physics raycast hits something within 10 units
            if (Physics.Raycast(interactRay, out hitInfo, 10))
            {
                #region NPC tag
                //and that hits info is tagged NPC
                if (hitInfo.collider.tag == "NPC")
                {
                    //Debug that we hit a NPC    
                    Debug.Log("OH HELLO THERE: " + hitInfo.transform.name + " is talking to you");
                    //trigger the dialogue script
                    if (hitInfo.collider.GetComponent<Dialogue>())
                    {
                        hitInfo.collider.GetComponent<Dialogue>().OpenDialogue();
                    }
                }
                #endregion
                #region Item
                //and that hits info is tagged Item
                if (hitInfo.collider.tag == "Item")
                {
                    //Debug that we hit an Item               
                    Debug.Log(hitInfo.transform.name);
                }
                #endregion
                #region Chest
                //and that hits info is tagged Chest
                if (hitInfo.collider.tag == "Chest")
                {
                    //Debug that we hit an Chest               
                    Debug.Log(hitInfo.transform.name);
                }
                #endregion
            }
        }
    }
    #endregion
}






