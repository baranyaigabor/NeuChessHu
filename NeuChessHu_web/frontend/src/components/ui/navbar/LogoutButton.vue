<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { navIconButtonClass, navMenuButtonClass, navSvgClass } from './navActionClasses.mjs'

const props = defineProps({
    menu: {
        type: Boolean,
        default: false
    }
})

const emit = defineEmits(['logout'])
const { t } = useI18n()

const buttonClass = computed(() =>
    props.menu ? navMenuButtonClass : navIconButtonClass
)
</script>

<template>
    <button type="button" :class="buttonClass" :title="t('nav.logout')" :aria-label="t('nav.logout')" @click="emit('logout')">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            :class="navSvgClass"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
            aria-hidden="true">
            <path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4" />
            <path d="M16 17l5-5-5-5" />
            <path d="M21 12H9" />
        </svg>

        <span v-if="props.menu">
            {{ t('nav.logout') }}
        </span>
    </button>
</template>