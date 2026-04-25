<script setup>
import BaseCardLayout from '@layouts/BaseCardLayout.vue'
import BasePersonalInformationCard from '@components/layout/BasePersonalInformation.vue'
</script>

<template>
    <BaseCardLayout>
        <BasePersonalInformationCard />
    </BaseCardLayout>
</template>

<route lang="yaml">
name: personalinformation
meta:
    title: Personal Information
</route>