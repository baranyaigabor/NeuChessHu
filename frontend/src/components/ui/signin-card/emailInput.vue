<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['email-change', 'email-blur'])
const { t } = useI18n()

function onEmailChange(event) {
  const value = event.target.value
  emit('email-change', value)
}
</script>

<template>
    <div class="gx-0 container-fluid signin-email-field">
        <p class="text-(--TextBrush)">{{ t('common.email') }}:</p>
        <input id="loginEmail"
               @input="onEmailChange"
               @blur="emit('email-blur')"
               class="w-full bg-(--ButtonBrush) text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:opacity-60 m-0 p-1 ps-2 rounded-[5px] border border-(--BorderBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]" 
               placeholder="example@example.com" 
               type="email">
    </div>
</template>

<style lang="css">
.signin-email-field {
  margin-top: 1.5rem;
  margin-bottom: 0.25rem;
}
</style>
