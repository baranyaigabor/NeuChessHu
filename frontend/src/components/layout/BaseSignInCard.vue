<script setup>
import { ref, onMounted } from 'vue';
import { useRouter } from "vue-router";
import { useUserStore } from "@stores/UserStore";
import {  EmailSignInInput,  OrSeparator,  OtherSignIn,  PasswordSignInInput,  SignInButton} from '@components/ui/signin-card';
import { useI18n } from '@utils/i18n'

const userStore = useUserStore();
const userEmail = ref('')
const router = useRouter()
const userPassword = ref('')
const redirectUri = ref(null)
const { t } = useI18n()

onMounted(() => {
  redirectUri.value = new URLSearchParams(window.location.search).get('redirect_uri')
})

function handleEmailChange(newEmail) 
{
  userEmail.value = newEmail
}

function handlePasswordChange(newPassword) 
{
  userPassword.value = newPassword
}

function LoginError() {
  try 
  {
    const forgetLink = document.getElementById("forgetPass")

    if (forgetLink)
    {
      forgetLink.remove()
    }
  } 
  catch 
  {

  }

  const errorDiv = document.getElementById("pass")
  const errorForgetPass = document.createElement("a")

  errorForgetPass.textContent = t('auth.forgotPassword')
  errorForgetPass.href = "#"

  errorForgetPass.id = "forgetPass"
  errorDiv.appendChild(errorForgetPass)
}

async function TryToSignIn() 
{
  if (!userEmail.value || !userPassword.value) 
  {
    LoginError()
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
    LoginError()
  }
}
</script>

<template>
  <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100">
    <div class="card" style="width: 18rem;">
      <div id="card-body" class="card-body">

        <form @submit.prevent="TryToSignIn">

          <h2 class="card-title text-(--TextBrush)!" style="color: var(--TextBrush);">{{ t('auth.signIn') }}</h2>

          <EmailSignInInput @email-change="handleEmailChange" />

          <div id="pass" class="gx-0 loginSep container-fluid">
            <PasswordSignInInput @password-change="handlePasswordChange" />
            <div id="forgetDiv"></div>
          </div>

          <SignInButton @submit="TryToSignIn"/>

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