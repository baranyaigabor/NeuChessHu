<script setup>
import { useI18n } from '@utils/i18n'

const emit = defineEmits(['submit'])
const { t } = useI18n()
</script>

<template>
    <div class="container-fluid d-flex justify-content-center">
        <a @click.prevent="emit('submit')" 
           id="login_BTN" 
           class="btn bg-(--ButtonBrush)! text-(--TextBrush)! border border-(--BorderBrush)">
           {{ t('auth.signIn') }}
        </a>
    </div>
</template>