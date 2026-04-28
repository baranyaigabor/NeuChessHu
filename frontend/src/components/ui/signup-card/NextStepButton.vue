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
    <div class="container-fluid justify-content-end d-flex p-0">
        <a id="singup_BTN" @click.prevent="!props.disabled && emit('submit')" :aria-disabled="props.disabled" 
            :class="['btn mt-4 w-26 border border-(--BorderBrush)! bg-(--ButtonBrush)! text-(--TextBrush)! shadow-[inset_0_0_0_0_var(--BorderBrush)] transition hover:shadow-[inset_0_0_0_1px_var(--BorderBrush)]', props.disabled ? 'disabled opacity-50 cursor-not-allowed' : '']">
           {{ t('common.next') }}
        </a>
    </div>
</template>