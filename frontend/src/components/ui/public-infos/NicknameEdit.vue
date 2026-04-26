<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
    value: {
        type: String,
        default: ""
    }
})

const emit = defineEmits(['nicknameChange'])
const { t } = useI18n()

function onNicknameChange(event) 
{
    emit('nicknameChange', event.target.value)
}
</script>

<template>
    <div class="w-full min-w-0 px-3">
        <input id="userNickname" @change="onNicknameChange" :value="props.value" class="m-0 h-9.5 w-full min-w-0 rounded-[5px] border border-black! bg-(--ButtonBrush) p-1 text-(--FieldTextBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] placeholder:text-(--FieldTextBrush) placeholder:opacity-60 focus:bg-(--ButtonBrush)! focus:outline-none" type="text" :placeholder="t('common.nickname')" required minlength="3" maxlength="14">
    </div>
</template>