<script setup>
import { ref } from 'vue';
import { useI18n } from '@utils/i18n'

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

const emit = defineEmits(['password-change', 'password-blur'])

function onPasswordChange(event) 
{
  const value = event.target.value
  emit('password-change', value)
}
</script>

<template>
    <div class="d-flex">
        <p class="text-(--TextBrush)">{{ t('common.password') }}:</p>
        <div id="password" class="d-flex justify-content-end text-[8px] w-full">
            <a href="#" class="me-1 text-(--TextBrush)" @mousedown.prevent="show" @mouseup.prevent="hide" @mouseleave.prevent="hide" id="showPass_A">
                <svg v-if="showPassword"
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
                <svg v-else
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
            </a>
        </div>
    </div>
    <input id="loginPass" @input="onPasswordChange" @blur="emit('password-blur')" class="w-full m-0 bg-(--ButtonBrush) text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:tracking-[0.08rem] placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-(--BorderBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]" 
           placeholder="*********" :type="showPassword ? 'text' : 'password'">
</template>