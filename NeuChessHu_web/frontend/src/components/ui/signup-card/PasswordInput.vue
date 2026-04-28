<script setup>
import { ref } from 'vue'
import { useI18n } from '@utils/i18n'

const props = defineProps({
    password: String,
    confirmPassword: String,
    passwordError: {
        type: String,
        default: ''
    },
    confirmPasswordError: {
        type: String,
        default: ''
    }
})

const emit = defineEmits(['update:password', 'update:confirmPassword', 'password-blur', 'confirm-password-blur'])

const showPassword = ref(false)
const { t } = useI18n()

function show() 
{
    showPassword.value = true
}

function hide() 
{
    showPassword.value = false
}

function onPasswordInput(e) 
{
    emit('update:password', e.target.value)
}

function onConfirmPasswordInput(e) 
{
    emit('update:confirmPassword', e.target.value)
}
</script>

<template>
    <div class="d-flex p-0">
        <p class="w-full mt-4! mb-1 text-(--TextBrush)">{{ t('common.password') }}: *</p>
        <div class="d-flex justify-content-end w-full">
            <div id="password" class="d-flex justify-content-end text-[8px] w-full">
                <button type="button" class="me-1 mt-[0.8rem]! cursor-pointer border-0 bg-transparent p-0 !text-[var(--TextBrush)] hover:!text-[var(--TextBrush)] focus:!text-[var(--TextBrush)] active:!text-[var(--TextBrush)] focus:outline-none" @mousedown.prevent="show" @mouseup.prevent="hide" @mouseleave.prevent="hide">                    
                    <svg
                        v-if="showPassword"
                        xmlns="http://www.w3.org/2000/svg"
                        class="h-4 w-4"
                        viewBox="0 0 24 24"
                        fill="none"
                        stroke="currentColor"
                        stroke-width="2"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        aria-hidden="true">

                        <path d="M2 12s3.5-7 10-7 10 7 10 7-3.5 7-10 7-10-7-10-7Z" />
                        <circle cx="12" cy="12" r="3" />
                    </svg>
                    <svg
                        v-else
                        xmlns="http://www.w3.org/2000/svg"
                        class="h-4 w-4"
                        viewBox="0 0 24 24"
                        fill="none"
                        stroke="currentColor"
                        stroke-width="2"
                        stroke-linecap="round"
                        stroke-linejoin="round"
                        aria-hidden="true">

                        <path d="M2 12s3.5-7 10-7c2.2 0 4 .8 5.5 1.8" />
                        <path d="M22 12s-3.5 7-10 7c-2.2 0-4-.8-5.5-1.8" />
                        <path d="M4 4l16 16" />
                        <path d="M9.9 9.9a3 3 0 0 0 4.2 4.2" />
                    </svg>
                </button>
            </div>
        </div>
    </div>

    <input :type="showPassword ? 'text' : 'password'" :value="password" @input="onPasswordInput" @blur="emit('password-blur')" placeholder="*********"
        class="gx-0 m-0 w-full rounded-[5px] border border-(--BorderBrush) bg-(--ButtonBrush) !p-1 !ps-2 text-(--FieldTextBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] placeholder:text-(--FieldTextBrush) placeholder:tracking-[0.08rem] placeholder:opacity-60">

    <p v-if="passwordError" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
        {{ passwordError }}
    </p>

    <p class="mt-5! mb-1 p-0 text-(--TextBrush)">{{ t('common.confirmPassword') }}: *</p>
    <input :type="showPassword ? 'text' : 'password'" :value="confirmPassword" @input="onConfirmPasswordInput" @blur="emit('confirm-password-blur')" placeholder="*********"
        class="gx-0 m-0 w-full rounded-[5px] border border-(--BorderBrush) bg-(--ButtonBrush) !p-1 !ps-2 text-(--FieldTextBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] placeholder:text-(--FieldTextBrush) placeholder:tracking-[0.08rem] placeholder:opacity-60">

    <p v-if="confirmPasswordError" class="m-0 mx-1 mt-1 p-0 text-[11px] text-danger">
        {{ confirmPasswordError }}
    </p>
</template>