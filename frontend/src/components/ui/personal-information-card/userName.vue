<script setup>
import UserFirstName from './userFirstNameInput.vue';
import UserLastName from './userLastNameInput.vue';

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

const emit = defineEmits(['firstNameChange', 'lastNameChange'])
</script>

<template>
    <UserFirstName :value="firstName" @firstNameChange="emit('firstNameChange', $event)"/>

    <UserLastName :value="lastName" @lastNameChange="emit('lastNameChange', $event)"/>
</template>