<script setup>
import { computed, reactive, ref, onMounted } from 'vue';
import { useRouter } from "vue-router";
import { useUserStore } from "@stores/UserStore";
import {  EmailSignInInput,  OrSeparator,  OtherSignIn,  PasswordSignInInput,  SignInButton} from '@components/ui/signin-card';
import { useI18n } from '@utils/i18n'
import { emailMessage, requiredMessage } from '@utils/validation.mjs'

const userStore = useUserStore();
const userEmail = ref('')
const router = useRouter()
const userPassword = ref('')
const redirectUri = ref(null)
const { t } = useI18n()
const touched = reactive({
  email: false,
  password: false
})
const submitAttempted = ref(false)
const authError = ref('')

const validationErrors = computed(() => ({
  email: emailMessage(userEmail.value),
  password: requiredMessage(userPassword.value, 'common.password')
}))

const isFormValid = computed(() =>
  Object.values(validationErrors.value).every((error) => error === '')
)

onMounted(() => {
  redirectUri.value = new URLSearchParams(window.location.search).get('redirect_uri')
})

function handleEmailChange(newEmail) 
{
  userEmail.value = newEmail
  touched.email = true
  authError.value = ''
}

function handlePasswordChange(newPassword) 
{
  userPassword.value = newPassword
  touched.password = true
  authError.value = ''
}

async function TryToSignIn() 
{
  submitAttempted.value = true
  touched.email = true
  touched.password = true
  authError.value = ''

  if (!isFormValid.value) 
  {
    return
  }

  try 
  {
    if (redirectUri.value) 
    {
      await userStore.login({
        email: userEmail.value,
        password: userPassword.value,
        redirect_uri: redirectUri.value
      })
      
      if (userStore.token) 
      {
        window.location.href = 'http://backend.vm2.test/oauth/close?token=' + encodeURIComponent(userStore.token) + '&redirect_uri=' + encodeURIComponent(redirectUri.value)
      }
    } 

    else 
    {
      const response = await userStore.login({
        email: userEmail.value,
        password: userPassword.value
      })

      router.push({ name: "user", params: { nickname:response.nickname } });
    }
  } 

  catch (error) 
  {
    authError.value = t('auth.invalidCredentials')
  }
}
</script>

<template>
  <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100">
    <div class="card" style="width: 18rem;">
      <div id="card-body" class="card-body">

        <form @submit.prevent="TryToSignIn">

          <h2 class="card-title text-(--TextBrush)!" style="color: var(--TextBrush);">{{ t('auth.signIn') }}</h2>

          <EmailSignInInput
            @email-change="handleEmailChange"
            @email-blur="touched.email = true" />
          <p v-if="(touched.email || submitAttempted) && validationErrors.email" class="mt-1 mx-1 text-[11px] text-danger">
            {{ validationErrors.email }}
          </p>

          <div id="pass" class="gx-0 loginSep container-fluid">
            <PasswordSignInInput
              @password-change="handlePasswordChange"
              @password-blur="touched.password = true" />
            <p v-if="(touched.password || submitAttempted) && validationErrors.password" class="mt-1 mx-1 text-[11px] text-danger">
              {{ validationErrors.password }}
            </p>
            <p v-if="authError" class="mt-1 mx-1 text-[11px] text-danger">
              {{ authError }}
            </p>
            <a v-if="authError" id="forgetPass" class="text-[11px] text-(--TextBrush)" href="#">
              {{ t('auth.forgotPassword') }}
            </a>
          </div>

          <SignInButton :disabled="!isFormValid" @submit="TryToSignIn"/>

          <OrSeparator />
          <OtherSignIn />

          <hr class="loginSep_HR border-(--BorderChangingBrush)/24!" />

          <RouterLink :to="{ name: 'signup' }">
            {{ t('auth.newToChess') }}
          </RouterLink>

        </form>

      </div>
    </div>
  </div>
</template>

<style lang="css">
.row > * {
  padding-left: 0 !important;
}

.card {
    border: 1px solid var(--BorderBrush) !important;
    background-color: var(--SideBarBrush);
    margin-top: -3rem;
    margin-bottom: 8rem;
}

.loginSep_HR {
    opacity: 1;
}

p {
    font-size: 14px;
    margin: 0;
    padding: 0;
}

label {
    color: var(--TextBrush);
    font-size: 10px;
    margin: 0;
    padding-left: 0;
    padding-top: 0;
    padding-right: 0.25rem;
    padding-bottom: 0;
}

.loginSep {
    margin-top: 1.5rem;
    margin-bottom: 1rem;
}

.icon {
    height: 1rem;
    width: 1rem;
    padding: 0;
}
</style>
