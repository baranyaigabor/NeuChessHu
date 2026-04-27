<script setup>
import { computed, ref } from 'vue'
import { ConfirmInfos, NextStepButton, PreviousStepButton } from '@components/ui/confirmation-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'
import { emailMessage, nicknameMessage, passwordMessage } from '@utils/validation'
import { termsUrl } from '@utils/docs'

const userStore = useUserStore();
const router = useRouter();
const acceptedTerms = ref(false)
const { locale, t } = useI18n()
const submitAttempted = ref(false)
const registerErrorKey = ref('')
const registerError = computed(() => registerErrorKey.value ? t(registerErrorKey.value) : '')
const termsHref = computed(() => termsUrl(locale.value))

const validationErrors = computed(() => 
{
    const rawData = userStore.registrationData

    return {
        nickname: nicknameMessage(rawData.nickname || ''),
        email: emailMessage(rawData.email || ''),
        password: passwordMessage(rawData.password || ''),
        terms: acceptedTerms.value ? '' : t('validation.acceptTerms')
    }
})

const isFormValid = computed(() =>
    Object.values(validationErrors.value).every((error) => error === '')
)

const handleNext = async () => 
{
    submitAttempted.value = true
    registerError.value = ''

    if (!isFormValid.value) 
    {
        return
    }

    const rawData = userStore.registrationData

    const userNickname = rawData.nickname

    try
    {
        await userStore.register({
            "nickname": rawData.nickname,
            "email": rawData.email,
            "password": rawData.password,
            "first_name": rawData.first_name === "" ? null : rawData.first_name,
            "last_name": rawData.last_name === "" ? null : rawData.last_name,
            "region": rawData.region === "" ? null : rawData.region,
            "profile_picture": null,
            "date_of_birth": rawData.date_of_birth === "" ? null : rawData.date_of_birth
        })

        userStore.resetRegistrationData()
        router.push(`/user/${encodeURIComponent(userNickname)}`)
    }

    catch (error)
    {
        const message = error.response?.data?.message ?? ''

        if (message.includes('users_nickname_unique') || message.includes('Duplicate entry'))
        {
            registerErrorKey.value = 'validation.nicknameTaken'
        }
        else
        {
            registerErrorKey.value = message ? '' : 'auth.networkError'

            if (message)
            {
                registerError
            }
        }
    }
}

const handlePrevious = () => {
    router.push({ name: "personalinformation" });
};
</script>

<template>
    <div class="flex min-h-[60vh] items-center my-4 justify-center"> 
        <div class="card confirmation-card w-full max-w-[18rem] border! border-(--BorderBrush)! bg-(--SideBarBrush)! text-(--TextBrush)">
            <div id="card-body" class="card-body">

                <form @submit.prevent="handleNext">
                    <div class="container-fluid">

                        <div class="row">
                            <ConfirmInfos />
                        </div>

                        <div class="row">
                            <div class="d-flex justify-content-center">
                                <div class="form-check">
                                    <input class="form-check-input mb-0.5" type="checkbox" id="terms" v-model="acceptedTerms">
                                    <label class="form-check-label mt-1 p-0 pr-1 text-xs text-(--TextBrush)!" for="terms">
                                        {{ t('registration.acceptTermsPrefix') }}
                                        <a :href="termsHref" class="text-(--TextBrush)! **:underline" target="_blank" rel="noopener noreferrer" @click.stop>
                                            {{ t('registration.termsLinkText') }}
                                        </a>
                                    </label>
                                </div>
                            </div>

                            <p v-if="submitAttempted && validationErrors.terms" class="m-0 mx-[0.7rem]! mt-1 p-0  text-[11px] text-danger">
                                {{ validationErrors.terms }}
                            </p>

                            <p v-if="submitAttempted && (validationErrors.nickname || validationErrors.email || validationErrors.password)" class="m-0 mx-[0.7rem]! mt-1 p-0  text-[11px] text-danger">
                                {{ t('validation.fixRegistrationFields') }}
                            </p>

                            <p v-if="registerError" class="m-0 mx-[0.7rem]! mt-1 p-0  text-[11px] text-danger">
                                {{ registerError }}
                            </p>

                        </div>

                        <div class="row">
                            <div class="d-flex justify-content-between p-0">
                                <PreviousStepButton @submit="handlePrevious" />
                                <NextStepButton :disabled="!isFormValid" @submit="handleNext" />
                            </div>
                        </div>

                    </div>
                </form> 

            </div>
        </div>
    </div>
</template>