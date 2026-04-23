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
        <a id="singup_BTN"
           @click.prevent="!props.disabled && emit('submit')" 
           class="btn w-18 bg-(--ButtonBrush)! text-(--TextBrush)! border border-(--BorderBrush)!">
           {{ t('common.next') }}
        </a>
    </div>
</template>