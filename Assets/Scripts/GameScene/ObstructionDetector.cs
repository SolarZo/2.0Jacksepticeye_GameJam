using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionDetector : MonoBehaviour
{
    public Transform target;
    public LayerMask obstructionLayer;
    public float fadeSpeed = 5f;

    private List<Renderer> obstructedRenderers = new List<Renderer>();

    RaycastHit hit;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;


    }

    void Update()
    {
        Ray ray = new Ray(transform.position, (target.position - transform.position).normalized * Vector3.Distance(transform.position, target.position));

        //RaycastHit[] hits = Physics.RaycastAll(transform.position, (target.position - transform.position).normalized * Vector3.Distance(transform.position, target.position), Vector3.Distance(transform.position, target.position), obstructionLayer);
        
        if (Physics.Raycast(ray,out hit))
        {
            Debug.Log(hit.collider.gameObject.name + " was hit");
        }

         


        /*
        List<Renderer> renderersToRestore = new List<Renderer>(obstructedRenderers);
        foreach (RaycastHit hit in hits)
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            if (hitRenderer != null && renderersToRestore.Contains(hitRenderer))
            {
                renderersToRestore.Remove(hitRenderer); // Keep it obstructed
            }
        }

        foreach (Renderer r in renderersToRestore)
        {
            StartCoroutine(FadeOut(r, 1f)); // Fade back to opaque
            obstructedRenderers.Remove(r);
        }

        // Handle current obstructions
        foreach (RaycastHit hit in hits)
        {
            Renderer hitRenderer = hit.collider.GetComponent<Renderer>();
            if (hitRenderer != null && !obstructedRenderers.Contains(hitRenderer))
            {
                StartCoroutine(FadeOut(hitRenderer, 0.3f)); // Fade to semi-transparent
                obstructedRenderers.Add(hitRenderer);
            }
        }*/
    }

    IEnumerator FadeOut(Renderer renderer, float targetAlpha)
    {
        Material material = renderer.material;
        Color currentColor = material.color;
        float startAlpha = currentColor.a;

        float timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime * fadeSpeed;
            currentColor.a = Mathf.Lerp(startAlpha, targetAlpha, timer);
            material.color = currentColor;
            yield return null;
        }
        currentColor.a = targetAlpha;
        material.color = currentColor;
    }
}
