<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['submit'])
const { t } = useI18n()
</script>

<template>
    <div class="container-fluid justify-content-start d-flex">
        <a href="#" id="submit_BTN" @click.prevent="emit('submit')" class="btn mt-4 w-26 border border-(--BorderBrush)! bg-(--ButtonBrush)! text-(--TextBrush)! shadow-[inset_0_0_0_0_var(--BorderBrush)] transition hover:shadow-[inset_0_0_0_1px_var(--BorderBrush)]">{{ t('common.previous') }}</a>
    </div>
</template>