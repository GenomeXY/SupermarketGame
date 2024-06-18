using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _uiCanvas;
    [SerializeField] private Countdown _countdown;
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;

    public RawImage rawImageProduct1;
    public RawImage rawImageProduct2;
    public RawImage rawImageProduct3;

    public TextMeshProUGUI textproduct1;
    public TextMeshProUGUI textproduct2;
    public TextMeshProUGUI textproduct3;

    [SerializeField] private Animator _startButtonAnimator;
    [SerializeField] private Animator _productListAnimator;
    [SerializeField] private Animator _textInstructionsAnimator;    
    void Start()
    {
        rawImageProduct1.texture = _fallingObjectsSpawner.selectedProducts[0].Sprite.texture;
        rawImageProduct2.texture = _fallingObjectsSpawner.selectedProducts[1].Sprite.texture;
        rawImageProduct3.texture = _fallingObjectsSpawner.selectedProducts[2].Sprite.texture;
        textproduct1.text = "= " + _fallingObjectsSpawner.selectedProducts[0].Amount.ToString();
        textproduct2.text = "= " + _fallingObjectsSpawner.selectedProducts[1].Amount.ToString();
        textproduct3.text = "= " + _fallingObjectsSpawner.selectedProducts[2].Amount.ToString();
        MyAudioManager.Instance.BackMenuMusic.Play();       // �������� ������ ���� 
        _startButtonAnimator.enabled = true;                // �������� �������� ������ �����
        _productListAnimator.enabled = false;               // ��������� �������� ������ ���������
        _textInstructionsAnimator.enabled = false;          // ��������� �������� ������ � �����������
        ObjectInterAction.OnStartTapped += StartGame;       // ����������� StartGame �� ������� ��� ������� �� ������ �����
    }

    public void StartGame()
    {
        MyAudioManager.Instance.StartButtonClick.Play();    // �������� ���� ������� �� ������
        StartCoroutine(MenuDisable());                      // ��������� �������� ����������� ����
    }

    private IEnumerator MenuDisable()
    {
        _startButtonAnimator.SetTrigger("Start");        // ��������� �������� �������� � �������� ������
        _productListAnimator.enabled = true;             // ��������� �������� ������ ������ ���������
        _textInstructionsAnimator.enabled = true;        // ��������� �������� ������� ������ ����������
        StartCoroutine(SoundDelay());
        yield return new WaitForSeconds(1f);        
        _startButtonAnimator.enabled = false;            // ��������� �������� ������ �����
        _menuCanvas.SetActive(false);                    // ��������� ������ � ����        
        _countdown.gameObject.SetActive(true);
        MyAudioManager.Instance.BackMenuMusic.Stop();
        MyAudioManager.Instance.BackGameMusic.Play();
        _uiCanvas.SetActive(true);
        _countdown.StartCountdown();        
    }
    private IEnumerator SoundDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        MyAudioManager.Instance.MenuFadeSound.Play();
    }
}
