<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['submit'])
const { t } = useI18n()
</script>

<template>
    <div class="container-fluid justify-content-start d-flex">
        <a href="#" id="submit_BTN" @click.prevent="emit('submit')" class="btn mt-4 w-24 bg-(--ButtonBrush)! text-(--TextBrush)! border border-(--BorderBrush)!">{{ t('common.previous') }}</a>
    </div>
</template>