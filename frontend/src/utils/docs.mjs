function docsBaseUrl() 
{
    const configuredHost = import.meta.env?.VITE_DOCS_URL
    const trimmedHost = configuredHost.trim().replace(/\/+$/, '')
    return `http://${trimmedHost.replace(/^https?:\/\//i, '')}`;
}

export function termsUrl(locale) 
{
    const language = locale === 'hu' ? 'hu' : 'en'

    return `${docsBaseUrl()}/${language}/terms`
}