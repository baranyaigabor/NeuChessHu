<script setup>
import { reactive, ref, computed } from 'vue'
import {EmailSignInInput, PasswordSignInInput, SignUpNextStepButton} from '@components/ui/signup-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'
import { confirmPasswordMessage, emailMessage, passwordMessage } from '@utils/validation'

const router = useRouter();
const userStore = useUserStore();
const { t } = useI18n()
const registrationData = userStore.getRegistrationData
const email = ref(registrationData.email || '')
const password = ref(registrationData.password || '')
const confirmPassword = ref(registrationData.password || '')

const alertMessage = ref('')
const alertType = ref('')
const submitAttempted = ref(false)
const touched = reactive({
  email: false,
  password: false,
  confirmPassword: false
})

const validationErrors = computed(() => ({
  email: emailMessage(email.value),
  password: passwordMessage(password.value),
  confirmPassword: confirmPasswordMessage(password.value, confirmPassword.value)
}))

const isFormValid = computed(() =>
  Object.values(validationErrors.value).every((error) => error === '')
)

const handleSignupNextStep = async () => 
{
    submitAttempted.value = true
    touched.email = true
    touched.password = true
    touched.confirmPassword = true
    alertMessage.value = ''
    alertType.value = ''

    if (!isFormValid.value) 
    {
        alertType.value = 'danger'
        alertMessage.value = t('validation.fixHighlighted')
        return
    }
    
    try 
    {
        userStore.setCredentials({
            email: email.value,
            password: password.value
        });

        router.push({ name: "personalinformation" });
    } 
    catch (error) 
    {   
        alertType.value = 'danger'
        alertMessage.value = t('auth.networkError')
    }
}
</script>

<template>
<div class="flex min-h-[60vh] items-center my-4 justify-center"> 
    <div class="card w-[18rem] !border [--bs-card-bg:var(--SideBarBrush)] [--bs-card-border-color:var(--BorderBrush)] !border-[var(--BorderBrush)] !bg-[var(--SideBarBrush)] text-[var(--TextBrush)]">
        <div id="card-body" class="card-body">
            <form @submit.prevent="handleSignupNextStep">
                <div class="container-fluid">
                    <h2 class="text-(--TextBrush)! -ml-[0.7rem]!">{{ t('auth.signUp') }}</h2>

                    <div class="row">
                        <EmailSignInInput :value="email" @email-change="email = $event; touched.email = true" @email-blur="touched.email = true"/>

                        <p v-if="(touched.email || submitAttempted) && validationErrors.email" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                            {{ validationErrors.email }}
                        </p>
                    </div>

                    <div class="row">
                        <PasswordSignInInput :password="password" :confirmPassword="confirmPassword"
                            :passwordError="(touched.password || submitAttempted) ? validationErrors.password : ''"
                            :confirmPasswordError="(touched.confirmPassword || submitAttempted) ? validationErrors.confirmPassword : ''"
                            @update:password="password = $event; touched.password = true"
                            @update:confirmPassword="confirmPassword = $event; touched.confirmPassword = true"
                            @password-blur="touched.password = true"
                            @confirm-password-blur="touched.confirmPassword = true"/>
                    </div>

                    <div class="row">
                        <div id="alerts">
                                <p v-if="alertMessage" :class="['m-0 mx-1 mt-1 p-0 text-[11px]', alertType === 'danger' ? 'text-danger' : 'text-success']">
                                {{ alertMessage }}
                            </p>
                        </div>
                    </div>

                    <div class="row">
                        <SignUpNextStepButton :disabled="!isFormValid" @submit="handleSignupNextStep"/>
                    </div>
                </div>
          
                <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">
          
                <RouterLink class="text-(--TextBrush)! no-underline hover:underline visited:text-(--TextBrush)" :to="{name: 'signin'}">
                    {{ t('auth.alreadyHaveAccount') }}
                </RouterLink>
          
            </form>
      
        </div>
    </div>
</div>
</template>