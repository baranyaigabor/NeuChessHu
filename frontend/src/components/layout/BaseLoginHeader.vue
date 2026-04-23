<script setup>
import { Stepper, StepperIndicator, StepperItem, StepperSeparator, StepperTitle, StepperTrigger } from '@/components/ui/stepper'
import { LanguageFlag, NavbarLogo, DownloadButtonDiv, DownloadButton } from '@components/ui/login-navbar'

import { computed } from 'vue'
import { useRoute } from 'vue-router'
import { useUserStore } from "@stores/UserStore";

const userStore = useUserStore();
const rawData = userStore.registrationData
const route = useRoute()

const showDownload = computed(() =>
  route.name === 'welcome' || route.name === 'signin'
)

const logoMove = computed(() =>
  showDownload.value
    ? route.name === 'signin'
      ? "!pl-[5rem]"
      : "!pl-[4.146rem]"
    : route.name === 'signup'
      ? "!pr-[3.25rem]"
      : "!pr-[3.21rem]"
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

  <div id="stepper"
    class="flex items-center justify-center w-full"
    v-if="showSignUpStepper">
    <Stepper>

      <StepperItem :step="stepperSignUpStep">
        <StepperTrigger>
          <RouterLink class="stepperLink" to="signup">
            <StepperIndicator>1</StepperIndicator>
            <StepperTitle>Sign Up</StepperTitle>
          </RouterLink>
        </StepperTrigger>
        <StepperSeparator />
      </StepperItem>

      <StepperItem :step="stepperPersonalInformationStep">
        <StepperTrigger>
          <component
            :is="stepperPersonalInformationStepClick ? 'RouterLink' : 'div'"
            :to="stepperPersonalInformationStepClick ? 'personalinformation' : null"
            class="stepperLink"
            :class="!stepperPersonalInformationStepClick ? 'pointer-events-none opacity-50' : ''">
            
            <StepperIndicator>2</StepperIndicator>
            <StepperTitle>Personal information</StepperTitle>
          </component>
        </StepperTrigger>
        <StepperSeparator />
      </StepperItem>

      <StepperItem :step="stepperConfirmationStep">
        <StepperTrigger>
          <component
            :is="stepperConfirmationStepClick ? 'RouterLink' : 'div'"
            :to="stepperConfirmationStepClick ? 'confirmation' : null"
            class="stepperLink"
            :class="!stepperConfirmationStepClick ? 'pointer-events-none opacity-50' : ''">

            <StepperIndicator>3</StepperIndicator>
            <StepperTitle>Final steps</StepperTitle>
          </component>
        </StepperTrigger>
        <StepperSeparator />
      </StepperItem>

    </Stepper>
  </div>
</template>

<style lang="css">
nav {
  background-color: #D0B399;
  border-bottom: 1px solid rgb(0, 0, 0);
  padding: 1rem;
  margin-bottom: 2rem;
}

.stepperLink {
  color: black;
  text-decoration: none;
}
</style>
