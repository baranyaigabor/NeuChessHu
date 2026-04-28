<script setup>
import { onBeforeUnmount, onMounted, ref } from 'vue'
import { useI18n } from '@utils/i18n'

const props = defineProps({
    compact: {
        type: Boolean,
        default: false
    }
})

const { t } = useI18n()
const isOpen = ref(false)
const downloadRoot = ref(null)

const downloadOptions = 
[
    {
        label: 'Windows x64',
        href: 'https://github.com/baranyaigabor/NeuChessHu/releases/download/v1.0/NeuChessHu-win-x64.zip',
    },
    {
        label: 'Windows ARM64',
        href: 'https://github.com/baranyaigabor/NeuChessHu/releases/download/v1.0/NeuChessHu-win-arm64.zip',
    },
]

function toggleMenu() 
{
    isOpen.value = !isOpen.value
}

function closeMenu() 
{
    isOpen.value = false
}

function handleClickOutside(event) 
{
    if (!downloadRoot.value?.contains(event.target)) 
    {
        closeMenu()
    }
}

function handleEscape(event) 
{
    if (event.key === 'Escape') 
    {
        closeMenu()
    }
}

onMounted(() => 
{
    document.addEventListener('click', handleClickOutside)
    document.addEventListener('keydown', handleEscape)
})

onBeforeUnmount(() => 
{
    document.removeEventListener('click', handleClickOutside)
    document.removeEventListener('keydown', handleEscape)
})
</script>

<template>
    <div ref="downloadRoot" :class="['relative inline-flex', props.compact ? '' : 'w-33']">
        <button type="button" id="downloadApp_A" :class="['group inline-flex! h-9 appearance-none items-center justify-center gap-2 overflow-hidden rounded border border-(--BorderBrush)! bg-(--ActionBlueBrush)! text-sm font-semibold leading-none text-(--ActionBlueTextBrush)! no-underline! shadow! transition! duration-200! visited:text-(--ActionBlueTextBrush)! visited:no-underline! md:hover:-translate-y-px hover:border-(--BorderBrush)! hover:bg-(--ActionBlueHoverBrush)! hover:text-(--ActionBlueTextBrush)! hover:no-underline! hover:shadow-[0_0_0_2px_var(--NavActionRingBrush),0_6px_14px_var(--NavActionShadowBrush)] focus:border-(--BorderBrush)! focus:bg-(--ActionBlueHoverBrush)! focus:text-(--ActionBlueTextBrush)! focus:no-underline! focus:shadow-[0_0_0_2px_var(--NavActionRingBrush),0_6px_14px_var(--NavActionShadowBrush)] focus:outline-none focus-visible:outline-none active:translate-y-0 active:scale-[0.97] active:border-(--BorderBrush)! active:bg-(--ActionBlueBrush)! active:text-(--ActionBlueTextBrush)! active:no-underline! active:shadow-inner!', props.compact ? 'w-9 px-0' : 'w-full px-3', isOpen ? 'rounded-b-none! border-b-0! bg-(--ActionBlueBrush)! hover:bg-(--ActionBlueBrush)! focus:bg-(--ActionBlueBrush)! md:hover:translate-y-0!' : '']" :aria-expanded="isOpen" aria-haspopup="menu" @click="toggleMenu">
            <svg
                xmlns="http://www.w3.org/2000/svg"
                class="h-4 w-4 shrink-0 transition-transform duration-200 md:group-hover:scale-110 group-active:scale-95"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
                aria-hidden="true">

                <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4" />
                <path d="M7 10l5 5 5-5" />
                <path d="M12 15V3" />
            </svg>
            <span :class="props.compact ? 'hidden' : 'inline-flex whitespace-nowrap'">
                {{ t('nav.downloadApp') }}
            </span>
        </button>

        <div v-if="isOpen" class="absolute right-0 top-full z-50 w-52 overflow-hidden rounded-b rounded-tl border border-t-0! border-(--BorderBrush)! bg-(--ActionBlueBrush)! py-1 text-sm text-(--ActionBlueTextBrush)! shadow-lg" role="menu">
            <div aria-hidden="true" :class="['absolute left-0 top-0 z-10 h-px bg-(--BorderBrush)', props.compact ? 'right-9' : 'right-[8.25rem]']"></div>
            <a v-for="option in downloadOptions" :key="option.href" :href="option.href" class="block px-3 py-2 text-(--ActionBlueTextBrush)! no-underline! transition hover:bg-(--ActionBlueBrush)! hover:text-(--ActionBlueTextBrush)! hover:no-underline!" role="menuitem" @click="closeMenu">
                {{ option.label }}
            </a>
        </div>
    </div>
</template>