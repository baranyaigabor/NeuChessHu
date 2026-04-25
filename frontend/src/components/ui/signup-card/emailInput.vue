<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
  value: {
    type: String,
    default: ""
  }
})

const emit = defineEmits(['email-change', 'email-blur'])
const { t } = useI18n()

function onEmailChange(event) {
  const value = event.target.value
  emit('email-change', value)
}
</script>

<template>
    <div class="gx-0 container-fluid signup-email-field">
        <p class="text-(--TextBrush)">{{ t('common.email') }}: *</p>
        <input id="loginEmail" 
               @input="onEmailChange"
               @blur="emit('email-blur')" 
               :value="props.value" 
               class="w-full m-0 bg-(--ButtonBrush) text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-(--BorderBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]" 
               placeholder="example@example.com" 
               type="email">
    </div>
</template>

<style lang="css">
.signup-email-field {
  margin-top: 1.5rem;
  margin-bottom: 0.25rem;
}
</style>
