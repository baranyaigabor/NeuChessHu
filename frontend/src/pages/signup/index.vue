<script setup>
import BaseLoginLayout from '@layouts/BaseLoginLayout.vue'
import BaseSignUpCard from '@components/layout/BaseSignUpCard.vue'
</script>

<template>
    <BaseLoginLayout>
    <BaseSignUpCard />
    </BaseLoginLayout>
</template>

<style lang="css">
</style>

<route lang="yaml">
name: signup
meta:
    title: Sign Up
</route>