<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
    compact: {
        type: Boolean,
        default: false
    }
})

const { t } = useI18n()
const downloadButtonClass = 'group inline-flex! h-9 appearance-none items-center justify-center gap-2 overflow-hidden rounded border border-(--BorderBrush)! bg-(--ActionBlueBrush)! text-sm font-semibold leading-none text-(--ActionBlueTextBrush)! no-underline! shadow! transition! duration-200! visited:text-(--ActionBlueTextBrush)! visited:no-underline! md:hover:-translate-y-px hover:border-(--BorderBrush)! hover:bg-(--ActionBlueHoverBrush)! hover:text-(--ActionBlueTextBrush)! hover:no-underline! hover:shadow-[0_0_0_2px_var(--NavActionRingBrush),0_6px_14px_var(--NavActionShadowBrush)] focus:border-(--BorderBrush)! focus:bg-(--ActionBlueHoverBrush)! focus:text-(--ActionBlueTextBrush)! focus:no-underline! focus:shadow-[0_0_0_2px_var(--NavActionRingBrush),0_6px_14px_var(--NavActionShadowBrush)] focus:outline-none focus-visible:outline-none active:translate-y-0 active:scale-[0.97] active:border-(--BorderBrush)! active:bg-(--ActionBlueBrush)! active:text-(--ActionBlueTextBrush)! active:no-underline! active:shadow-inner!'
</script>

<template>
    <a href="#" id="downloadApp_A" @click.prevent :class="[downloadButtonClass, props.compact ? 'w-9 px-0' : 'w-33 px-3']">
        <svg
            xmlns="http://www.w3.org/2000/svg"
            class="h-4 w-4 shrink-0 transition-transform duration-200 md:group-hover:scale-110 group-active:scale-95"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
            aria-hidden="true"
        >
            <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4" />
            <path d="M7 10l5 5 5-5" />
            <path d="M12 15V3" />
        </svg>
        <span :class="props.compact ? 'hidden' : 'inline-flex whitespace-nowrap'">
            {{ t('nav.downloadApp') }}
        </span>
    </a>
</template>