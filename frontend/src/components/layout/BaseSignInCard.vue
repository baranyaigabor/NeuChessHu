<script setup>
import { computed, reactive, ref, onMounted } from 'vue';
import { useRouter } from "vue-router";
import { useUserStore } from "@stores/UserStore";
import { EmailSignInInput, PasswordSignInInput, SignInButton} from '@components/ui/signin-card';
import { useI18n } from '@utils/i18n'
import { emailMessage, requiredMessage } from '@utils/validation'

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
        <div class="card -mt-12 mb-32 w-[18rem] border! border-(--BorderBrush)! bg-(--SideBarBrush)! text-(--TextBrush)">
            <div id="card-body" class="card-body">
                <form @submit.prevent="TryToSignIn">

                    <h2 class="card-title text-(--TextBrush)!">{{ t('auth.signIn') }}</h2>

                    <EmailSignInInput @email-change="handleEmailChange" @email-blur="touched.email = true" />

                    <p v-if="(touched.email || submitAttempted) && validationErrors.email" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                        {{ validationErrors.email }}
                    </p>

                    <div id="pass" class="container-fluid gx-0 my-4 mt-6">
                        <PasswordSignInInput @password-change="handlePasswordChange" @password-blur="touched.password = true" />
                        
                        <p v-if="(touched.password || submitAttempted) && validationErrors.password" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                            {{ validationErrors.password }}
                        </p>

                        <p v-if="authError" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                            {{ authError }}
                        </p>

                        <a v-if="authError" id="forgetPass" class="text-[11px] text-(--TextBrush)" href="#">
                            {{ t('auth.forgotPassword') }}
                        </a>
                    </div>

                    <SignInButton :disabled="!isFormValid" @submit="TryToSignIn"/>

                    <hr class="opacity-100 border-(--BorderChangingBrush)/24!" />

                    <RouterLink :to="{ name: 'signup' }" class="text-(--TextBrush)! no-underline hover:underline visited:text-(--TextBrush)">
                        {{ t('auth.newToChess') }}
                    </RouterLink>

                </form>
            </div>
        </div>
    </div>
</template>