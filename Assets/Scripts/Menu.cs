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
    [SerializeField] private UIFly _uiFlyDown;
    [SerializeField] private UIFly _uiFlyLeft;
    public ScaleUp scaleUpStartButton;
    public ScaleUp scaleUpProductList;
    public ScaleUp scaleUpText;

    public Image ImageProduct1;
    public Image ImageProduct2;
    public Image ImageProduct3;

    public TextMeshProUGUI textproduct1;
    public TextMeshProUGUI textproduct2;
    public TextMeshProUGUI textproduct3;

    [SerializeField] private Animator _startButtonAnimator;
    [SerializeField] private Animator _productListAnimator;
    [SerializeField] private Animator _textInstructionsAnimator;    
    void Start()
    {
        scaleUpStartButton.StartScaling();
        scaleUpProductList.StartScaling();
        scaleUpText.StartScaling();
        SetDataInMenu();
        MyAudioManager.Instance.BackMenuMusic.Play();
        _textInstructionsAnimator.enabled = false;          // отключаем анимацию текста с инструкцией
        ObjectInterAction.OnStartTapped += StartGame;       // подписываем StartGame на событие при нажатии на кнопку старт
    }

    public void Restart()
    {                
        scaleUpStartButton.StartScaling();
        scaleUpProductList.StartScaling();
        scaleUpText.StartScaling();
        _pulseButton.gameObject.SetActive(true);
        SetDataInMenu();  // <-- Обновляем данные меню при рестарте
        MyAudioManager.Instance.BackMenuMusic.Play();
       
        ObjectInterAction.OnStartTapped += StartGame;       // подписываем StartGame на событие при нажатии на кнопку старт
    }
    private void SetDataInMenu()
    {
        ImageProduct1.sprite = _fallingObjectsSpawner.selectedProducts[0].Sprite;
        ImageProduct2.sprite = _fallingObjectsSpawner.selectedProducts[1].Sprite;
        ImageProduct3.sprite = _fallingObjectsSpawner.selectedProducts[2].Sprite;

        textproduct1.text = "= " + _fallingObjectsSpawner.selectedProducts[0].Amount.ToString();
        textproduct2.text = "= " + _fallingObjectsSpawner.selectedProducts[1].Amount.ToString();
        textproduct3.text = "= " + _fallingObjectsSpawner.selectedProducts[2].Amount.ToString();
    }
    public void StartGame()
    {
        MyAudioManager.Instance.StartButtonClick.Play();    // включаем звук нажатия на кнопку
        StartCoroutine(StartGameProcess());                 // запускаем корутину отключающую меню        
    }

    private IEnumerator StartGameProcess()
    {
        _pulseButton.gameObject.SetActive(false);
        _uiFlyDown.StartFlying();
        _uiFlyLeft.StartFlying();
        yield return new WaitForSeconds(1f);                
        MyAudioManager.Instance.BackMenuMusic.Stop(); 
        MyAudioManager.Instance.BackGameMusic.Play();
        _gameManager.CountdownGameState();
        _uiFlyDown.StopFlying();
        _uiFlyLeft.StopFlying();
    }
}
