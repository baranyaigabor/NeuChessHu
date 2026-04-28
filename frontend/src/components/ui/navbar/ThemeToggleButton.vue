<script setup>
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { navIconButtonClass, navMenuButtonClass, navSvgClass } from './navActionClasses.mjs'

const props = defineProps({
    isDarkTheme: {
        type: Boolean,
        default: false
    },
    menu: {
        type: Boolean,
        default: false
    }
})

const emit = defineEmits(['toggle'])
const { t } = useI18n()

const buttonClass = computed(() =>
    props.menu ? navMenuButtonClass : navIconButtonClass
)

const label = computed(() =>
    props.isDarkTheme 
        ? t('nav.lightMode') 
        : t('nav.darkMode')
)
</script>

<template>
    <button type="button" :class="buttonClass" :title="label" :aria-label="label" @click="emit('toggle')">
        <svg
            v-if="props.isDarkTheme"
            xmlns="http://www.w3.org/2000/svg"
            :class="navSvgClass"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
            aria-hidden="true">
            <circle cx="12" cy="12" r="4" />
            <path d="M12 2v2M12 20v2M4.93 4.93l1.41 1.41M17.66 17.66l1.41 1.41M2 12h2M20 12h2M4.93 19.07l1.41-1.41M17.66 6.34l1.41-1.41" />
        </svg>

        <svg
            v-else
            xmlns="http://www.w3.org/2000/svg"
            :class="navSvgClass"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
            aria-hidden="true">
            <path d="M12 3a6 6 0 0 0 9 7.4A8 8 0 1 1 12 3Z" />
        </svg>

        <span v-if="props.menu">
            {{ label }}
        </span>
    </button>
</template>