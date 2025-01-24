using System;
using System.Collections;
using System.Collections.Generic;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class Localization : MonoBehaviour
{
    private const string Language = "Language";
    private const string EnglishCode = "English";
    private const string RussianCode = "Russian";
    private const string TurkishCode = "Turkish";
    private const string English = "en";
    private const string Russian = "ru";
    private const string Turkish = "tr";

    [SerializeField] private LeanLocalization _leanLocalization;
    // [SerializeField] private Load _load;

    private string _currentLanguage;
    private string _autoFoundLanguage;

    public event Action<string> LanguageChanged;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
         /*_autoFoundLanguage = YandexGamesSdk.Environment.i18n.lang;
         _currentLanguage = _load.Get(Language, _autoFoundLanguage);
         SetLanguage(_currentLanguage);
         LanguageChanged?.Invoke(_currentLanguage);*/

        /*_autoFoundLanguage = YandexGamesSdk.Environment.i18n.lang;
        SetLanguage(_currentLanguage);
        LanguageChanged?.Invoke(_currentLanguage);*/
        
        
        _autoFoundLanguage = YandexGamesSdk.Environment.i18n.lang;
        Debug.Log("ЯЗЫК " + _autoFoundLanguage);
        Debug.Log("ЯЗЫК " + _autoFoundLanguage);
        Debug.Log("ЯЗЫК " + _autoFoundLanguage);
        Debug.Log("ЯЗЫК " + _autoFoundLanguage);
        Debug.Log("ЯЗЫК " + _autoFoundLanguage);
        SetLanguage(_autoFoundLanguage);
#endif
        // LanguageChanged?.Invoke(_currentLanguage);
    }


    private void Start()
    {
        /*_autoFoundLanguage = YandexGamesSdk.Environment.i18n.lang;
        Debug.Log("!!! " + _autoFoundLanguage);
        Debug.Log("!!! " + _autoFoundLanguage);
        Debug.Log("!!! " + _autoFoundLanguage);

        SetLanguage(_autoFoundLanguage);*/
        
        // LanguageChanged?.Invoke(_currentLanguage);
    }

    public void SetLanguage(string languageCode)
    {
        Debug.Log("Передали " + languageCode);
        
        switch (languageCode)
        {
            case English:
                Debug.Log("ТУТ " + EnglishCode);
                _leanLocalization.SetCurrentLanguage(EnglishCode);  
                break;

            case Turkish:
                Debug.Log("ТУТ " + TurkishCode);
                _leanLocalization.SetCurrentLanguage(TurkishCode);
                break;

            case Russian:
                Debug.Log("ТУТ " + RussianCode);
                _leanLocalization.SetCurrentLanguage(RussianCode);
                break;
        }
    }
}