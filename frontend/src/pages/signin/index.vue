<script setup>
import BaseCardLayout from '@layouts/BaseCardLayout.vue'
import BaseSingInCard from '@components/layout/BaseSignInCard.vue'
</script>

<template>
    <BaseCardLayout>
        <BaseSingInCard />
    </BaseCardLayout>
</template>

<style lang="css">
</style>

<route lang="yaml">
name: signin
meta:
    title: Sign In
</route>