<script setup>
import { Stepper, StepperIndicator, StepperItem, StepperSeparator, StepperTitle, StepperTrigger } from '@/components/ui/stepper'
import { LanguageButton, NavbarLogo, DownloadButton, ThemeToggleButton, LogoutButton } from '@components/ui/navbar'
import { navIconButtonClass, navHamburgerSvgClass } from '@components/ui/navbar/navActionClasses.mjs'

import { computed, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useUserStore } from "@stores/UserStore";
import { useI18n } from '@utils/i18n'

const userStore = useUserStore();
const rawData = userStore.registrationData
const route = useRoute()
const router = useRouter()
const { t } = useI18n()
const isDarkTheme = ref(document.documentElement.classList.contains('dark'))
const isMobileMenuOpen = ref(false)

const isAuthenticated = computed(() => !!userStore.token)
const showLogout = computed(() => isAuthenticated.value || route.name === 'admin')

const showDownload = computed(() =>
    route.name === 'welcome' ||
    route.name === 'signin' ||
    route.name === 'user'
)

const showSignUpStepper = computed(() =>
    route.name === 'signup' ||
    route.name === 'personalinformation' ||
    route.name === 'confirminformation'
)

const stepperSignUpStep = computed(() =>
    route.name === 'signup' ? 1 : 2
)

const stepperPersonalInformationStep = computed(() =>
    route.name === 'personalinformation' ? 1 : 2
)

const stepperConfirmationStep = computed(() =>
    route.name === 'confirminformation' ? 1 : 2
)

const stepperPersonalInformationStepClick = computed(() =>
    rawData.email !== '' && rawData.password !== ''
)

const stepperConfirmationStepClick = computed(() =>
    rawData.nickname !== ''
)

function toggleTheme() {
    isDarkTheme.value = document.documentElement.classList.toggle('dark')
    localStorage.setItem('theme', isDarkTheme.value ? 'dark' : 'light')
    isMobileMenuOpen.value = false
}

async function handleLogout() {
    await userStore.logout()
    isMobileMenuOpen.value = false
    router.push({ name: 'signin' })
}

</script>

