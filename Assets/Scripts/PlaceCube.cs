using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaceCube : MonoBehaviour
{
    public GameObject previewCube;
    public GameObject placementText;
    public TMP_Text buttonText;
    public GameObject cubePrefab;
    public LayerMask placementLayer;

    private float rotationY = 0f;
    private GameObject currentPreview;
    private float previewTimer = 0f;
    private bool isPreviewActive = false;

    private bool isPlacementMode = false;

    public void Update()
    {
        if (!isPlacementMode) return;

        HandlePlacement();
        HandleRotation();

        if (isPreviewActive)
        {
            UpdatePreview();
        }
    }

    public void TogglePlacementMode()
    {
        Debug.Log("Toggling placement mode");


        isPlacementMode = !isPlacementMode;
        placementText.SetActive(isPlacementMode);

        if (isPlacementMode)
        {
            buttonText.text = "Placement Mode";
        }
        else
        {
            buttonText.text = "Normal Mode";
        }
    }

    void HandlePlacement()
    {
        if (isPreviewActive) return; // ?? block placement during preview

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, placementLayer))
            {
                Vector3 pos = hit.point;

                pos.x = Mathf.Round(pos.x);
                pos.z = Mathf.Round(pos.z);

                if (hit.collider.CompareTag("PlacedCube"))
                {
                    pos.y = hit.collider.transform.position.y + 1f;
                }
                else
                {
                    pos.y = 0.5f;
                }

                Instantiate(cubePrefab, pos, Quaternion.Euler(0, rotationY, 0));
            }
        }
    }
    void HandleRotation()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rotationY += 10f;

            if (currentPreview != null)
            {
                Destroy(currentPreview);
            }

            currentPreview = Instantiate(previewCube);

            Collider col = currentPreview.GetComponent<Collider>();
            if (col != null)
            {
                col.enabled = false;
            }

            SetTransparent(currentPreview);

            previewTimer = 1.5f;
            isPreviewActive = true;
        }
    }

    void UpdatePreview()
    {
        previewTimer -= Time.deltaTime;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, placementLayer))
        {
            Vector3 pos = hit.point;

            pos.x = Mathf.Round(pos.x);
            pos.z = Mathf.Round(pos.z);

            if (hit.collider.CompareTag("PlacedCube"))
            {
                pos.y = hit.collider.transform.position.y + 1f;
            }
            else
            {
                pos.y = 0.5f;
            }

            currentPreview.transform.position = pos;
            currentPreview.transform.rotation = Quaternion.Euler(0, rotationY, 0);
        }

        if (previewTimer <= 0f)
        {
            isPreviewActive = false;
            Destroy(currentPreview);
        }
    }

    void SetTransparent(GameObject obj)
    {
        Renderer r = obj.GetComponent<Renderer>();
        Color c = r.material.color;
        c.a = 0.5f;
        r.material.color = c;
    }
}
