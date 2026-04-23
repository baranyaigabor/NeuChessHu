<script setup>
import BaseLoginLayout from '@layouts/BaseLoginLayout.vue'
import BasePersonalInformationCard from '@components/layout/BasePersonalInformation.vue'
</script>

<template>
    <BaseLoginLayout>
        <BasePersonalInformationCard />
    </BaseLoginLayout>
</template>

<route lang="yaml">
name: personalinformation
meta:
    title: Personal Information
</route>