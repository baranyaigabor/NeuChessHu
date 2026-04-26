<script setup>
import { Stepper, StepperIndicator, StepperItem, StepperSeparator, StepperTitle, StepperTrigger } from '@/components/ui/stepper'
import { LanguageFlag, NavbarLogo, DownloadButtonDiv, DownloadButton } from '@components/ui/login-navbar'

import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useUserStore } from "@stores/UserStore";
import { useI18n } from '@utils/i18n'

const userStore = useUserStore();
const rawData = userStore.registrationData
const route = useRoute()
const { t } = useI18n()

const showDownload = computed(() =>
  route.name === 'welcome' ||
  route.name === 'signin' ||
  route.name === 'user'
)

const logoMove = computed(() =>
  showDownload.value
    ? route.name === 'signin'
      ? "pl-[5rem]!"
      : "pl-[4.146rem]!"
    : route.name === 'signup'
      ? "pr-[3.25rem]!"
      : "pr-[3.21rem]!"
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
</script>
  background-color: var(--NavBarBrush);
  border-bottom: 1px solid var(--BorderBrush);
  padding: 1rem;
  margin-bottom: 2rem;
<template>
  <header>
    <nav class="navbar navbar-expand-lg">
      <div class="container-fluid">
        <LanguageFlag />

        <NavbarLogo :class="logoMove" />

        <DownloadButtonDiv>
          <DownloadButton v-if="showDownload" />
        </DownloadButtonDiv>
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
.navbar {
  background-color: var(--NavBarBrush);
  border-bottom: 1px solid var(--BorderBrush);
  padding: 1rem;
  margin-bottom: 2rem;
}

.stepperLink {
  color: var(--TextBrush);
  text-decoration: none;
}
</style>