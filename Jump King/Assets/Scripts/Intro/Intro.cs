using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public string sceneToLoad; // Tên của cảnh cần chuyển đến
    public AudioSource sound_press_space;
    public AudioSource sound_menu_intro;
    private TextMeshProUGUI textMeshPro;
    private bool isBlinking = false;


    void Start()
    {
        sound_menu_intro.Play();
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Kiểm tra xem người chơi có ấn nút Space không
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Gọi hàm chuyển cảnh
            SwitchScene();
        }
    }

    void SwitchScene()
    {
        sound_press_space.Play();
        StartCoroutine(BlinkTextFast());
    }

    IEnumerator BlinkTextFast()
    {
        // Thiết lập thời gian giữa các nhấp nháy nhanh hơn
        float fastBlinkInterval = 0.05f;

        // Nhấp nháy nhanh hơn trong khi âm thanh đang chơi
        while (sound_press_space.isPlaying)
        {
            // Đảo ngược trạng thái của chữ nhấp nháy
            isBlinking = !isBlinking;

            // Hiển thị hoặc ẩn chữ tùy thuộc vào trạng thái
            textMeshPro.enabled = isBlinking;

            // Chờ cho đến khi đến lượt nhấp nháy tiếp theo
            yield return new WaitForSeconds(fastBlinkInterval);
        }

        // Khi âm thanh kết thúc, chuyển cảnh
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToLoad);
        }
    }
}
