<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['email-change'])
const { t } = useI18n()

function onEmailChange(event) {
  const value = event.target.value
  emit('email-change', value)
}
</script>

<template>
    <div class="gx-0 loginSep container-fluid">
        <p class="text-(--TextBrush)">{{ t('common.email') }}:</p>
        <input id="loginEmail"
               @change="onEmailChange" 
               class="w-full bg-(--ButtonBrush) text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:opacity-60 m-0 p-1 ps-2 rounded-[5px] border border-(--BorderBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]" 
               placeholder="example@example.com" 
               type="text">
    </div>
</template>