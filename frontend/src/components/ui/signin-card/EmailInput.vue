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
    <div class="container-fluid gx-0 mb-1 mt-6">
        <p class="text-(--TextBrush) m-0 mb-1">{{ t('common.email') }}:</p>
        <input id="loginEmail" @input="onEmailChange" @blur="emit('email-blur')" type="email" placeholder="example@example.com" 
               class="w-full bg-(--ButtonBrush)! text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-(--BorderBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]">
    </div>
</template>