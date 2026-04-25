<script setup>
import UserFirstName from './UserFirstNameInput.vue';
import UserLastName from './UserLastNameInput.vue';

const props = defineProps({
  firstName: {
    type: String,
    default: ""
  },
  lastName: {
    type: String,
    default: ""
  }
})

const emit = defineEmits(['firstNameChange', 'lastNameChange', 'firstNameBlur', 'lastNameBlur'])
</script>

<template>
    <UserFirstName :value="firstName" @firstNameChange="emit('firstNameChange', $event)" @firstNameBlur="emit('firstNameBlur')" />

    <UserLastName :value="lastName" @lastNameChange="emit('lastNameChange', $event)" @lastNameBlur="emit('lastNameBlur')" class="ml-5.5"/>
</template>
