using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementController : MonoBehaviour
{
    public CharacterController _playerController;

    [SerializeField] private float _playerSpeed = 4f;

    private int _memoryCount;
    [SerializeField] private TextMeshProUGUI _memoryCountText;
    [SerializeField] private GameObject _escapeHouseObject;
    [SerializeField] private GameObject _findMemoriesObject;
    [SerializeField] private GameObject _memoryCountObject;


    // Start is called before the first frame update
    void Start()
    {
        _memoryCount = 0;
        SetMemoryCountText();

        _escapeHouseObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y > 1.2f)
        {
            transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movePlayer = transform.right * x + transform.forward * z;

        _playerController.Move(movePlayer * _playerSpeed * Time.deltaTime);
    }

    void SetMemoryCountText()
    {
        _memoryCountText.text = "Memories Found: " + _memoryCount.ToString();

        if (_memoryCount == 4)
        {
            _escapeHouseObject.SetActive(true);
            _findMemoriesObject.SetActive(false);
            _memoryCountObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Memory"))
        {
            other.gameObject.SetActive(false);
            _memoryCount++;

            SetMemoryCountText();
        }
    }
}
