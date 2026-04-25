<script setup>
import { computed, ref } from 'vue'
import { ConfirmInfos, NextStepButton, PreviousStepButton } from '@components/ui/confirmation-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'
import { emailMessage, nicknameMessage, passwordMessage } from '@utils/validation'

const userStore = useUserStore();
const router = useRouter();
const acceptedTerms = ref(false)
const { t } = useI18n()
const submitAttempted = ref(false)

const validationErrors = computed(() => {
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

    if (!isFormValid.value) 
    {
        return
    }

    const rawData = userStore.registrationData

    const userNickname = rawData.nickname

    userStore.register({
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
    router.push({ name: "user", params: { nickname:userNickname } })
}

const handlePrevious = () => {
    router.push({ name: "personalinformation" });
};
</script>

<template>
    <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100 px-2 sm:px-4">
        <div class="card confirmation-card w-full max-w-[18rem]">
            <div id="card-body" class="card-body">

                <form @submit.prevent="handleNext">
                    <div class="container-fluid">

                        <div class="row">
                            <ConfirmInfos />
                        </div>

                        <div class="row">
                            <div class="d-flex justify-content-center">
                                <div class="form-check">
                                    <input 
                                        class="form-check-input" 
                                        type="checkbox" 
                                        id="terms" 
                                        v-model="acceptedTerms"
                                    >
                                    <label class="form-check-label" for="terms">
                                        {{ t('registration.acceptTerms') }}
                                    </label>
                                </div>
                            </div>
                            <p v-if="submitAttempted && validationErrors.terms" class="mt-1  mx-1  p-0 text-center text-[11px] text-danger">
                                {{ validationErrors.terms }}
                            </p>
                            <p v-if="submitAttempted && (validationErrors.nickname || validationErrors.email || validationErrors.password)" class="mt-1  mx-1 p-0 text-center text-[11px] text-danger">
                                {{ t('validation.fixRegistrationFields') }}
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

<style lang="css">
.card {
    border: 1px solid var(--BorderBrush) !important;
    background-color: var(--SideBarBrush);
    margin-bottom: 12rem;
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
</style>
