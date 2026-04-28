import { computed, ref } from 'vue'
import en from '@/locales/en.mjs'
import hu from '@/locales/hu.mjs'

const messages = { en, hu }
const supportedLocales = ['en', 'hu']
const fallbackLocale = 'en'

function getSavedLocale() 
{
    if (typeof window === 'undefined') 
    {
        return fallbackLocale
    }

    const saved = window.localStorage.getItem('neuchess-locale')
    return supportedLocales.includes(saved) ? saved : fallbackLocale
}

export const locale = ref(getSavedLocale())

function updateDocumentLanguage(value) 
{
    if (typeof document !== 'undefined') 
    {
        document.documentElement.lang = value
    }
}

updateDocumentLanguage(locale.value)

function getMessage(source, key) 
{
    return key.split('.').reduce((current, part) => current?.[part], source)
}

function interpolate(message, params) 
{
    return Object.entries(params).reduce(
        (text, [key, value]) => text.replaceAll(`{${key}}`, value),
        message
    )
}

export function t(key, params = {}) 
{
    const message = getMessage(messages[locale.value], key) ??
                    getMessage(messages[fallbackLocale], key) ??
                    key

    return typeof message === 'string' 
      ? interpolate(message, params) 
      : message
}

export function setLocale(value) 
{
    if (!supportedLocales.includes(value)) 
    {
        return
    }

    locale.value = value
    updateDocumentLanguage(value)

    if (typeof window !== 'undefined') 
    {
        window.localStorage.setItem('neuchess-locale', value)
    }
}

export function toggleLocale() 
{
    setLocale(locale.value === 'en' ? 'hu' : 'en')
}

export function useI18n() 
{
    const languageName = computed(() => 
        t(`language.${locale.value}`))

    return {
        locale,
        languageName,
        t,
        setLocale,
        toggleLocale,
    }
}