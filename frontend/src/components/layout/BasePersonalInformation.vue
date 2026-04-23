<script setup>
import { ref, computed } from 'vue'
import {UserName, UserNameInput, UserRegionInput, UserDateOfBirthInput, NextStepButton, PreviousStepButton} from '@components/ui/personal-information-card'
import { useUserStore } from "@stores/UserStore";
import { useRouter } from "vue-router";
import { useI18n } from '@utils/i18n'

const userStore = useUserStore();
const router = useRouter();
const firstName = ref("");
const lastName = ref("");
const nickname = ref('');
const region = ref("");
const dob = ref("");
const { t } = useI18n()

const isFormValid = computed(() =>
 nickname.value.trim() !== ''
)

const handleNext = async () => 
{
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
        <div class="card" style="width: 18rem;">
            <div id="card-body" class="card-body">
                <h2 class="text-(--TextBrush)!" style="color: var(--TextBrush);">{{ t('registration.personalInformation') }}</h2>

                <form @submit.prevent="handleNext">
                    <div class="container-fluid px-0">

                        <div class="row">
                            <UserNameInput :value="userStore.getRegistrationData.nickname" @usernameChange="nickname = $event"/>
                        </div>

                        <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">

                        <div class="row d-flex justify-content-center">
                            <UserName :firstName="userStore.getRegistrationData.first_name" @firstNameChange="firstName = $event" :lastName="userStore.getRegistrationData.last_name" @lastNameChange="lastName = $event"/>
                        </div>

                        <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">

                        <div class="row">
                            <UserRegionInput :value="userStore.getRegistrationData.region" @regionChange="region = $event"/>
                        </div>

                        <hr class="loginSep_HR border-(--BorderChangingBrush)/24!">

                        <div class="row">
                            <UserDateOfBirthInput v-model="userStore.getRegistrationData.date_of_birth" @dateOfBirthChange="dob = $event"/>
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
.card{
    border: 1px solid var(--BorderBrush);
    background-color: var(--SideBarBrush);
    margin-bottom: 12rem;
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
</style>