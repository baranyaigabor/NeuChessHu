<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
    value: {
        type: String,
        default: ""
    }
})

const emit = defineEmits(['lastNameChange'])
const { t } = useI18n()

function onLastNameChange(event) 
{
    emit('lastNameChange', event.target.value)
}
</script>

<template>
    <div class="w-full min-w-0">
        <input id="userLastName" @change="onLastNameChange" :value="props.value" class="m-0 w-full min-w-0 rounded-[5px] border border-black! bg-(--ButtonBrush) p-1 px-1.5! text-(--FieldTextBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] placeholder:text-(--FieldTextBrush) placeholder:opacity-60 focus:bg-(--ButtonBrush)! focus:outline-none" type="text" :placeholder="t('common.lastName')" required minlength="3" maxlength="14">
    </div>
</template>