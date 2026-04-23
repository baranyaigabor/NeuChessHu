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
    <div class="col">
        <label for="userLastName">{{ t('common.lastName') }}:</label><br>
        <input id="userLastName" 
               @change="onLastNameChange" 
               :value="props.value" 
               class="w-[6.5rem] m-0 bg-(--ButtonBrush) text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-(--BorderBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)] focus:bg-transparent! focus:outline-none" 
               type="text" 
               :placeholder="t('common.lastName')" 
               required 
               minlength="3" 
               maxlength="14">
    </div>
</template>
