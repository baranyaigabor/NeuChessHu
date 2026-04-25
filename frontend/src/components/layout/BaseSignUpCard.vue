<script setup>
import { reactive, ref, computed } from 'vue'
import {EmailSignInInput, PasswordSignInInput, SignUpNextStepButton} from '@components/ui/signup-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'
import { confirmPasswordMessage, emailMessage, passwordMessage } from '@utils/validation.mjs'

const router = useRouter();
const userStore = useUserStore();
const { t } = useI18n()
const registrationData = userStore.getRegistrationData
const email = ref(registrationData.email || '')
const password = ref(registrationData.password || '')
const confirmPassword = ref('')

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
    <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100">
      <div class="card" style="width: 18rem;">
        <div id="card-body" class="card-body">
        
          <form @submit.prevent="handleSignupNextStep">
            <div class="container-fluid">
                <div class="row">
                    <h2 class="card-title ps-0 text-(--TextBrush)!" style="color: var(--TextBrush);">{{ t('auth.signUp') }}</h2>
                </div>
                
                <div class="row">
                    <EmailSignInInput
                      :value="email"
                      @email-change="email = $event; touched.email = true"
                      @email-blur="touched.email = true"/>
                    <p v-if="(touched.email || submitAttempted) && validationErrors.email" class="mt-1 mx-1 p-0 text-[11px] text-danger">
                      {{ validationErrors.email }}
                    </p>
                </div>
                
                <div class="row">
                    <PasswordSignInInput
                      :password="password"
                      :confirmPassword="confirmPassword"
                      :passwordError="(touched.password || submitAttempted) ? validationErrors.password : ''"
                      :confirmPasswordError="(touched.confirmPassword || submitAttempted) ? validationErrors.confirmPassword : ''"
                      @update:password="password = $event; touched.password = true"
                      @update:confirmPassword="confirmPassword = $event; touched.confirmPassword = true"
                      @password-blur="touched.password = true"
                      @confirm-password-blur="touched.confirmPassword = true"/>
                </div>
                
                <div class="row">
                    <div id="alerts">
                      <p v-if="alertMessage" :class="['mt-1 mx-1 p-0 text-[11px]', alertType === 'danger' ? 'text-danger' : 'text-success']">
                        {{ alertMessage }}
                      </p>
                    </div>
                </div>
                
                <div class="row">
                    <SignUpNextStepButton :disabled="!isFormValid" @submit="handleSignupNextStep"/>
                </div>
            </div>
        
        
            <hr class="loginSep_HR !border-(--BorderChangingBrush)]/[0.24">
        
            <RouterLink class="text-(--TextBrush)" :to="{name: 'signin'}">
              {{ t('auth.alreadyHaveAccount') }}
            </RouterLink>
        
          </form>
      
        </div>
      </div>
    </div>
</template>

<style lang="css">
.card{
    border: 1px solid var(--BorderBrush);
    background-color: var(--SideBarBrush);
    margin-top: -5rem;
    margin-bottom: 8rem;
}

a{
    color: var(--TextBrush);
}

p{
    font-size: 14px;
    margin: 0;
    padding: 0;
}

label{
    color: var(--TextBrush);
    font-size: 10px;
    margin: 0;
    padding-left: 0;
    padding-top: 0;
    padding-right: 0.25rem;
    padding-bottom: 0;
}

.loginSep{
    margin-top: 1.5rem;
    margin-bottom: 1rem;
}
</style>
