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
    
</template>
