<script setup>
import BaseLoginLayout from '@layouts/BaseLoginLayout.vue'
import BaseSingInCard from '@components/layout/BaseSignInCard.vue'
</script>

<template>
    <BaseLoginLayout>
        <BaseSingInCard />
    </BaseLoginLayout>
</template>

<style lang="css">
</style>

<route lang="yaml">
name: signin
meta:
    title: Sign In
</route>