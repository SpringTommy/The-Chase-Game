using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float maxDistance = 2f;
    [SerializeField] private Text interactableName;
    [SerializeField] private GameObject ousideSpawn;
    [SerializeField] private string newSceneName;

    private InteractionObject targetInteraction;

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = Camera.main.transform.position;
        Vector3 direction = Camera.main.transform.forward;
        RaycastHit raycastHit = new RaycastHit();
        string interactionText = "";
        targetInteraction = null;

        if (Physics.Raycast(origin, direction, out raycastHit, maxDistance))
        {
            targetInteraction = raycastHit.collider.gameObject.GetComponent<InteractionObject>();
        }

        if (targetInteraction && targetInteraction.enabled)
        {
            interactionText = targetInteraction.GetInteractionText();
        }

        SetInteractableNameText(interactionText);
    }

    private void SetInteractableNameText(string newText)
    {
        if (interactableName)
        {
            interactableName.text = newText;
        }
    }

    public void TryInteract()
    {
        if (targetInteraction && targetInteraction.enabled)
        {
            targetInteraction.Interact();
        }
    }

    public void tpPlayer()
    {
        transform.position = ousideSpawn.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            SceneManager.LoadScene(newSceneName);
        }
    }
}