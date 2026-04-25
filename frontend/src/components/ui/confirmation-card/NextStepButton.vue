<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['submit'])
const { t } = useI18n()

const props = defineProps({
  disabled: {
    type: Boolean,
    default: false
  }
})
</script>

<template>
    <div class="container-fluid d-flex justify-content-end p-0">
        <a href="#" 
           id="submit_BTN" 
           @click.prevent="!props.disabled && emit('submit')"
           :aria-disabled="props.disabled"
           :class="[
             'btn mt-4 w-24 bg-(--ButtonBrush)! text-(--TextBrush)! border border-(--BorderBrush)!',
             props.disabled ? 'disabled opacity-50 cursor-not-allowed' : ''
           ]">
           {{ t('common.confirm') }}</a>
    </div>
</template>
