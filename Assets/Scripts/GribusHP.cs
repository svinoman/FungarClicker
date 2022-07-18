using UnityEngine;

public class GribusHP : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private AudioSource _impactAudio;
    void Update()
    {
        PCRaycast();
        MobileRaycast();
    }

    private void PCRaycast()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray _mouseRaycast = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit _mouseRaycastHit;
            if(Physics.Raycast(_mouseRaycast, out _mouseRaycastHit))
            {
                if (_mouseRaycastHit.transform.tag == "Gribus")
                {
                    _mouseRaycastHit.transform.GetComponent<GribusAI>().hp -= 1;
                    _impactAudio.pitch = Random.Range(0.9f, 1.1f);
                    _impactAudio.Play();
                }
                if(_mouseRaycastHit.transform.tag == "Bomb")
                {
                    _mouseRaycastHit.transform.GetComponent<Animator>().SetBool("isBombDetonated", true);
                    GameObject.FindGameObjectWithTag("GribusManager").GetComponent<GribusSpawner>().BombEffect();
                }
                if (_mouseRaycastHit.transform.tag == "Icecream")
                {
                    _mouseRaycastHit.transform.GetComponent<Animator>().SetBool("isIcecreamEaten", true);
                    GameObject.FindGameObjectWithTag("GribusManager").GetComponent<GribusSpawner>().IcecreamEffect();
                }
            }
        }
    }
    private void MobileRaycast()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray _touchScreenRaycast = _mainCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit _touchScreenRaycastHit;
                if (Physics.Raycast(_touchScreenRaycast, out _touchScreenRaycastHit))
                {
                    if (_touchScreenRaycastHit.transform.tag == "Gribus")
                    {
                        _touchScreenRaycastHit.transform.GetComponent<GribusAI>().hp -= 1;
                        _impactAudio.pitch = Random.Range(0.9f, 1.1f);
                        _impactAudio.Play();
                    }
                    if (_touchScreenRaycastHit.transform.tag == "Bomb")
                    {
                        _touchScreenRaycastHit.transform.GetComponent<Animator>().SetBool("isBombDetonated", true);
                    }
                    if (_touchScreenRaycastHit.transform.tag == "Icecream")
                    {
                        _touchScreenRaycastHit.transform.GetComponent<Animator>().SetBool("isIcecreamEaten", true);
                    }
                }
            }
        }
    }
}
