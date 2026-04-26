<script setup>
import englishFlag from '@assets/flags/English_Flag.svg'
import hungarianFlag from '@assets/flags/Hungarian_Flag.svg'
import { computed } from 'vue'
import { useI18n } from '@utils/i18n'
import { languageFlagButtonClass, navMenuButtonClass } from './navActionClasses.mjs'

const props = defineProps({
    menu: {
        type: Boolean,
        default: false
    },
    inverseFlag: {
        type: Boolean,
        default: false
    }
})

const emit = defineEmits(['toggled'])
const { locale, t, toggleLocale } = useI18n()

const flagPath = computed(() => 
{
    const isEnglish = locale.value === 'en'

    if (props.inverseFlag) {
        return isEnglish ? hungarianFlag : englishFlag
    }

    return isEnglish ? englishFlag : hungarianFlag
})

const languageTitle = computed(() =>
    locale.value === 'en'
        ? t('language.switchToHungarian')
        : t('language.switchToEnglish')
)

const buttonClass = computed(() =>
    props.menu ? navMenuButtonClass : languageFlagButtonClass
)

function handleClick() {
    toggleLocale()
    emit('toggled')
}
</script>

<template>
    <button type="button" :class="buttonClass" :title="languageTitle" :aria-label="languageTitle" @click="handleClick">
        <img :src="flagPath" :class="props.menu ? 'h-6 transition-transform duration-200 md:group-hover:scale-105 group-active:scale-95' : 'h-12 transition-transform duration-200 md:group-hover:scale-105 group-active:scale-95'" :alt="languageTitle">

        <span v-if="props.menu">
            {{ languageTitle }}
        </span>
    </button>
</template>