<template>
    <header>
        <nav class="navbar px-4 py-4 navbar-expand-lg mb-8! flex! flex-col! items-stretch! border-b! border-(--BorderBrush)! bg-(--NavBarBrush)!">
            <div class="relative flex w-full items-center justify-between gap-3">
                <div class="relative z-10 shrink-0 basis-[3rem] md:hidden" aria-hidden="true"></div>

                <div class="relative z-10 hidden shrink-0 basis-[4.5rem] justify-start md:flex">
                    <LanguageButton />
                </div>

                <div class="pointer-events-none absolute left-1/2 top-1/2 flex min-w-0 -translate-x-1/2 -translate-y-1/2 justify-center">
                    <NavbarLogo />
                </div>

                <div class="relative z-10 hidden shrink-0 items-center justify-end gap-2 md:flex xl:gap-3">
                    <DownloadButton v-if="showDownload" compact />
                    <ThemeToggleButton :is-dark-theme="isDarkTheme" @toggle="toggleTheme" />
                    <LogoutButton v-if="showLogout" @logout="handleLogout" />
                </div>

                <div class="relative z-10 flex shrink-0 basis-[3rem] justify-end md:hidden">
                    <button type="button" :class="navIconButtonClass"
                        :aria-expanded="isMobileMenuOpen" aria-label="Menu" @click="isMobileMenuOpen = !isMobileMenuOpen">
                        <svg
                            v-if="!isMobileMenuOpen"
                            xmlns="http://www.w3.org/2000/svg"
                            :class="navHamburgerSvgClass"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            aria-hidden="true"
                        >
                            <path d="M4 6h16M4 12h16M4 18h16" />
                        </svg>
                        <svg
                            v-else
                            xmlns="http://www.w3.org/2000/svg"
                            :class="navHamburgerSvgClass"
                            viewBox="0 0 24 24"
                            fill="none"
                            stroke="currentColor"
                            stroke-width="2"
                            stroke-linecap="round"
                            stroke-linejoin="round"
                            aria-hidden="true"
                        >
                            <path d="M18 6 6 18M6 6l12 12" />
                        </svg>
                    </button>
                </div>
            </div>

            <div v-if="isMobileMenuOpen" class="mt-3.5 flex flex-col items-stretch gap-2 rounded bg-(--NavBarBrush) p-2 pb-0 -mb-2 md:hidden">
                <LanguageButton menu inverse-flag @toggled="isMobileMenuOpen = false" />
                <DownloadButton v-if="showDownload" class="w-full!" />
                <ThemeToggleButton menu :is-dark-theme="isDarkTheme" @toggle="toggleTheme" />
                <LogoutButton v-if="showLogout" menu @logout="handleLogout" />
            </div>
        </nav>
    </header>

    <div id="stepper" class="flex w-full items-center justify-center px-2 sm:px-4" v-if="showSignUpStepper">
        <Stepper class="mx-auto grid w-full max-w-[44rem] grid-cols-3 items-start gap-1 sm:gap-2">

            <StepperItem :step="stepperSignUpStep" class="min-w-0 justify-center gap-1 sm:gap-2">
                <StepperTrigger class="w-full min-w-0">
                    <RouterLink class="stepperLink flex min-w-0 flex-col items-center gap-1 text-center" to="signup">
                        <StepperIndicator>1</StepperIndicator>
                        <StepperTitle class="max-w-[4.5rem] whitespace-normal text-center text-[10px] leading-tight sm:max-w-none sm:text-xs md:text-sm">{{ t('auth.signUpStepper') }}</StepperTitle>
                    </RouterLink>
                </StepperTrigger>
                <StepperSeparator class="hidden sm:block" />
            </StepperItem>

            <StepperItem :step="stepperPersonalInformationStep" class="min-w-0 justify-center gap-1 sm:gap-2">
                <StepperTrigger class="w-full min-w-0">
                    <component
                    
                        :is="stepperPersonalInformationStepClick ? 'RouterLink' : 'div'"
                        :to="stepperPersonalInformationStepClick ? 'personalinformation' : null"
                        class="stepperLink flex min-w-0 flex-col items-center gap-1 text-center"
                        :class="!stepperPersonalInformationStepClick ? 'pointer-events-none opacity-50' : ''">
                        
                        <StepperIndicator>2</StepperIndicator>
                        <StepperTitle class="max-w-[5.25rem] whitespace-normal text-center text-[10px] md:text-nowrap leading-tight sm:max-w-none sm:text-xs md:text-sm">{{ t('registration.personalInformation') }}</StepperTitle>
                    </component>
                </StepperTrigger>
                <StepperSeparator class="hidden sm:block" />
            </StepperItem>

            <StepperItem :step="stepperConfirmationStep" class="min-w-0 justify-center gap-1 sm:gap-2">
                <StepperTrigger class="w-full min-w-0">
                    <component
                        :is="stepperConfirmationStepClick ? 'RouterLink' : 'div'"
                        :to="stepperConfirmationStepClick ? 'confirmation' : null"
                        class="stepperLink text-(--TextBrush) decoration-0 flex min-w-0 flex-col items-center gap-1 text-center"
                        :class="!stepperConfirmationStepClick ? 'pointer-events-none opacity-50' : ''">

                        <StepperIndicator>3</StepperIndicator>
                        <StepperTitle class="max-w-[4.5rem] whitespace-normal text-center text-[10px] leading-tight sm:max-w-none sm:text-xs md:text-sm">
                            {{ t('registration.finalSteps') }}
                        </StepperTitle>
                    </component>
                </StepperTrigger>
                <StepperSeparator class="hidden sm:block" />
            </StepperItem>

        </Stepper>
    </div>
</template>

<style lang="css">
.stepperLink {
    color: var(--TextBrush);
    text-decoration: none;
}
</style>