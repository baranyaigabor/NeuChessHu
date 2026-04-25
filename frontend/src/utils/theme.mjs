export function applySavedTheme() 
{
    const savedTheme = localStorage.getItem('theme')

    if (savedTheme === 'dark') 
    {
        document.documentElement.classList.add('dark')
    }
    else
    {
        document.documentElement.classList.remove('dark')
    }
}

export function useThemeToggler() 
{
    const isDark = document.documentElement.classList.toggle('dark')
    localStorage.setItem('theme', isDark 
      ? 'dark' 
      : 'light')
}