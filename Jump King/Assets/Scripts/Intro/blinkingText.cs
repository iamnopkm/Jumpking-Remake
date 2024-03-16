using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class blinkingText : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Khoảng thời gian giữa các nhấp nháy
    private TextMeshProUGUI textMeshPro;
    private bool isBlinking = false;

    void Start()
    {
        // Lấy tham chiếu đến TextMeshPro Component
        textMeshPro = GetComponent<TextMeshProUGUI>();

        // Bắt đầu Coroutine để thực hiện nhấp nháy
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText()
    {
        while (true)
        {
            // Đảo ngược trạng thái của chữ nhấp nháy
            isBlinking = !isBlinking;

            // Hiển thị hoặc ẩn chữ tùy thuộc vào trạng thái
            textMeshPro.enabled = isBlinking;

            // Chờ cho đến khi đến lượt nhấp nháy tiếp theo
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
