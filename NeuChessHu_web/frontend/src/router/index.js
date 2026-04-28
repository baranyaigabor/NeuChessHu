import { createRouter, createWebHistory } from 'vue-router'
import { routes } from 'vue-router/auto-routes'
import { watch } from 'vue'
import { locale, t } from '@utils/i18n'

const routeTitleKeys = {
    welcome: 'pageTitles.welcome',
    signin: 'pageTitles.signIn',
    signup: 'pageTitles.signUp',
    personalinformation: 'pageTitles.personalInformation',
    confirminformation: 'pageTitles.confirmations',
    admin: 'pageTitles.administration',
}

function setTitle(to) {
    if (to.name === 'user')
    {
        document.title = to.params.nickname || 'NeuChess'
        return
    }

    const titleKey = routeTitleKeys[to.name]
    const title = titleKey ? t(titleKey) : to.meta?.title
    document.title = title ? `${title} | NeuChess` : 'NeuChess'
}

export const router = createRouter({
    routes,
    history: createWebHistory(),
    linkActiveClass: 'active',
})

router.afterEach(setTitle)

watch(locale, () => 
    setTitle(router.currentRoute.value)
)