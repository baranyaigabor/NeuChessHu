<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['submit'])
const { t } = useI18n()
</script>

<template>
    <div class="container-fluid d-flex justify-content-end p-0">
        <a href="#" 
           id="submit_BTN" 
           @click.prevent="emit('submit')" 
           class="btn mt-4 w-[6rem] bg-(--ButtonBrush)! text-(--TextBrush)! border border-(--BorderBrush)!">
           {{ t('common.confirm') }}</a>
    </div>
</template>