<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
  value: {
    type: String,
    default: ""
  }
})

const emit = defineEmits(['firstNameChange', 'firstNameBlur'])
const { t } = useI18n()

function onFirstNameChange(event) 
{
  emit('firstNameChange', event.target.value)
}
</script>

<template>                            
    <div class="col">
        <label for="userFirstName">{{ t('common.firstName') }}:</label><br>
        <input id="userFirstName" @input="onFirstNameChange" @blur="emit('firstNameBlur')" :value="props.value" class="w-26 m-0 bg-(--ButtonBrush)! text-(--FieldTextBrush)! placeholder:text-(--FieldTextBrush)! placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-(--BorderBrush)! shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]" 
               type="text" :placeholder="t('common.firstName')" minlength="2" maxlength="14">
    </div>
</template>