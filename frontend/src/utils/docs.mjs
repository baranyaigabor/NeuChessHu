function docsBaseUrl() 
{
    const configuredHost = import.meta.env?.VITE_DOCS_URL || 'docs.vm2.test'
    const trimmedHost = configuredHost.trim().replace(/\/+$/, '')
    return `http://${trimmedHost.replace(/^https?:\/\//i, '')}`
}

export function termsUrl(locale) 
{
    const language = locale === 'hu' ? 'hu' : 'en'

    return `${docsBaseUrl()}/${language}/terms`
}

export function userGuideUrl(locale)
{
    const language = locale === 'hu' ? 'hu' : 'en'

    return `${docsBaseUrl()}/${language}/user-guide`
}