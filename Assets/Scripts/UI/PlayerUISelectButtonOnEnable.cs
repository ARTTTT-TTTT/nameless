using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ART
{
    public class PlayerUISelectButtonOnEnable : MonoBehaviour
    {
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.Select(); // ทำให้ปุ่มถูกเลือกทันทีเมื่อเปิด
        }
    }
}