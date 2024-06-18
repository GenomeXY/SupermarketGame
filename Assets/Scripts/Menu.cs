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
        MyAudioManager.Instance.BackMenuMusic.Play();       // включаем музыку меню 
        _startButtonAnimator.enabled = true;                // включаем анимацию кнопки старт
        _productListAnimator.enabled = false;               // отключаем анимацию списка продуктов
        _textInstructionsAnimator.enabled = false;          // отключаем анимацию текста с инструкцией
        ObjectInterAction.OnStartTapped += StartGame;       // подписываем StartGame на событие при нажатии на кнопку старт
    }

    public void StartGame()
    {
        MyAudioManager.Instance.StartButtonClick.Play();    // включаем звук нажатия на кнопку
        StartCoroutine(MenuDisable());                      // запускаем корутину отключающую меню
    }

    private IEnumerator MenuDisable()
    {
        _startButtonAnimator.SetTrigger("Start");        // запускаем анимацию перелета и вращения кнопки
        _productListAnimator.enabled = true;             // запускаем анимацию отлета списка продуктов
        _textInstructionsAnimator.enabled = true;        // запускаем анимацию отслета текста инструкций
        StartCoroutine(SoundDelay());
        yield return new WaitForSeconds(1f);        
        _startButtonAnimator.enabled = false;            // выключаем анимацию кнопки старт
        _menuCanvas.SetActive(false);                    // выключаем канвас с меню        
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
