using StarterAssets;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class DetectionSphere : MonoBehaviour
{
    [SerializeField]
    private ThirdPersonController controller;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            controller.AddEnemy(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
           controller.RemoveEnemy(other.gameObject.name);
        }
    }
}
