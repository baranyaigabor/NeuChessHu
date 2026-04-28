import { t } from '@utils/i18n'

export const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/

function fieldName(labelKey) 
{
    return labelKey.includes('.') ? t(labelKey) : labelKey
}

export function requiredMessage(value, labelKey) 
{
    return value?.toString().trim() ? '' : t('validation.required', { field: fieldName(labelKey) })
}

export function emailMessage(value) 
{
    const required = requiredMessage(value, 'common.email')

    if (required) 
    {
        return required
    }

    return emailPattern.test(value.trim()) ? '' : t('validation.emailInvalid')
}

export function passwordMessage(value) 
{
    const required = requiredMessage(value, 'common.password')

    if (required) 
    {
        return required
    }

    if (value.length < 8) 
    {
        return t('validation.passwordMin', { min: 8 })
    }

    if (!/[A-Za-z]/.test(value) || !/\d/.test(value)) 
    {
        return t('validation.passwordLetterNumber')
    }

    return ''
}

export function confirmPasswordMessage(password, confirmPassword) 
{
    const required = requiredMessage(confirmPassword, 'common.confirmPassword')

    if (!confirmPassword?.toString().trim()) 
    {
        return t('validation.confirmPasswordRequired', { field: fieldName('common.confirmPassword') })
    }

    return password === confirmPassword ? '' : t('validation.passwordMismatch')
}

export function nicknameMessage(value) 
{
    const required = requiredMessage(value, 'common.username')

    if (required) 
    {
        return required
    }

    if (value.trim().length < 3) {
        return t('validation.usernameMin', { min: 3 })
    }

    if (value.trim().length > 14) {
        return t('validation.usernameMax', { max: 14 })
    }

    return ''
}

export function optionalNameMessage(value, labelKey) 
{
    if (!value?.trim()) 
    {
        return ''
    }

    if (value.trim().length < 2) 
    {
        return t('validation.optionalMin', { field: fieldName(labelKey), min: 2 })
    }

    if (value.trim().length > 30) 
    {
        return t('validation.optionalMax', { field: fieldName(labelKey), max: 30 })
    }

    return ''
}

export function dateOfBirthMessage(value) 
{
    if (!value) 
    {
        return ''
    }

    let date

    if (typeof value.toDate === 'function') 
    {
        date = value.toDate(Intl.DateTimeFormat().resolvedOptions().timeZone)
    } 

    else if (value.year && value.month && value.day) 
    {
        date = new Date(value.year, value.month - 1, value.day)
    } 

    else 
    {
        date = new Date(value)
    }

    if (Number.isNaN(date.getTime())) 
    {
        return t('validation.dateInvalid')
    }

    return date <= new Date() ? '' : t('validation.dateFuture')
}