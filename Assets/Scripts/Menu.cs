using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private FallingObjectsSpawner _fallingObjectsSpawner;
    [SerializeField] private PulseButton _pulseButton;
    [SerializeField] private ScaleUp _scaleUp;

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
        SetDataInMenu();
        MyAudioManager.Instance.BackMenuMusic.Play();
        _textInstructionsAnimator.enabled = false;          // ��������� �������� ������ � �����������
        ObjectInterAction.OnStartTapped += StartGame;       // ����������� StartGame �� ������� ��� ������� �� ������ �����
    }

    private void OnEnable()
    {
        //_startButtonAnimator.SetTrigger("Shake");
    }
    public void Restart()
    {
        //_productListAnimator.SetTrigger("Exit");
        _pulseButton.gameObject.SetActive(true);
        //_startButtonAnimator.SetTrigger("Exit");
        SetDataInMenu();  // <-- ��������� ������ ���� ��� ��������
        MyAudioManager.Instance.BackMenuMusic.Play();
        //_textInstructionsAnimator.enabled = false;          // ��������� �������� ������ � �����������
        ObjectInterAction.OnStartTapped += StartGame;       // ����������� StartGame �� ������� ��� ������� �� ������ �����
    }
    private void SetDataInMenu()
    {
        rawImageProduct1.texture = _fallingObjectsSpawner.selectedProducts[0].Sprite.texture;
        rawImageProduct2.texture = _fallingObjectsSpawner.selectedProducts[1].Sprite.texture;
        rawImageProduct3.texture = _fallingObjectsSpawner.selectedProducts[2].Sprite.texture;
        textproduct1.text = "= " + _fallingObjectsSpawner.selectedProducts[0].Amount.ToString();
        textproduct2.text = "= " + _fallingObjectsSpawner.selectedProducts[1].Amount.ToString();
        textproduct3.text = "= " + _fallingObjectsSpawner.selectedProducts[2].Amount.ToString();
    }
    public void StartGame()
    {
        MyAudioManager.Instance.StartButtonClick.Play();    // �������� ���� ������� �� ������
        StartCoroutine(StartGameProcess());                 // ��������� �������� ����������� ����        
    }

    private IEnumerator StartGameProcess()
    {
        //_startButtonAnimator.SetTrigger("Start");        // ��������� �������� �������� � �������� ������
        _pulseButton.gameObject.SetActive(false);        
        //_productListAnimator.SetTrigger("Start");
        //_textInstructionsAnimator.enabled = true;        // ��������� �������� ������� ������ ����������
        //StartCoroutine(SoundDelay());
        yield return new WaitForSeconds(1f);                
        MyAudioManager.Instance.BackMenuMusic.Stop(); 
        MyAudioManager.Instance.BackGameMusic.Play();
        _gameManager.CountdownGameState();        
    }
    private IEnumerator SoundDelay() 
    {
        yield return new WaitForSeconds(0.5f);
        MyAudioManager.Instance.MenuFadeSound.Play();
    }
}
