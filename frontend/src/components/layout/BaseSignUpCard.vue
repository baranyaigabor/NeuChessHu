<script setup>
import { ref, computed } from 'vue'
import {EmailSignInInput, PasswordSignInInput, SignUpNextStepButton} from '@components/ui/signup-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'

const email = ref('')
const password = ref('')
const router = useRouter();
const confirmPassword = ref('')
const userStore = useUserStore();
const { t } = useI18n()

const alertMessage = ref('')
const alertType = ref('')

const isFormValid = computed(() => {
  return (
    email.value.trim() !== '' &&
    password.value.trim() !== '' &&
    confirmPassword.value.trim() !== '' &&
    password.value === confirmPassword.value
  )
})

const handleSignupNextStep = async () => 
{
    alertMessage.value = ''
    alertType.value = ''

    if (password.value !== confirmPassword.value) 
    {
        alertType.value = 'danger'
        alertMessage.value = t('auth.passwordsDoNotMatch')
        return
    }
    
    try 
    {
        if (password.value !== confirmPassword.value) 
        {
            alertType.value = "danger";
            alertMessage.value = t('auth.passwordsDoNotMatch');
            return;
        }

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
                    <EmailSignInInput :value="userStore.getRegistrationData.email" @email-change="email = $event"/>
                </div>
                
                <div class="row">
                    <PasswordSignInInput :password="userStore.getRegistrationData.password || password" :confirmPassword="userStore.getRegistrationData.password || confirmPassword" @update:password="password = $event" @update:confirmPassword="confirmPassword = $event"/>
                </div>
                
                <div class="row">
                    <div id="alerts"></div>
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