<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
  value: {
    type: String,
    default: ""
  }
})

const emit = defineEmits(['firstNameChange'])
const { t } = useI18n()

function onFirstNameChange(event) 
{
  emit('firstNameChange', event.target.value)
}
</script>

<template>                            
    <div class="col">
        <label for="userFirstName">{{ t('common.firstName') }}:</label><br>
        <input id="userFirstName" @change="onFirstNameChange" :value="props.value" class="w-[6.5rem] m-0 bg-[var(--ButtonBrush)] text-[var(--FieldTextBrush)] placeholder:text-[var(--FieldTextBrush)] placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-[var(--BorderBrush)] shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] !focus:bg-transparent focus:outline-none" type="text" :placeholder="t('common.firstName')" required minlength="3" maxlength="14">
    </div>
</template>
