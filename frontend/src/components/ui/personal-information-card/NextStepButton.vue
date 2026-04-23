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
    <div class="container-fluid justify-content-end d-flex">
        <a href="#" id="submit_BTN" @click.prevent="!props.disabled && emit('submit')" class="btn mt-4 w-[5.5rem] bg-(--ButtonBrush)! text-(--TextBrush)! border border-(--BorderBrush)!">{{ t('common.next') }}</a>
    </div>
</template>
