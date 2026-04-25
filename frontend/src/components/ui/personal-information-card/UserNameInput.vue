<script setup>
import { useI18n } from '@utils/i18n'

const props = defineProps({
  value: {
    type: String,
    default: ""
  }
})

const emit = defineEmits(['usernameChange', 'usernameBlur'])
const { t } = useI18n()

function onUsernameChange(event) 
{
  const value = event.target.value
  emit('usernameChange', value)
}
</script>

<template>
    <div class="col">
        <label for="username">{{ t('common.username') }}: *</label><br>
        <input id="username" @input="onUsernameChange" @blur="emit('usernameBlur')" :value="props.value" class="w-full m-0 bg-(--ButtonBrush) text-(--FieldTextBrush) placeholder:text-(--FieldTextBrush) placeholder:opacity-60 p-1 ps-2 rounded-[5px] border border-(--BorderBrush) shadow-[inset_0_2px_5px_var(--InsetShadowBrush)]" 
               type="text" :placeholder="t('common.username')" required minlength="3" maxlength="14">
    </div>
</template>