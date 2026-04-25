<script setup>
import { reactive, ref, computed } from 'vue'
import {UserName, UserNameInput, UserRegionInput, UserDateOfBirthInput, NextStepButton, PreviousStepButton} from '@components/ui/personal-information-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'
import { dateOfBirthMessage, nicknameMessage, optionalNameMessage } from '@utils/validation'

const userStore = useUserStore();
const router = useRouter();
const registrationData = userStore.getRegistrationData
const firstName = ref(registrationData.first_name || "");
const lastName = ref(registrationData.last_name || "");
const nickname = ref(registrationData.nickname || '');
const region = ref(registrationData.region || "");
const dob = ref(registrationData.date_of_birth || "");
const { t } = useI18n()
const submitAttempted = ref(false)
const touched = reactive({
    nickname: false,
    firstName: false,
    lastName: false,
    dob: false
})

const validationErrors = computed(() => ({
    nickname: nicknameMessage(nickname.value),
    firstName: optionalNameMessage(firstName.value, 'common.firstName'),
    lastName: optionalNameMessage(lastName.value, 'common.lastName'),
    dob: dateOfBirthMessage(dob.value)
}))

const isFormValid = computed(() =>
    Object.values(validationErrors.value).every((error) => error === '')
)

const handleNext = async () => 
{
    submitAttempted.value = true
    touched.nickname = true
    touched.firstName = true
    touched.lastName = true
    touched.dob = true

    if (!isFormValid.value) {
        return
    }

    userStore.setPersonalInformations({
        first_name: firstName.value,
        last_name: lastName.value,
        nickname: nickname.value,
        region: region.value,
        date_of_birth: dob.value
    });
    
    router.push({ name: "confirminformation" });
};

const handlePrevious = async () => 
{
    userStore.setPersonalInformations({
        first_name: firstName.value,
        last_name: lastName.value,
        nickname: nickname.value,
        region: region.value,
        date_of_birth: dob.value
    });

    router.push({ name: "signup" });
};
</script>

<template>
    <div class="container-fluid d-flex justify-content-center align-items-center min-vh-100">
        <div class="card mb-48 w-[18rem] border! border-(--BorderBrush)! bg-(--SideBarBrush)! text-(--TextBrush)!">
            <div id="card-body" class="card-body">
                <h2 class="text-(--TextBrush)!">{{ t('registration.personalInformation') }}</h2>

                <form @submit.prevent="handleNext">
                    <div class="container-fluid px-0">

                        <div class="row">
                            <UserNameInput
                              :value="nickname"
                              @usernameChange="nickname = $event; touched.nickname = true"
                              @usernameBlur="touched.nickname = true"/>
                              
                            <p v-if="(touched.nickname || submitAttempted) && validationErrors.nickname" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                                {{ validationErrors.nickname }}
                            </p>
                        </div>

                        <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">

                        <div class="row d-flex justify-content-center">
                            <UserName
                              :firstName="firstName"
                              :lastName="lastName"
                              @firstNameChange="firstName = $event; touched.firstName = true"
                              @lastNameChange="lastName = $event; touched.lastName = true"
                              @firstNameBlur="touched.firstName = true"
                              @lastNameBlur="touched.lastName = true"/>

                            <p v-if="(touched.firstName || submitAttempted) && validationErrors.firstName" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                                {{ validationErrors.firstName }}
                            </p>

                            <p v-if="(touched.lastName || submitAttempted) && validationErrors.lastName" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                                {{ validationErrors.lastName }}
                            </p>
                        </div>

                        <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">

                        <div class="row">
                            <UserRegionInput :value="region" @regionChange="region = $event"/>
                        </div>

                        <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">

                        <div class="row">
                            <UserDateOfBirthInput :model-value="dob" @dateOfBirthChange="dob = $event; touched.dob = true"/>
                            
                            <p v-if="(touched.dob || submitAttempted) && validationErrors.dob" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
                                {{ validationErrors.dob }}
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